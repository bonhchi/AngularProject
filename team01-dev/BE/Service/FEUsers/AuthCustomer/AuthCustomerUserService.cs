using AutoMapper;
using Common.Constants;
using Common.Enums;
using Common.Http;
using Common.MD5;
using Domain.DTOs.Customer;
using Domain.DTOs.CustomerFE;
using Domain.DTOs.Mails;
using Domain.Entities;
using Infrastructure.EntityFramework;
using Infrastructure.Extensions;
using Infrastructure.Mails;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Service.Auth;
using Service.Customers;
using Service.Gmails;
using Service.InformationWebsiteServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Service.AuthCustomer
{
    public class AuthCustomerUserService : IAuthCustomerUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Customer> _customerRepository;

        private readonly IUserManager _userManager;
        private readonly IMapper _mapper;
        private readonly IGmailService _gmailService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IInfomationWebService _infomationWebService;

        public AuthCustomerUserService(IRepository<User> repository,
            IMapper mapper,
            IRepository<Customer> customerRepository,
            IUserManager userManager,
            IGmailService gmailService,
            IUnitOfWork unitOfWork, IInfomationWebService infomationWebService)
        {
            _userRepository = repository;
            _mapper = mapper;
            _customerRepository = customerRepository;
            _userManager = userManager;
            _gmailService = gmailService;
            _unitOfWork = unitOfWork;
            _infomationWebService = infomationWebService;
        }

        public ReturnMessage<CustomerDataReturnDTO> CheckLogin(CustomerLoginDTO data)
        {
            if (data.Username.IsNullOrEmpty() || data.Password.IsNullOrEmpty())
            {
                return new ReturnMessage<CustomerDataReturnDTO>(true, null, MessageConstants.InvalidAuthInfoMsg);
            }

            try
            {
                var account = _userRepository.Queryable().Include(it => it.Customer).Where(a => !a.IsDeleted && a.Type == UserType.Customer && a.Username == data.Username && a.Password == MD5Helper.ToMD5Hash(data.Password)).FirstOrDefault();
                if (account.IsNullOrEmpty())
                {
                    return new ReturnMessage<CustomerDataReturnDTO>(true, null, MessageConstants.InvalidAuthInfoMsg);
                }

                var claims = new Claim[]
                {
                    new Claim(ClaimTypes.UserData, account.Username),
                    new Claim(ClaimTypes.NameIdentifier,account.Id.ToString()),
                };

                // Generate JWT token
                var token = _userManager.GenerateToken(claims, DateTime.UtcNow);
                var result = _mapper.Map<User, CustomerDataReturnDTO>(account);
                result.Token = token;
                return new ReturnMessage<CustomerDataReturnDTO>(false, result, MessageConstants.LoginSuccess);
            }
            catch (Exception ex)
            {
                return new ReturnMessage<CustomerDataReturnDTO>(true, null, ex.Message);
            }
        }

        public ReturnMessage<CustomerDataReturnDTO> CheckRegister(CustomerRegisterDTO data)
        {
            try
            {
                var model = _mapper.Map<CustomerRegisterDTO, CreateCustomerDTO>(data);
                if (model.Username.Trim() == "")
                    return new ReturnMessage<CustomerDataReturnDTO>(true, null, MessageConstants.Error);

                if (_userRepository.Queryable().Any(it => it.Type == UserType.Customer && it.Username.CompareTo(model.Username) == 0))
                {
                    return new ReturnMessage<CustomerDataReturnDTO>(true, null, MessageConstants.ExistUsername);
                }

                if (_userRepository.Queryable().Any(it => it.Type == UserType.Customer && it.Email.CompareTo(model.Email) == 0))
                {
                    return new ReturnMessage<CustomerDataReturnDTO>(true, null, MessageConstants.ExistEmail);
                }

                if (_customerRepository.Queryable().Any(it => model.Phone.IsNotNullOrEmpty() && it.Phone.CompareTo(model.Phone) == 0))
                {
                    return new ReturnMessage<CustomerDataReturnDTO>(true, null, MessageConstants.ExistPhone);
                }
                var user = _mapper.Map<CreateCustomerDTO, User>(model);
                user.Password = MD5Helper.ToMD5Hash(model.Password);
                user.Insert();
                user.Type = UserType.Customer;

                var customer = _mapper.Map<CreateCustomerDTO, Customer>(model);
                customer.Insert();

                _unitOfWork.BeginTransaction();
                _userRepository.Insert(user);

                customer.UserId = user.Id;
                customer.User = user;
                _customerRepository.Insert(customer);

                _unitOfWork.SaveChanges();

                user.CustomerId = customer.Id;
                user.Customer = customer;
                _userRepository.Update(user);
                _unitOfWork.SaveChanges();

                var claims = new Claim[]
                    {
                    new Claim(ClaimTypes.UserData, user.Username),
                    new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                    };

                // Generate JWT token
                var token = _userManager.GenerateToken(claims, DateTime.UtcNow);
                var result = _mapper.Map<User, CustomerDataReturnDTO>(user);
                result.Token = token;

                ////GetInfoWebsite
                //var infowebsite = _infomationWebService.GetInfo();
                //if (infowebsite.HasError)
                //{
                //    throw new Exception(infowebsite.Message);
                //}

                ////Send Mail
                //var sendMail = new SendMailDTO()
                //{
                //    ToAddresse = new EmailAddress()
                //    {
                //        Address = result.Email,
                //        Name = result.FirstName
                //    },
                //    FromAddresse = new EmailAddress()
                //    {
                //        Address = infowebsite.Data.Email,
                //        Name = "Admin",
                //    },
                //    Content = "Congratulations " + result.LastName + " " + result.FirstName + " have successfully created an account at " + infowebsite.Data.Title,
                //    ContentType = EmailContentTypeEnum.TEXT,
                //    Subject = "Register Success " + infowebsite.Data.Title
                //};
                //var send = _gmailService.SendMail(sendMail);
                //if (send.HasError)
                //{
                //    throw new Exception(send.Message);
                //}

                _unitOfWork.Commit();
                return new ReturnMessage<CustomerDataReturnDTO>(false, result, MessageConstants.RegisterSuccess);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                return new ReturnMessage<CustomerDataReturnDTO>(true, null, ex.Message);
            }
        }

        public ReturnMessage<CustomerDataReturnDTO> GetCustomerDataReturnDTO()
        {
            try
            {
                var entity = _userRepository.Queryable().Include(it => it.Customer).Where(it => it.Type == UserType.Customer && !it.IsDeleted && it.Id == _userManager.AuthorizedUserId && it.Type == UserType.Customer).FirstOrDefault();
                if (entity.IsNullOrEmpty())
                {
                    return new ReturnMessage<CustomerDataReturnDTO>(true, null, MessageConstants.Error);
                }

                var result = _mapper.Map<User, CustomerDataReturnDTO>(entity);
                return new ReturnMessage<CustomerDataReturnDTO>(false, result, MessageConstants.LoginSuccess);
            }
            catch (Exception ex)
            {
                return new ReturnMessage<CustomerDataReturnDTO>(true, null, ex.Message);
            }
        }        

        public ReturnMessage<string> ForgetPassword(CustomerEmailDTO model)
        {
            try
            {
                var user = _userRepository.Queryable().FirstOrDefault(it => !it.IsDeleted && it.Type == UserType.Customer && it.Email == model.Email);
                if (user.IsNullOrEmpty())
                {
                    return new ReturnMessage<string>(true, "Don't find email", MessageConstants.SearchSuccess);
                }
                //var newPassword = new Random().Next(100000, 999999);
                //user.Password = MD5Helper.ToMD5Hash(newPassword.ToString());
                //_unitOfWork.BeginTransaction();
                //_userRepository.Update(user);
                //_unitOfWork.SaveChanges();

                ////GetInfoWebsite
                //var infowebsite = _infomationWebService.GetInfo();
                //if (infowebsite.HasError)
                //{
                //    throw new Exception(infowebsite.Message);
                //}

                ////Send Mail
                //var sendMail = new SendMailDTO()
                //{
                //    ToAddresse = new EmailAddress()
                //    {
                //        Address = user.Email,
                //        Name = user.FirstName
                //    },
                //    FromAddresse = new EmailAddress()
                //    {
                //        Address = infowebsite.Data.Email,
                //        Name = "Admin",
                //    },
                //    Content = "<h1>Hi " + user.LastName + " " + user.FirstName + "</h1><br><br>" + "Username: " +user.Username + "<br>" + "New Password: " + newPassword + "<br><br>Thank you!",
                //    ContentType = EmailContentTypeEnum.HTML,
                //    Subject = "Forgetpassword " + infowebsite.Data.Title
                //};
                //var send = _gmailService.SendMail(sendMail);
                //if (send.HasError)
                //{
                //    throw new Exception(send.Message);
                //}

                //_unitOfWork.Commit();
                return new ReturnMessage<string>(false, "Please check your email", MessageConstants.SearchSuccess);
            }
            catch (Exception ex)
            {
                //_unitOfWork.Rollback();
                return new ReturnMessage<string>(true, null, ex.Message);
            }
        }
    }
}
