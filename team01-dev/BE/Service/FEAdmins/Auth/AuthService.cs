using AutoMapper;
using Common.Constants;
using Common.Enums;
using Common.Http;
using Common.MD5;
using Domain.DTOs.User;
using Domain.Entities;
using Infrastructure.EntityFramework;
using Infrastructure.Extensions;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Service.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IRepositoryAsync<User> _userRepository;
        private readonly IUserManager _userManager;
        private readonly IMapper _mapper;

        public AuthService(
            IUserManager userManager, IRepositoryAsync<User> repository, IMapper mapper)
        {
            _userManager = userManager;
            _userRepository = repository;
            _mapper = mapper;
        }

        public async Task<ReturnMessage<UserDataReturnDTO>> CheckLogin(UserLoginDTO data)
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
                var token = await _userManager.GenerateToken(claims, DateTime.UtcNow);
                var result = _mapper.Map<User, UserDataReturnDTO>(account);
                result.Token = token;
                return new ReturnMessage<UserDataReturnDTO>(false, result, MessageConstants.LoginSuccess);
            }
            catch (Exception ex)
            {
                return new ReturnMessage<UserDataReturnDTO>(true, null, ex.Message);
            }
        }

        public async Task<ReturnMessage<UserDataReturnDTO>> GetInformationUser()
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
                await Task.CompletedTask;
                return new ReturnMessage<UserDataReturnDTO>(false, result, MessageConstants.LoginSuccess);
            }
            catch (Exception ex)
            {
                return new ReturnMessage<UserDataReturnDTO>(true, null, ex.Message);
            }
        }
    }
}
