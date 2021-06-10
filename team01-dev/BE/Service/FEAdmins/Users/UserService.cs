using AutoMapper;
using Common.Constants;
using Common.Http;
using Common.Pagination;
using Domain.DTOs.User;
using Domain.Entities;
using Infrastructure.EntityFramework;
using Infrastructure.Extensions;
using Common.MD5;
using System;
using Domain.DTOs.Users;
using Common.Enums;
using System.Linq;
using Common.StringEx;
using Microsoft.EntityFrameworkCore;
using Service.Auth;

namespace Service.Users
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserManager _userManager;

        public UserService(IRepository<User> userRepository, IUnitOfWork unitOfWork, IMapper mapper, IUserManager userManager)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        public ReturnMessage<UserDTO> Create(CreateUserDTO model)
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
                _userRepository.Insert(entity);
                _unitOfWork.SaveChanges();
                var result = new ReturnMessage<UserDTO>(false, _mapper.Map<User, UserDTO>(entity), MessageConstants.CreateSuccess);
                return result;
            }
            catch (Exception ex)
            {
                return new ReturnMessage<UserDTO>(true, null, ex.Message);
            }
        }

        public ReturnMessage<UserDTO> Delete(DeleteUserDTO model)
        {
            try
            {
                if(model.Id == CommonConstantsUser.UserAdminId || model.Id == _userManager.AuthorizedUserId)
                {
                    return new ReturnMessage<UserDTO>(true, null, MessageConstants.Error);
                }
                var entity = _userRepository.Find(model.Id);
                if (entity.IsNotNullOrEmpty())
                {
                    entity.Delete();
                    _userRepository.Update(entity);
                    _unitOfWork.SaveChanges();
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

        public ReturnMessage<UserDTO> Update(UpdateUserDTO model)
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
                    _userRepository.Update(entity);
                    _unitOfWork.SaveChanges();
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

        public ReturnMessage<PaginatedList<UserDTO>> SearchPagination(SearchPaginationDTO<UserDTO> search)
        {
            if (search == null)
            {
                return new ReturnMessage<PaginatedList<UserDTO>>(false, null, MessageConstants.GetPaginationFail);
            }
            var query = _userRepository.Queryable().Where(it => it.Type == UserType.Admin &&
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
            var data = _mapper.Map<PaginatedList<User>, PaginatedList<UserDTO>>(resultEntity);
            var result = new ReturnMessage<PaginatedList<UserDTO>>(false, data, MessageConstants.GetPaginationSuccess);

            return result;
        }

        public ReturnMessage<UserDTO> GetDetailUser(Guid id)
        {
            try
            {
                var entity = _userRepository.Find(id);
                return new ReturnMessage<UserDTO>(false, _mapper.Map<User, UserDTO>(entity), MessageConstants.DeleteSuccess);
            }
            catch (Exception ex)
            {
                return new ReturnMessage<UserDTO>(true, null, ex.Message);
            }
        }
    }
}
