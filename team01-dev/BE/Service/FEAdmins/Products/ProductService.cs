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

namespace Service.Products
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductService(IRepository<Category> categoryRepository, IRepository<Product> productRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public ReturnMessage<ProductDTO> Create(CreateProductDTO model)
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
                var category = _categoryRepository.Find(model.CategoryId);
                if (category == null)
                {
                    return new ReturnMessage<ProductDTO>(true, null, MessageConstants.Error);
                }
                entity.CategoryId = category.Id;
                entity.Category = category;
                entity.Insert();
                _productRepository.Insert(entity);
                _unitOfWork.SaveChanges();
                var result = new ReturnMessage<ProductDTO>(false, _mapper.Map<Product, ProductDTO>(entity), MessageConstants.CreateSuccess);
                return result;
            }
            catch (Exception ex)
            {
                return new ReturnMessage<ProductDTO>(true, null, ex.Message);
            }
        }

        public ReturnMessage<ProductDTO> Delete(DeleteProductDTO model)
        {
            try
            {
                var entity = _productRepository.Find(model.Id);
                if (entity.IsNotNullOrEmpty())
                {
                    entity.Delete();
                    _productRepository.Update(entity);
                    _unitOfWork.SaveChanges();
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

        public ReturnMessage<List<ProductDTO>> GetByCategory(Guid id)
        {
            try
            {
                var listDTO = _productRepository.Queryable().Where(product => product.CategoryId == id).ToList();
                var list = _mapper.Map<List<ProductDTO>>(listDTO);
                var result = new ReturnMessage<List<ProductDTO>>(false, list, MessageConstants.ListSuccess);
                return result;
            }

            catch (Exception ex)
            {
                return new ReturnMessage<List<ProductDTO>>(true, null, ex.Message);
            }
        }

        public ReturnMessage<PaginatedList<ProductDTO>> SearchPagination(SearchPaginationDTO<ProductDTO> search)
        {
            if (search == null)
            {
                return new ReturnMessage<PaginatedList<ProductDTO>>(false, null, MessageConstants.Error);
            }
            var query = _productRepository.Queryable().Include(it => it.Category).Where(it => (search.Search == null ||
                    (
                        (
                            (search.Search.Id == Guid.Empty ? false : it.Id == search.Search.Id) ||
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

            return result;
        }

        public ReturnMessage<ProductDTO> Update(UpdateProductDTO model)
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
                var category = _categoryRepository.Find(model.CategoryId);
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
                    _productRepository.Update(entity);
                    _unitOfWork.SaveChanges();
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

        public ReturnMessage<ProductDTO> GetById(Guid id)
        {
            try
            {
                var search = _productRepository.Queryable().AsNoTracking().FirstOrDefault(product => product.Id == id);
                if (search.IsNotNullOrEmpty() && (search.IsDeleted == false))
                {

                    var category = _categoryRepository.Find(search.CategoryId);
                    if (category == null)
                    {
                        return new ReturnMessage<ProductDTO>(true, null, MessageConstants.Error);
                    }

                    var result = new ReturnMessage<ProductDTO>(false, _mapper.Map<ProductDTO>(search), MessageConstants.ListSuccess);
                    return result;
                }
                return new ReturnMessage<ProductDTO>(true, null, MessageConstants.Error);

            }

            catch (Exception ex)
            {
                return new ReturnMessage<ProductDTO>(true, null, ex.Message);
            }
        }

        public ReturnMessage<UpdateProductDTO> UpdateCount(UpdateProductDTO product, int quantity)
        {
            try
            {

                product.SaleCount += quantity;
                var entity = _mapper.Map<Product>(product);

                _productRepository.Update(entity);
                _unitOfWork.SaveChanges();
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
