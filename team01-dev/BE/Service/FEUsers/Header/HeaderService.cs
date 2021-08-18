using AutoMapper;
using Common.Constants;
using Common.Http;
using Domain.DTOs.Blogs;
using Domain.DTOs.Categories;
using Domain.DTOs.FEUsers.Headers;
using Domain.DTOs.InfomationWeb;
using Domain.Entities;
using Infrastructure.EntityFramework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Header
{
    public class HeaderService : IHeaderService
    {
        private readonly IRepositoryAsync<Category> _categoryRepository;
        private readonly IRepositoryAsync<Blog> _blogRepository;
        private readonly IRepositoryAsync<InformationWebsite> _webInfoRepository;
        private readonly IMapper _mapper;

        public HeaderService(IRepositoryAsync<InformationWebsite> webInfoRepository, IRepositoryAsync<Category> categoryRepository, IMapper mapper, IRepositoryAsync<Blog> blogRepository)
        {
            _categoryRepository = categoryRepository;
            _blogRepository = blogRepository;
            _mapper = mapper;
            _webInfoRepository = webInfoRepository;
        }

        public async Task<ReturnMessage<HeaderDTO>> GetHeader()
        {
            HeaderDTO headerDTO = new();
            var blogQueries = _blogRepository.Queryable().Where(it => !it.IsDeleted).OrderByDescending(it => it.CreateByDate).ThenBy(it => it.Title).ThenBy(it => it.Title.Length).Take(5);
            var blog = _mapper.Map<List<BlogDTO>>(blogQueries);

            var categoryQueries = _categoryRepository.Queryable().Where(it => !it.IsDeleted).OrderBy(it => it.Name).ThenBy(it => it.Name.Length);
            var category = _mapper.Map<List<CategoryDTO>>(categoryQueries);

            var webInfoQueries = _webInfoRepository.Find(CommonConstants.WebSiteInformationId);
            var webInfo = _mapper.Map<InformationWebDTO>(webInfoQueries);
            headerDTO.Blogs = blog;
            headerDTO.Categories = category;
            headerDTO.InformationWeb = webInfo;

            var result = new ReturnMessage<HeaderDTO>(false, headerDTO, MessageConstants.ListSuccess);

            await Task.CompletedTask;
            return result;
        }

    }
}
