using AutoMapper;
using Common.Constants;
using Common.Enums;
using Common.Http;
using Common.MD5;
using Common.Pagination;
using Common.StringEx;
using Domain.DTOs.Customer;
using Domain.DTOs.Users;
using Domain.Entities;
using Infrastructure.EntityFramework;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Service.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.Customers
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserManager _userManager;

        public CustomerService(IRepository<User> userRepository, IUnitOfWork unitOfWork, IMapper mapper, IRepository<Customer> customerRepository, IUserManager userManager)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _customerRepository = customerRepository;
            _userManager = userManager;
        }

        public ReturnMessage<CustomerDTO> Create(CreateCustomerDTO model)
        {
            model.Username = StringExtension.CleanString(model.Username);
            model.Password = StringExtension.CleanString(model.Password);
            if(model.Username == null || model.Password == null)
            {
                var entity = _mapper.Map<CreateCustomerDTO, Customer>(model);
                return new ReturnMessage<CustomerDTO>(true, _mapper.Map<Customer, CustomerDTO>(entity), MessageConstants.InvalidString);
            }
            try
            {
                var userInfo = _userManager.GetInformationUser();
                if (userInfo.IsNullOrEmpty())
                {
                    return new ReturnMessage<CustomerDTO>(true, null, MessageConstants.CreateFail);
                }
                if (model.Username.Trim() == "")
                    return new ReturnMessage<CustomerDTO>(true, null, MessageConstants.Error);

                if(_userRepository.Queryable().Any(it => it.Type == UserType.Customer && it.Username.CompareTo(model.Username) == 0))
                {
                    return new ReturnMessage<CustomerDTO>(true, null, MessageConstants.ExistUsername);
                }

                if(_userRepository.Queryable().Any(it =>it.Type == UserType.Customer && it.Email.CompareTo(model.Email) == 0))
                {
                    return new ReturnMessage<CustomerDTO>(true, null, MessageConstants.ExistEmail);
                }

                if(_customerRepository.Queryable().Any(it => model.Phone.IsNotNullOrEmpty() && it.Phone.CompareTo(model.Phone) == 0))
                {
                    return new ReturnMessage<CustomerDTO>(true, null, MessageConstants.ExistPhone);
                }
                var user = _mapper.Map<CreateCustomerDTO, User>(model);
                user.Password = MD5Helper.ToMD5Hash(model.Password);
                user.Insert(userInfo);
                user.Type = UserType.Customer;

                var customer = _mapper.Map<CreateCustomerDTO, Customer>(model);
                customer.Insert(userInfo);

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

                _unitOfWork.Commit();
                var result = new ReturnMessage<CustomerDTO>(false, _mapper.Map<User, CustomerDTO>(user), MessageConstants.CreateSuccess);
                return result;
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                return new ReturnMessage<CustomerDTO>(true, null, ex.Message);
            }
        }

        public ReturnMessage<CustomerDTO> Delete(DeleteCustomerDTO model)
        {
            try
            {
                var userInfo = _userManager.GetInformationUser();
                if (userInfo.IsNullOrEmpty())
                {
                    return new ReturnMessage<CustomerDTO>(true, null, MessageConstants.CreateFail);
                }
                var user = _userRepository.Queryable().FirstOrDefault(it => it.Id == model.Id && it.Type == UserType.Customer && !it.IsDeleted);
                if (user.IsNullOrEmpty())
                {
                    return new ReturnMessage<CustomerDTO>(true, null, MessageConstants.Error);
                }

                user.Delete(userInfo);

                var customer = _customerRepository.Find(user.CustomerId);

                _unitOfWork.BeginTransaction();
                _userRepository.Update(user);
                if(customer.IsNotNullOrEmpty())
                {
                    customer.Delete(userInfo);
                    _customerRepository.Update(customer);
                }
                _unitOfWork.SaveChanges();
                _unitOfWork.Commit();
                var result = new ReturnMessage<CustomerDTO>(false, _mapper.Map<User, CustomerDTO>(user), MessageConstants.DeleteSuccess);
                return result;
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                return new ReturnMessage<CustomerDTO>(true, null, ex.Message);
            }
        }

        public ReturnMessage<CustomerDTO> Update(UpdateCustomerDTO model)
        {
            model.Username = StringExtension.CleanString(model.Username);
            model.Password = StringExtension.CleanString(model.Password);
            if (model.Username == null || model.Password == null)
            {
                var entity = _mapper.Map<UpdateCustomerDTO, Customer>(model);
                return new ReturnMessage<CustomerDTO>(true, _mapper.Map<Customer, CustomerDTO>(entity), MessageConstants.InvalidString);
            }
            try
            {
                var userInfo = _userManager.GetInformationUser();
                if (userInfo.IsNullOrEmpty())
                {
                    return new ReturnMessage<CustomerDTO>(true, null, MessageConstants.CreateFail);
                }
                //if (model.Username.Trim() == "")
                //{
                //    return new ReturnMessage<CustomerDTO>(true, null, MessageConstants.Error);
                //}

                var user = _userRepository.Queryable().FirstOrDefault(it => it.Id == model.Id && it.Type == UserType.Customer && !it.IsDeleted);
                if (user.IsNullOrEmpty())
                {
                    return new ReturnMessage<CustomerDTO>(true, null, MessageConstants.Error);
                }

                var customer = _customerRepository.Find(user.CustomerId);
                if (customer.IsNullOrEmpty())
                {
                    return new ReturnMessage<CustomerDTO>(true, null, MessageConstants.Error);
                }

                if (_userRepository.Queryable().Any(it => it.Type == UserType.Customer && it.Username == model.Username && it.Id != user.Id))
                {
                    return new ReturnMessage<CustomerDTO>(true, null, MessageConstants.ExistUsername);
                }

                if (_userRepository.Queryable().Any(it => it.Type == UserType.Customer && it.Email == model.Email && it.Id != user.Id))
                {
                    return new ReturnMessage<CustomerDTO>(true, null, MessageConstants.ExistEmail);
                }

                if (_customerRepository.Queryable().Any(it => model.Phone.IsNotNullOrEmpty() && it.Phone == model.Phone && it.Id != user.CustomerId))
                {
                    return new ReturnMessage<CustomerDTO>(true, null, MessageConstants.ExistPhone);
                }

                _unitOfWork.BeginTransaction();
                user.Update(userInfo, model);
                _userRepository.Update(user);
                customer.Update(userInfo, model);
                _customerRepository.Update(customer);
                _unitOfWork.SaveChanges();
                _unitOfWork.Commit();
                var result = new ReturnMessage<CustomerDTO>(false, _mapper.Map<User, CustomerDTO>(user), MessageConstants.UpdateSuccess);
                return result;
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                return new ReturnMessage<CustomerDTO>(true, null, ex.Message);
            }
        }

        public ReturnMessage<PaginatedList<CustomerDTO>> SearchPagination(SearchPaginationDTO<CustomerDTO> search)
        {
            if (search == null)
            {
                return new ReturnMessage<PaginatedList<CustomerDTO>>(false, null, MessageConstants.GetPaginationFail);
            }

            var query = _userRepository.Queryable().Include(it => it.Customer).Where(it => it.Type == UserType.Customer && it.IsDeleted == false &&
                    (search.Search == null ||
                        (
                            (
                                (search.Search.Id == Guid.Empty ? false : it.Id == search.Search.Id) ||
                                it.Username.Contains(search.Search.Username) ||
                                it.Email.Contains(search.Search.Email) ||
                                it.FirstName.Contains(search.Search.FirstName) ||
                                it.LastName.Contains(search.Search.LastName) ||
                                it.ImageUrl.Contains(search.Search.ImageUrl)
                            )
                        )
                    )
                )
                .OrderBy(it => it.Username)
                .ThenBy(it => it.Username.Length);
            var resultEntity = new PaginatedList<User>(query, search.PageIndex * search.PageSize, search.PageSize);
            var data = _mapper.Map<PaginatedList<User>, PaginatedList<CustomerDTO>>(resultEntity);
            var result = new ReturnMessage<PaginatedList<CustomerDTO>>(false, data, MessageConstants.GetPaginationSuccess);

            return result;
        }
    }
}
