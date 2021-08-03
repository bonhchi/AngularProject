using AutoMapper;
using Common.Constants;
using Common.Http;
using Common.Pagination;
using Common.StringEx;
using Domain.DTOs.Products;
using Domain.Entities;
using Infrastructure.EntityFramework;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Products
{
    public class ProductService : IProductService
    {
        private readonly IRepositoryAsync<Product> _productRepository;
        private readonly IRepositoryAsync<Category> _categoryRepository;
        private readonly IUnitOfWorkAsync _unitOfWork;
        private readonly IMapper _mapper;
        public ProductService(IRepositoryAsync<Category> categoryRepository, IRepositoryAsync<Product> productRepository, IUnitOfWorkAsync unitOfWork, IMapper mapper)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public async Task<ReturnMessage<ProductDTO>> CreateAsync(CreateProductDTO model)
        {
            model.Name = StringExtension.CleanString(model.Name);
            model.Description = StringExtension.CleanString(model.Description);
            if (model.Name == null ||
               model.Description == null)
            {
                var entity = _mapper.Map<CreateProductDTO, Product>(model);
                return new ReturnMessage<ProductDTO>(true, _mapper.Map<Product, ProductDTO>(entity), MessageConstants.InvalidString);
            }

            try
            {
                var entity = _mapper.Map<CreateProductDTO, Product>(model);
                var category = await _categoryRepository.FindAsync(model.CategoryId);
                if (category == null)
                {
                    return new ReturnMessage<ProductDTO>(true, null, MessageConstants.Error);
                }
                entity.CategoryId = category.Id;
                entity.Category = category; //checking error
                entity.Insert();
                _productRepository.InsertAsync(entity);
                await _unitOfWork.SaveChangesAsync();
                var result = new ReturnMessage<ProductDTO>(false, _mapper.Map<Product, ProductDTO>(entity), MessageConstants.CreateSuccess);
                return result;
            }
            catch (Exception ex)
            {
                return new ReturnMessage<ProductDTO>(true, null, ex.Message);
            }
        }

        public async Task<ReturnMessage<ProductDTO>> DeleteAsync(DeleteProductDTO model)
        {
            try
            {
                var entity = await _productRepository.FindAsync(model.Id);
                if (entity.IsNotNullOrEmpty())
                {
                    entity.Delete();
                    await _productRepository.UpdateAsync(entity);
                    await _unitOfWork.SaveChangesAsync();
                    var result = new ReturnMessage<ProductDTO>(false, _mapper.Map<Product, ProductDTO>(entity), MessageConstants.DeleteSuccess);
                    return result;
                }
                return new ReturnMessage<ProductDTO>(true, null, MessageConstants.Error);
            }
            catch (Exception ex)
            {
                return new ReturnMessage<ProductDTO>(true, null, ex.Message);
            }
        }

        public async Task<ReturnMessage<List<ProductDTO>>> GetByCategory(Guid id)
        {
            try
            {
                var listDTO = _productRepository.Queryable().Where(product => product.CategoryId == id).ToList();
                var list = _mapper.Map<List<ProductDTO>>(listDTO);
                var result = new ReturnMessage<List<ProductDTO>>(false, list, MessageConstants.ListSuccess);
                await Task.CompletedTask;
                return result;
            }

            catch (Exception ex)
            {
                return new ReturnMessage<List<ProductDTO>>(true, null, ex.Message);
            }
        }

        public async Task<ReturnMessage<PaginatedList<ProductDTO>>> SearchPaginationAsync(SearchPaginationDTO<ProductDTO> search)
        {
            if (search == null)
            {
                return new ReturnMessage<PaginatedList<ProductDTO>>(false, null, MessageConstants.Error);
            }
            var query = _productRepository.Queryable().Include(it => it.Category).Where(it => (search.Search == null ||
                    (
                        (
                            (search.Search.Id != Guid.Empty && it.Id == search.Search.Id) ||
                            it.Name.Contains(search.Search.Name) ||
                            it.Description.Contains(search.Search.Description)

                        )
                    )) && !it.IsDeleted
                )
                .OrderBy(it => it.Name)
                .ThenBy(it => it.Name.Length);
            var resultEntity = new PaginatedList<Product>(query, search.PageIndex * search.PageSize, search.PageSize);
            var data = _mapper.Map<PaginatedList<Product>, PaginatedList<ProductDTO>>(resultEntity);
            var result = new ReturnMessage<PaginatedList<ProductDTO>>(false, data, MessageConstants.ListSuccess);

            await Task.CompletedTask;
            return result;
        }

        public async Task<ReturnMessage<ProductDTO>> UpdateAsync(UpdateProductDTO model)
        {
            model.Name = StringExtension.CleanString(model.Name);
            model.Description = StringExtension.CleanString(model.Description);
            if (model.Name == null ||
               model.Description == null)
            {
                var entity = _mapper.Map<UpdateProductDTO, Product>(model);
                return new ReturnMessage<ProductDTO>(true, _mapper.Map<Product, ProductDTO>(entity), MessageConstants.InvalidString);
            }
            try
            {
                var entity = _mapper.Map<UpdateProductDTO, Product>(model);
                var category = await _categoryRepository.FindAsync(model.CategoryId);
                if (category == null)
                {
                    return new ReturnMessage<ProductDTO>(true, null, MessageConstants.Error);
                }
                entity.Category = category;
                entity.CategoryId = category.Id;
                entity.Update();
                if (entity.IsNotNullOrEmpty())
                {
                    entity.Update(model);
                    await _productRepository.UpdateAsync(entity);
                    await _unitOfWork.SaveChangesAsync();
                    var result = new ReturnMessage<ProductDTO>(false, _mapper.Map<Product, ProductDTO>(entity), MessageConstants.UpdateSuccess);
                    return result;
                }
                return new ReturnMessage<ProductDTO>(true, null, MessageConstants.Error);
            }
            catch (Exception ex)
            {
                return new ReturnMessage<ProductDTO>(true, null, ex.Message);
            }
        }

        public async Task<ReturnMessage<ProductDTO>> GetById(Guid id)
        {
            try
            {
                var search = _productRepository.Queryable().AsNoTracking().FirstOrDefault(product => product.Id == id);
                if (search.IsNotNullOrEmpty() && (search.IsDeleted == false))
                {

                    var category = await _categoryRepository.FindAsync(search.CategoryId);
                    if (category == null)
                    {
                        return new ReturnMessage<ProductDTO>(true, null, MessageConstants.Error);
                    }

                    var result = new ReturnMessage<ProductDTO>(false, _mapper.Map<ProductDTO>(search), MessageConstants.ListSuccess);
                    return result;
                }

                await Task.CompletedTask;
                return new ReturnMessage<ProductDTO>(true, null, MessageConstants.Error);

            }

            catch (Exception ex)
            {
                return new ReturnMessage<ProductDTO>(true, null, ex.Message);
            }
        }

        public async Task<ReturnMessage<UpdateProductDTO>> UpdateCount(UpdateProductDTO product, int quantity)
        {
            try
            {

                product.SaleCount += quantity;
                var entity = _mapper.Map<Product>(product);

                await _productRepository.UpdateAsync(entity);
                await _unitOfWork.SaveChangesAsync();
                var result = new ReturnMessage<UpdateProductDTO>(false, product, MessageConstants.UpdateSuccess);
                return result;
                
            }

            catch (Exception ex)
            {
                return new ReturnMessage<UpdateProductDTO>(true, null, ex.Message);
            }
        }
    }
}
