using AutoMapper;
using Common.Constants;
using Common.Http;
using Common.Pagination;
using Domain.DTOs.Blogs;
using Domain.Entities;
using Infrastructure.EntityFramework;
using Infrastructure.Extensions;
using System;
using Common.StringEx;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Blogs
{
    public class BlogService : IBlogService
    {
        private readonly IRepositoryAsync<Blog> _blogRepository;
        private readonly IUnitOfWorkAsync _unitOfWork;
        private readonly IMapper _mapper;
        public BlogService(IRepositoryAsync<Blog> blogRepository, IUnitOfWorkAsync unitOfWork, IMapper mapper)
        {
            _blogRepository = blogRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ReturnMessage<BlogDTO>> CreateAsync(CreateBlogDTO model)
        {
            model.Title = StringExtension.CleanString(model.Title);
            model.ShortDes = StringExtension.CleanString(model.ShortDes);
            model.ContentHTML = StringExtension.CleanString(model.ContentHTML);
            if (model.Title == null ||
               model.ShortDes == null ||
               model.ContentHTML == null)
            {
                var entity = _mapper.Map<CreateBlogDTO, Blog>(model);
                return new ReturnMessage<BlogDTO>(true, _mapper.Map<Blog, BlogDTO>(entity), MessageConstants.InvalidString);
            }
            try
            {
                var entity = _mapper.Map<CreateBlogDTO, Blog>(model);
                entity.CreatedByName = CommonConstantsBlog.CreateByName; //name depends on user who logged in with role
                entity.Insert();
                _blogRepository.InsertAsync(entity);
                await _unitOfWork.SaveChangesAsync();
                var result = new ReturnMessage<BlogDTO>(false, _mapper.Map<Blog, BlogDTO>(entity), MessageConstants.CreateSuccess);
                return result;
            }
            catch (Exception ex)
            {
                return new ReturnMessage<BlogDTO>(true, null, ex.Message);
            }
        }

        public async Task<ReturnMessage<BlogDTO>> DeleteAsync(DeleteBlogDTO model)
        {
            try
            {
                var entity = await _blogRepository.FindAsync(model.Id);
                if (entity.IsNotNullOrEmpty())
                {
                    entity.Delete();
                    _blogRepository.Delete(entity);
                    await _unitOfWork.SaveChangesAsync();
                    var result = new ReturnMessage<BlogDTO>(false, _mapper.Map<Blog, BlogDTO>(entity), MessageConstants.DeleteSuccess);
                    return result;
                }
                return new ReturnMessage<BlogDTO>(true, null, MessageConstants.Error);
            }
            catch (Exception ex)
            {
                return new ReturnMessage<BlogDTO>(true, null, ex.Message);
            }
        }

        public async Task<ReturnMessage<BlogDTO>> UpdateAsync(UpdateBlogDTO model)
        {
            model.Title = StringExtension.CleanString(model.Title);
            model.ShortDes = StringExtension.CleanString(model.ShortDes);
            model.ContentHTML = StringExtension.CleanString(model.ContentHTML);
            if (model.Title == null ||
               model.ShortDes == null ||
               model.ContentHTML == null)
            {
                var entity = _mapper.Map<UpdateBlogDTO, Blog>(model);
                return new ReturnMessage<BlogDTO>(true, _mapper.Map<Blog, BlogDTO>(entity), MessageConstants.UpdateFail);
            }
            try
            {
                var entity = await _blogRepository.FindAsync(model.Id);
                if (entity.IsNotNullOrEmpty())
                {
                    entity.Update(model);
                    await _blogRepository.UpdateAsync(entity);
                    await _unitOfWork.SaveChangesAsync();
                    var result = new ReturnMessage<BlogDTO>(false, _mapper.Map<Blog, BlogDTO>(entity), MessageConstants.UpdateSuccess);
                    return result;

                }
                return new ReturnMessage<BlogDTO>(true, null, MessageConstants.Error);
            }
            catch (Exception ex)
            {
                return new ReturnMessage<BlogDTO>(true, null, ex.Message);
            }
        }

        public async Task<ReturnMessage<PaginatedList<BlogDTO>>> SearchPaginationAsync(SearchPaginationDTO<BlogDTO> search)
        {
            if (search == null)
            {
                return new ReturnMessage<PaginatedList<BlogDTO>>(false, null, MessageConstants.GetPaginationFail);
            }

            var query = _blogRepository.Queryable().Where(it => search.Search == null ||
  
                    (
                        (search.Search.Id != Guid.Empty && it.Id == search.Search.Id) ||
                        it.Title.Contains(search.Search.Title) ||
                        it.ShortDes.Contains(search.Search.ShortDes) ||
                        it.ContentHTML.Contains(search.Search.ContentHTML) ||
                        it.ImageUrl.Contains(search.Search.ImageUrl)
                    )
                 && !it.IsDeleted).OrderBy(it => it.Title).ThenBy(it => it.Title.Length);

            var resultEntity = new PaginatedList<Blog>(query, search.PageIndex * search.PageSize, search.PageSize);
            var data = _mapper.Map<PaginatedList<Blog>, PaginatedList<BlogDTO>>(resultEntity);
            var result = new ReturnMessage<PaginatedList<BlogDTO>>(false, data, MessageConstants.GetPaginationSuccess);
            await Task.CompletedTask;
            return result;
        }
    }
}
