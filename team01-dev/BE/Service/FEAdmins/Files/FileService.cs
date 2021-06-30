using AutoMapper;
using Common.Constants;
using Common.Http;
using Common.Pagination;
using Domain.DTOs.Categories;
using Domain.DTOs.Files;
using Domain.Entities;
using Infrastructure.EntityFramework;
using Infrastructure.Extensions;
using Service.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Files
{
    public class FileService : IFileService
    {
        private readonly IRepositoryAsync<File> _fileRepository;
        private readonly IUnitOfWorkAsync _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserManager _userManager;

        public FileService(IMapper mapper,
            IUnitOfWorkAsync unitOfWork,
            IRepositoryAsync<File> fileRepository,
            IUserManager userManager)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _fileRepository = fileRepository;
            _userManager = userManager;
        }

        public async Task<ReturnMessage<List<FileDTO>>> CreateAsync(List<CreateFileDTO> model)
        {
            if (model.IsNullOrEmpty())
            {
                return new ReturnMessage<List<FileDTO>>(true, null, MessageConstants.Error);
            }

            try
            {
                var userInfo = await _userManager.GetInformationUser();
                if (userInfo.IsNullOrEmpty())
                {
                    return new ReturnMessage<List<FileDTO>>(true, null, MessageConstants.CreateFail);
                }
                var entities = _mapper.Map<List<CreateFileDTO>, List<Domain.Entities.File>>(model);
                entities.ForEach(it => it.Insert(userInfo));
                _unitOfWork.BeginTransaction();
                _fileRepository.InsertRange(entities);
                await _unitOfWork.SaveChangesAsync();
                _unitOfWork.Commit();
                var result = new ReturnMessage<List<FileDTO>>(false, _mapper.Map<List<Domain.Entities.File>, List<FileDTO>>(entities), MessageConstants.CreateSuccess + " " + model.Count + " files");
                return result;
            }
            catch (Exception ex)
            {
                return new ReturnMessage<List<FileDTO>>(true, null, ex.Message);
            }

        }

        public async Task<ReturnMessage<List<FileDTO>>> Delete(List<DeleteFileDTO> model)
        {
            if (model.IsNullOrEmpty())
            {
                return new ReturnMessage<List<FileDTO>>(true, null, MessageConstants.Error);
            }

            try
            {
                var userInfo = await _userManager.GetInformationUser();
                if (userInfo.IsNullOrEmpty())
                {
                    return new ReturnMessage<List<FileDTO>>(true, null, MessageConstants.CreateFail);
                }
                var entities = _fileRepository.Queryable().Where(it => model.IndexOf(_mapper.Map<Domain.Entities.File, DeleteFileDTO>(it)) > -1);
                entities.ToList().ForEach(it =>
                {
                    it.Delete(userInfo);
                    it.IsDeleted = true;
                });
                _unitOfWork.BeginTransaction();
                _fileRepository.UpdateRange(entities);
                await _unitOfWork.SaveChangesAsync();
                _unitOfWork.Commit();

                var result = new ReturnMessage<List<FileDTO>>(false, _mapper.Map<List<Domain.Entities.File>, List<FileDTO>>(entities.ToList()), MessageConstants.DeleteSuccess);
                return result;
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                return new ReturnMessage<List<FileDTO>>(true, null, ex.Message);
            }
        }

        public async Task<ReturnMessage<PaginatedList<FileDTO>>> SearchPagination(SearchPaginationDTO<FileDTO> search)
        {
            if (search == null)
            {
                return new ReturnMessage<PaginatedList<FileDTO>>(false, null, MessageConstants.Error);
            }

            var resultEntity = await _fileRepository.GetPaginatedListAsync(it => search.Search == null ||
                (
                    (
                        (search.Search.Id == Guid.Empty ? false : it.Id == search.Search.Id) ||
                        it.Name.Contains(search.Search.Name)
                    //it.Description.Contains(search.Search.Description)
                    )
                )
                , search.PageSize
                , search.PageIndex * search.PageSize
                , t => t.Name
            );
            var data = _mapper.Map<PaginatedList<File>, PaginatedList<FileDTO>>(resultEntity);
            var result = new ReturnMessage<PaginatedList<FileDTO>>(false, data, MessageConstants.SearchSuccess);
            return result;
        }

        public async Task<ReturnMessage<List<FileDTO>>> Update(List<UpdateFileDTO> model)
        {
            if (model.IsNullOrEmpty())
            {
                return new ReturnMessage<List<FileDTO>>(true, null, MessageConstants.Error);
            }

            try
            {
                var userInfo = await _userManager.GetInformationUser();
                if (userInfo.IsNullOrEmpty())
                {
                    return new ReturnMessage<List<FileDTO>>(true, null, MessageConstants.CreateFail);
                }
                var entities = _mapper.Map<List<UpdateFileDTO>, List<Domain.Entities.File>>(model);
                entities.ForEach(it => it.Update(userInfo));
                _unitOfWork.BeginTransaction();
                await _fileRepository.UpdateRangeAsync(entities);
                await _unitOfWork.SaveChangesAsync();
                _unitOfWork.Commit();
                var result = new ReturnMessage<List<FileDTO>>(false, _mapper.Map<List<File>, List<FileDTO>>(entities.ToList()), MessageConstants.UpdateSuccess);
                return result;
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                return new ReturnMessage<List<FileDTO>>(true, null, ex.Message);
            }
        }

        public async Task<ReturnMessage<List<FileDTO>>> UpdateIdFile(List<FileDTO> fileIds, Guid? entityId)
        {
            if (fileIds.IsNullOrEmpty() || entityId.IsNullOrEmpty() || entityId == Guid.Empty)
            {
                return new ReturnMessage<List<FileDTO>>(false, null, MessageConstants.UpdateSuccess);
            }

            var files = new List<File>();

            try
            {
                var userInfo = await _userManager.GetInformationUser();
                if (userInfo.IsNullOrEmpty())
                {
                    return new ReturnMessage<List<FileDTO>>(true, null, MessageConstants.CreateFail);
                }
                foreach (var fileId in fileIds)
                {
                    var item = await _fileRepository.FindAsync(fileId.Id);
                    if (item.IsNotNullOrEmpty())
                    {
                        item.EntityId = entityId.ToString();
                        item.Update(userInfo);
                        files.Add(item);
                    }
                }
                _unitOfWork.BeginTransaction();
                await _fileRepository.UpdateRangeAsync(files);
                await _unitOfWork.SaveChangesAsync();
                _unitOfWork.Commit();

                var result = new ReturnMessage<List<FileDTO>>(false, _mapper.Map<List<Domain.Entities.File>, List<FileDTO>>(files), MessageConstants.UpdateSuccess);
                return result;

            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                return new ReturnMessage<List<FileDTO>>(true, null, ex.Message);
            }
        }
    }
}
