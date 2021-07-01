using AutoMapper;
using Common.Constants;
using Common.Http;
using Common.MD5;
using Common.StringEx;
using Domain.DTOs.Profiles;
using Domain.DTOs.User;
using Domain.DTOs.Users;
using Domain.Entities;
using Infrastructure.EntityFramework;
using Infrastructure.Extensions;
using Service.Auth;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Profiles
{
    public class ProfileService : IProfileService
    {
        private readonly IRepositoryAsync<User> _userRepository;
        private readonly IUnitOfWorkAsync _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserManager _userManager;
        private readonly IAuthService _authService;



        public ProfileService(IRepositoryAsync<User> userRepository, IUnitOfWorkAsync unitOfWork, IMapper mapper, IUserManager userManager, IAuthService authService)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _authService = authService;
        }

        public ReturnMessage<UpdateProfileDTO> ChangePassword(ChangePassworProfileDTO model)
        {
            try
            {
                var entity = _userRepository.Queryable().FirstOrDefault(it => (it.Username == model.Username) && (it.Password == MD5Helper.ToMD5Hash(model.Password)) );
                if (entity.IsNotNullOrEmpty() && (model.ConfirmNewPassword == model.NewPassword))
                {
                    entity.ChangePassword(model);
                    _userRepository.Update(entity);
                    _unitOfWork.SaveChangesAsync();
                    var result = new ReturnMessage<UpdateProfileDTO>(false, _mapper.Map<User, UpdateProfileDTO>(entity), MessageConstants.UpdateSuccess);
                    return result;
                }
                return new ReturnMessage<UpdateProfileDTO>(true, null, MessageConstants.InvalidAuthInfoMsg);
            }
            catch (Exception ex)
            {
                return new ReturnMessage<UpdateProfileDTO>(true, null, ex.Message);
            }
        }

        public async Task<ReturnMessage<UserDataReturnDTO>> UpdateAsync(UpdateProfileDTO model)
        {
            model.FirstName = StringExtension.CleanString(model.FirstName);
            model.LastName = StringExtension.CleanString(model.LastName);
            model.Email = StringExtension.CleanString(model.Email);
            if (model.FirstName == null || model.LastName == null || model.Email == null)
            {
                return new ReturnMessage<UserDataReturnDTO>(true, null, MessageConstants.InvalidString);
            }
            try
            {
                var entity = await _userRepository.FindAsync(model.Id);
                if (entity.IsNotNullOrEmpty())
                {
                    entity.UpdateProfile(model);
                    _userRepository.Update(entity);
                    await _unitOfWork.SaveChangesAsync();
                    var result = new ReturnMessage<UserDataReturnDTO>(false, _mapper.Map<User, UserDataReturnDTO>(entity), MessageConstants.DeleteSuccess);
                    return result;
                }
                return new ReturnMessage<UserDataReturnDTO>(true, null, MessageConstants.Error);
            }
            catch (Exception ex)
            {
                return new ReturnMessage<UserDataReturnDTO>(true, null, ex.Message);
            }
        }
    }
}
