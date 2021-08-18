using AutoMapper;
using Common.Constants;
using Common.Http;
using Common.Pagination;
using Domain.Entities;
using Infrastructure.EntityFramework;
using Infrastructure.Extensions;
using Common.MD5;
using System;
using Domain.DTOs.Users;
using Common.Enums;
using System.Linq;
using Common.StringEx;
using Service.Auth;
using System.Threading.Tasks;

namespace Service.Users
{
    public class UserService : IUserService
    {
        private readonly IRepositoryAsync<User> _userRepository;
        private readonly IUnitOfWorkAsync _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserManager _userManager;

        public UserService(IRepositoryAsync<User> userRepository, IUnitOfWorkAsync unitOfWork, IMapper mapper, IUserManager userManager)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<ReturnMessage<UserDTO>> CreateAsync(CreateUserDTO model)
        {
            //if (model.Username.Trim() == "" || model.Password.Trim() == "")
            //    return new ReturnMessage<UserDTO>(true, null, MessageConstants.Error);

            model.Username = StringExtension.CleanString(model.Username);
            model.Password = StringExtension.CleanString(model.Password);
            if(model.Username == null || model.Password == null)
            {
                return new ReturnMessage<UserDTO>(true, null, MessageConstants.InvalidString);
            }

            if (_userRepository.Queryable().Any(it => it.Username == model.Username && it.Type == UserType.Admin))
            {
                return new ReturnMessage<UserDTO>(true, null, "0", MessageConstants.ExistUsername);
            }

            if (_userRepository.Queryable().Any(it => it.Email == model.Email && it.Type == UserType.Admin))
            {
                return new ReturnMessage<UserDTO>(true, null, "1", MessageConstants.ExistEmail);
            }
            try
            {
                var entity = _mapper.Map<CreateUserDTO, User>(model);
                entity.Password = MD5Helper.ToMD5Hash(model.Password);
                entity.Insert();
                entity.Type = UserType.Admin;
                _userRepository.InsertAsync(entity);
                await _unitOfWork.SaveChangesAsync();
                var result = new ReturnMessage<UserDTO>(false, _mapper.Map<User, UserDTO>(entity), MessageConstants.CreateSuccess);
                return result;
            }
            catch (Exception ex)
            {
                return new ReturnMessage<UserDTO>(true, null, ex.Message);
            }
        }

        public async Task <ReturnMessage<UserDTO>> DeleteAsync(DeleteUserDTO model)
        {
            try
            {
                if(model.Id == CommonConstantsUser.UserAdminId || model.Id == _userManager.AuthorizedUserId)
                {
                    return new ReturnMessage<UserDTO>(true, null, MessageConstants.Error);
                }
                var entity = await _userRepository.FindAsync(model.Id);
                if (entity.IsNotNullOrEmpty())
                {
                    entity.Delete();
                    _userRepository.Update(entity);
                    await _unitOfWork.SaveChangesAsync();
                    var result = new ReturnMessage<UserDTO>(false, _mapper.Map<User, UserDTO>(entity), MessageConstants.DeleteSuccess);
                    return result;
                }
                return new ReturnMessage<UserDTO>(true, null, MessageConstants.Error);
            }
            catch (Exception ex)
            {
                return new ReturnMessage<UserDTO>(true, null, ex.Message);
            }
        }

        public async Task <ReturnMessage<UserDTO>> UpdateAsync(UpdateUserDTO model)
        {
            if (model.Id != _userManager.AuthorizedUserId && model.Id == CommonConstantsUser.UserAdminId)
            {
                return new ReturnMessage<UserDTO>(true, null, MessageConstants.Error);
            }
            model.Username = StringExtension.CleanString(model.Username);
            model.Password = StringExtension.CleanString(model.Password);
            if (model.Username == null || model.Password == null)
            {
                return new ReturnMessage<UserDTO>(true, null, MessageConstants.InvalidString);
            }
            try
            {
                //if (model.Username.Trim() == "")
                //    return new ReturnMessage<UserDTO>(false, null, MessageConstants.CreateSuccess);
                var entity = _userRepository.Find(model.Id);
                if (entity.IsNotNullOrEmpty())
                {
                    entity.Update(model);
                    await _userRepository.UpdateAsync(entity);
                    await _unitOfWork.SaveChangesAsync();
                    var result = new ReturnMessage<UserDTO>(false, _mapper.Map<User, UserDTO>(entity), MessageConstants.UpdateSuccess);
                    return result;
                }
                return new ReturnMessage<UserDTO>(true, null, MessageConstants.Error);
            }
            catch (Exception ex)
            {
                return new ReturnMessage<UserDTO>(true, null, ex.Message);
            }
        }
        public async Task<ReturnMessage<PaginatedList<UserDTO>>> SearchPagination(SearchPaginationDTO<UserDTO> search)
        {
            if (search == null)
            {
                return new ReturnMessage<PaginatedList<UserDTO>>(false, null, MessageConstants.GetPaginationFail);
            }
            var query = _userRepository.Queryable().Where(it => it.Type == UserType.Admin &&
                    (search.Search == null ||
                        
                            
                                (search.Search.Id != Guid.Empty && it.Id == search.Search.Id) ||
                                it.Username.Contains(search.Search.Username) ||
                                it.Email.Contains(search.Search.Email) ||
                                it.FirstName.Contains(search.Search.FirstName) ||
                                it.LastName.Contains(search.Search.LastName) ||
                                it.ImageUrl.Contains(search.Search.ImageUrl)
                            
                        
                    )
                )
                .OrderBy(it => it.Username)
                .ThenBy(it => it.Username.Length);
            var resultEntity = new PaginatedList<User>(query, search.PageIndex * search.PageSize, search.PageSize);
            var data = _mapper.Map<PaginatedList<User>, PaginatedList<UserDTO>>(resultEntity);
            var result = new ReturnMessage<PaginatedList<UserDTO>>(false, data, MessageConstants.GetPaginationSuccess);

            await Task.CompletedTask;
            return result;
        }

        public async Task<ReturnMessage<UserDTO>> GetDetailUser(Guid id)
        {
            try
            {
                var entity = await _userRepository.FindAsync(id);
                return new ReturnMessage<UserDTO>(false, _mapper.Map<User, UserDTO>(entity), MessageConstants.DeleteSuccess);
            }
            catch (Exception ex)
            {
                return new ReturnMessage<UserDTO>(true, null, ex.Message);
            }
        }
    }
}
