using AutoMapper;
using Common.Constants;
using Common.Http;
using Common.Pagination;
using Common.StringEx;
using Domain.DTOs.FEAdmins.Subcategory;
using Domain.Entities;
using Infrastructure.EntityFramework;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Service.FEAdmins.Subcategories
{
    public class SubcategoryService : ISubcategoryService
    {
        private readonly IRepositoryAsync<Subcategory> _subcategoryRepository;
        private readonly IRepositoryAsync<Category> _categoryRepository;
        private readonly IRepositoryAsync<SubcategoryType> _subcategoryTypeRepository;
        private readonly IUnitOfWorkAsync _unitOfWork;
        private readonly IMapper _mapper;

        public SubcategoryService(IRepositoryAsync<Subcategory> subcategoryRepository, IRepositoryAsync<Category> categoryRepository, IRepositoryAsync<SubcategoryType> subcategoryTypeRepository, IUnitOfWorkAsync unitOfWork, IMapper mapper)
        {
            _subcategoryRepository = subcategoryRepository;
            _categoryRepository = categoryRepository;
            _subcategoryTypeRepository = subcategoryTypeRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ReturnMessage<SubcategoryDTO>> CreateAsync(CreateSubcategoryDTO model)
        {
            model.Name = StringExtension.CleanString(model.Name);
            if(model.Name == null)
            {
                return new ReturnMessage<SubcategoryDTO>(true, null, MessageConstants.InvalidString);
            }
            try
            {
                var entity = _mapper.Map<CreateSubcategoryDTO, Subcategory>(model);
                var category = await _categoryRepository.FindAsync(model.CategoryId);
                var subcategoryType = await _subcategoryTypeRepository.FindAsync(model.SubcategoryTypeId);
                if (category == null || subcategoryType == null)
                {
                    return new ReturnMessage<SubcategoryDTO>(true, null, MessageConstants.Error);
                }
                entity.CategoryId = category.Id;
                entity.Category = category;
                entity.SubcategoryTypeId = subcategoryType.Id;
                entity.SubcategoryType = subcategoryType;
                entity.Insert();
                _subcategoryRepository.InsertAsync(entity);
                await _unitOfWork.SaveChangesAsync();
                var result = new ReturnMessage<SubcategoryDTO>(false, _mapper.Map<Subcategory, SubcategoryDTO>(entity), MessageConstants.CreateSuccess);
                return result;
            }
            catch (Exception ex)
            {
                return new ReturnMessage<SubcategoryDTO>(true, null, ex.Message);
            }
        }

        public async Task<ReturnMessage<SubcategoryDTO>> DeleteAsync(DeleteSubcategoryDTO model)
        {
            try
            {
                var entity = await _subcategoryRepository.FindAsync(model.Id);
                if (entity.IsNotNullOrEmpty())
                {
                    entity.Delete();
                    await _subcategoryRepository.UpdateAsync(entity);
                    await _unitOfWork.SaveChangesAsync();
                    var result = new ReturnMessage<SubcategoryDTO>(false, _mapper.Map<Subcategory, SubcategoryDTO>(entity), MessageConstants.DeleteSuccess);
                    return result;
                }
                return new ReturnMessage<SubcategoryDTO>(true, null, MessageConstants.Error);
            }
            catch (Exception ex)
            {
                return new ReturnMessage<SubcategoryDTO>(true, null, ex.Message);
            }
        }

        public async Task<ReturnMessage<SubcategoryDTO>> UpdateAsync(UpdateSubcategoryDTO model)
        {
            model.Name = StringExtension.CleanString(model.Name);
            var entity = _mapper.Map<UpdateSubcategoryDTO, Subcategory>(model);
            if (model.Name == null)
            {
                return new ReturnMessage<SubcategoryDTO>(true, _mapper.Map<Subcategory,SubcategoryDTO>(entity), MessageConstants.InvalidString);
            }
            try
            {
                var category = await _categoryRepository.FindAsync(model.CategoryId);
                var subcategoryType = await _subcategoryTypeRepository.FindAsync(model.SubcategoryTypeId);
                if (category == null || subcategoryType == null)
                {
                    return new ReturnMessage<SubcategoryDTO>(true, null, MessageConstants.Error);
                }
                entity.CategoryId = category.Id;
                entity.Category = category;
                entity.SubcategoryTypeId = subcategoryType.Id;
                entity.SubcategoryType = subcategoryType;
                entity.Update();
                await _subcategoryRepository.UpdateAsync(entity);
                await _unitOfWork.SaveChangesAsync();
                var result = new ReturnMessage<SubcategoryDTO>(false, _mapper.Map<Subcategory, SubcategoryDTO>(entity), MessageConstants.UpdateSuccess);
                return result;
            }
            catch (Exception ex)
            {
                return new ReturnMessage<SubcategoryDTO>(true, null, ex.Message);
            }
        }

        public async Task<ReturnMessage<PaginatedList<SubcategoryDTO>>> SearchPaginationAsync(SearchPaginationDTO<SubcategoryDTO> search)
        {
            if (search == null)
            {
                return new ReturnMessage<PaginatedList<SubcategoryDTO>>(false, null, MessageConstants.Error);
            }
            var query = _subcategoryRepository.Queryable().Include(it => it.Category).Where(it => (search.Search == null ||
                            (search.Search.Id != Guid.Empty && it.Id == search.Search.Id) ||
                            it.Name.Contains(search.Search.Name)
                    ) && !it.IsDeleted
                )
                .OrderBy(it => it.Name)
                .ThenBy(it => it.Name.Length);
            var resultEntity = new PaginatedList<Subcategory>(query, search.PageIndex * search.PageSize, search.PageSize);
            var data = _mapper.Map<PaginatedList<Subcategory>, PaginatedList<SubcategoryDTO>>(resultEntity);
            var result = new ReturnMessage<PaginatedList<SubcategoryDTO>>(false, data, MessageConstants.ListSuccess);
            await Task.CompletedTask;
            return result;
        }
    }
}
