using AutoMapper;
using Common.Constants;
using Common.Http;
using Common.Pagination;
using Domain.DTOs.Categories;
using Domain.Entities;
using Infrastructure.EntityFramework;
using Infrastructure.Extensions;
using System;
using System.Linq;
using Common.StringEx;
using System.Threading.Tasks;

namespace Service.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepositoryAsync<Product> _productRepository;
        private readonly IRepositoryAsync<Category> _categoryRepository;
        private readonly IUnitOfWorkAsync _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IRepositoryAsync<Category> categoryRepository, IRepositoryAsync<Product> productRepository, IUnitOfWorkAsync unitOfWork, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<ReturnMessage<CategoryDTO>> CreateAsync(CreateCategoryDTO model)
        {
            model.Name = StringExtension.CleanString(model.Name);
            model.Description = StringExtension.CleanString(model.Description);
            if (model.Name == null ||
               model.Description == null)
            {
                var entity = _mapper.Map<CreateCategoryDTO, Category>(model);
                return new ReturnMessage<CategoryDTO>(true, _mapper.Map<Category, CategoryDTO>(entity), MessageConstants.InvalidString);
            }
            try
            {
                var entity = _mapper.Map<CreateCategoryDTO, Category>(model);
                entity.Insert();
                _categoryRepository.InsertAsync(entity);
                await _unitOfWork.SaveChangesAsync();
                var result = new ReturnMessage<CategoryDTO>(false, _mapper.Map<Category, CategoryDTO>(entity), MessageConstants.CreateSuccess);
                return result;
            }
            catch (Exception ex)
            {
                return new ReturnMessage<CategoryDTO>(true, null, ex.Message);
            }
        }

        public async Task<ReturnMessage<CategoryDTO>> DeleteAsync(DeleteCategoryDTO model)
        {
            try
            {
                var entity = await _categoryRepository.FindAsync(model.Id);
               
                var products = _productRepository.Queryable().Where(r => r.CategoryId == model.Id);

                if (entity.IsNotNullOrEmpty())
                {
                    foreach (var product in products)
                    {
                        product.Delete();
                        _productRepository.Update(product);
                    }
                    entity.Delete();
                    await _categoryRepository.UpdateAsync(entity);
                    await _unitOfWork.SaveChangesAsync();
                    var result = new ReturnMessage<CategoryDTO>(false, _mapper.Map<Category, CategoryDTO>(entity), MessageConstants.DeleteSuccess);
                    return result;
                }
                return new ReturnMessage<CategoryDTO>(true, null, MessageConstants.Error);
            }
            catch (Exception ex)
            {
                return new ReturnMessage<CategoryDTO>(true, null, ex.Message);
            }
        }

        public async Task<ReturnMessage<PaginatedList<CategoryDTO>>> SearchPaginationAsync(SearchPaginationDTO <CategoryDTO> search)
        {
            if (search == null)
            {
                return new ReturnMessage<PaginatedList<CategoryDTO>>(false, null, MessageConstants.Error);
            }

            var query = _categoryRepository.Queryable().Where(it => (search.Search == null ||
                
                    
                        (search.Search.Id == Guid.Empty ? false : it.Id == search.Search.Id) ||
                        it.Name.Contains(search.Search.Name) ||
                        it.Description.Contains(search.Search.Description)
                    

                ) && !it.IsDeleted)
                .OrderBy(it => it.Name)
                .ThenBy(it => it.Name.Length);
            var resultEntity = new PaginatedList<Category>(query, search.PageIndex * search.PageSize, search.PageSize);
            var data = _mapper.Map<PaginatedList<Category>, PaginatedList<CategoryDTO>>(resultEntity);
            var result = new ReturnMessage<PaginatedList<CategoryDTO>>(false, data, MessageConstants.ListSuccess);
            await Task.CompletedTask;
            return result;
        }
    

        public async Task<ReturnMessage<CategoryDTO>> UpdateAsync(UpdateCategoryDTO model)
        {
            model.Name = StringExtension.CleanString(model.Name);
            model.Description = StringExtension.CleanString(model.Description);
            if (model.Name == null ||
               model.Description == null)
            {
                var entity = _mapper.Map<UpdateCategoryDTO, Category>(model);
                return new ReturnMessage<CategoryDTO>(true, _mapper.Map<Category, CategoryDTO>(entity), MessageConstants.UpdateFail);
            }

            try
            {
                var entity = _categoryRepository.Find(model.Id);

                if (entity.IsNotNullOrEmpty())
                {
                    entity.Update(model);
                    await _categoryRepository.UpdateAsync(entity);
                    await _unitOfWork.SaveChangesAsync();
                    var result = new ReturnMessage<CategoryDTO>(false, _mapper.Map<Category, CategoryDTO>(entity), MessageConstants.UpdateSuccess);
                    return result;
                }
                return new ReturnMessage<CategoryDTO>(true, null, MessageConstants.Error);
            }
            catch (Exception ex)
            {
                return new ReturnMessage<CategoryDTO>(true, null, ex.Message);
            }
        }
    }
}
