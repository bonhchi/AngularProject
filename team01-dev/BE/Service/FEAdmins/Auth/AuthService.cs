using AutoMapper;
using Common;
using Common.Constants;
using Common.Enums;
using Common.Http;
using Common.MD5;
using Data;
using Domain.DTOs;
using Domain.DTOs.User;
using Domain.Entities;
using Infrastructure.EntityFramework;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Service.Auth
{
    public class AuthService : IAuthService
    {
        private IRepository<User> _userRepository;
        private IUserManager _userManager;
        private IMapper _mapper;

        public AuthService(
            IUserManager userManager, IRepository<User> repository, IMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _userRepository = repository;
            _mapper = mapper;
        }

        public ReturnMessage<UserDataReturnDTO> CheckLogin(UserLoginDTO data)
        {
            if (data.Username.IsNullOrEmpty() || data.Password.IsNullOrEmpty())
            {
                return new ReturnMessage<UserDataReturnDTO>(true, null, MessageConstants.InvalidAuthInfoMsg);
            }

            try
            {
                var account = _userRepository.Queryable().Where(a => !a.IsDeleted && a.Type == UserType.Admin && a.Username == data.Username && a.Password == MD5Helper.ToMD5Hash(data.Password)).FirstOrDefault();
                if (account.IsNullOrEmpty())
                {
                    return new ReturnMessage<UserDataReturnDTO>(true, null, MessageConstants.InvalidAuthInfoMsg);
                }

                var claims = new Claim[]
                {
                    new Claim(ClaimTypes.UserData, account.Username),
                    new Claim(ClaimTypes.NameIdentifier,account.Id.ToString()),
                };

                // Generate JWT token
                var token = _userManager.GenerateToken(claims, DateTime.UtcNow);
                var result = _mapper.Map<User, UserDataReturnDTO>(account);
                result.Token = token;
                return new ReturnMessage<UserDataReturnDTO>(false, result, MessageConstants.LoginSuccess);
            }
            catch (Exception ex)
            {
                return new ReturnMessage<UserDataReturnDTO>(true, null, ex.Message);
            }
        }

        public ReturnMessage<UserDataReturnDTO> GetInformationUser()
        {
            try
            {
                var data = _userRepository.Queryable().Where(it => it.Id == _userManager.AuthorizedUserId
                && it.Type == UserType.Admin).FirstOrDefault();
                if (data.IsNullOrEmpty())
                {
                    return new ReturnMessage<UserDataReturnDTO>(true, null, MessageConstants.Error);
                }

                var result = _mapper.Map<User, UserDataReturnDTO>(data);
                return new ReturnMessage<UserDataReturnDTO>(false, result, MessageConstants.LoginSuccess);
            }
            catch (Exception ex)
            {
                return new ReturnMessage<UserDataReturnDTO>(true, null, ex.Message);
            }
        }
    }
}
