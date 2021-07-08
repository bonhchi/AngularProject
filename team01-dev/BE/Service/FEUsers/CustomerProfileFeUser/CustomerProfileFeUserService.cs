using AutoMapper;
using Common.Constants;
using Common.Enums;
using Common.Http;
using Common.MD5;
using Domain.DTOs.CustomerFE;
using Domain.DTOs.CustomerProfileFeUser;
using Domain.DTOs.Users;
using Domain.Entities;
using Infrastructure.EntityFramework;
using Infrastructure.Extensions;
using Service.Auth;
using Service.AuthCustomer;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Service.CustomerProfileFeUser
{
    public class CustomerProfileFeUserService : ICustomerProfileFeUserService
    {
        private readonly IRepositoryAsync<User> _userRepository;
        private readonly IRepositoryAsync<Customer> _customerRepository;
        private readonly IAuthCustomerUserService _authCustomerUserService;//not use
        private readonly IUnitOfWorkAsync _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserManager _userManager;

        public CustomerProfileFeUserService(IRepositoryAsync<User> userRepository, IRepositoryAsync<Customer> customerRepository, IAuthCustomerUserService authCustomerUserService, IUnitOfWorkAsync unitOfWork, IMapper mapper, IAuthService authService, IUserManager userManager)
        {
            _userRepository = userRepository;
            _customerRepository = customerRepository;
            _authCustomerUserService = authCustomerUserService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<ReturnMessage<CustomerDataReturnDTO>> ChangePassword(ChangePasswordCustomerProfileFeUserDTO model)
        {
            try
            {
                var user = _userRepository.Queryable().FirstOrDefault(it => it.Type == UserType.Customer
                                                                            && it.Id == _userManager.AuthorizedUserId
                                                                            && it.Password == MD5Helper.ToMD5Hash(model.Password));

                if (user.IsNullOrEmpty())
                {
                    return new ReturnMessage<CustomerDataReturnDTO>(true, null, MessageConstants.InvalidAuthInfoMsg);
                }

                if (model.Password == model.NewPassword)
                {
                    return new ReturnMessage<CustomerDataReturnDTO>(true, null, MessageConstants.CurrentPasswordEqualNewPassword);
                }
                var userInfo = _mapper.Map<User, UserInformationDTO>(user);
                user.ChangePassword(userInfo, model);
                await _userRepository.UpdateAsync(user);
                await _unitOfWork.SaveChangesAsync();

                return new ReturnMessage<CustomerDataReturnDTO>(false, null, MessageConstants.UpdateSuccess);
            }
            catch (Exception ex)
            {
                return new ReturnMessage<CustomerDataReturnDTO>(true, null, ex.Message);
            }
        }

        public async Task<ReturnMessage<CustomerDataReturnDTO>> UpdateProfile(UpdateCustomerProfileFeUserDTO model)
        {
            try
            {
                var user = _userRepository.Queryable()
                    .FirstOrDefault(it => it.Type == UserType.Customer && it.Id == _userManager.AuthorizedUserId && !it.IsDeleted);
                if (user.IsNullOrEmpty())
                {
                    return new ReturnMessage<CustomerDataReturnDTO>(true, null, MessageConstants.InvalidAuthInfoMsg);
                }


                if (_userRepository.Queryable().Any(it => it.Email == model.Email && it.Id != user.Id))
                {
                    return new ReturnMessage<CustomerDataReturnDTO>(true, null, MessageConstants.ExistEmail);
                }

                if (_customerRepository.Queryable().Any(it => model.Phone.IsNotNullOrEmpty() && it.Phone == model.Phone && it.Id != user.CustomerId))
                {
                    return new ReturnMessage<CustomerDataReturnDTO>(true, null, MessageConstants.ExistPhone);
                }

                var customer = _customerRepository.Find(user.CustomerId);
                if (customer.IsNullOrEmpty())
                {
                    return new ReturnMessage<CustomerDataReturnDTO>(true, null, MessageConstants.Error);
                }

                var userInfo = _mapper.Map<User, UserInformationDTO>(user);
                user.UpdateProfile(userInfo, model);
                customer.UpdateProfile(userInfo, model);

                _unitOfWork.BeginTransaction();
                await _userRepository.UpdateAsync(user);
                await _customerRepository.UpdateAsync(customer);
                await _unitOfWork.SaveChangesAsync();
                _unitOfWork.Commit();

                return new ReturnMessage<CustomerDataReturnDTO>(false, _mapper.Map<User, CustomerDataReturnDTO>(user), MessageConstants.UpdateSuccess);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                return new ReturnMessage<CustomerDataReturnDTO>(true, null, ex.Message);
            }
        }
    }
}
