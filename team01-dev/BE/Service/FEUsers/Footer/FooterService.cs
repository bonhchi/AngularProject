using AutoMapper;
using Common.Constants;
using Common.Http;
using Domain.DTOs.Categories;
using Domain.DTOs.FEUsers.Footers;
using Domain.DTOs.InfomationWeb;
using Domain.DTOs.PageContent;
using Domain.DTOs.SocialMedias;
using Domain.Entities;
using Infrastructure.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Footer
{
    public class FooterService : IFooterService
    {
        private readonly IRepository<SocialMedia> _socialMediaRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<PageContent> _pageRepository;
        private readonly IRepository<InformationWebsite> _informationRepository;
        private readonly IMapper _mapper;

        public FooterService(IRepository<SocialMedia> socialMediaRepository, IRepository<Category> categoryRepository, IMapper mapper, IRepository<PageContent> pageRepository, IRepository<InformationWebsite> informationRepository)
        {
            _socialMediaRepository = socialMediaRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _pageRepository = pageRepository;
            _informationRepository = informationRepository;
        }

        public ReturnMessage<FooterDTO> GetFooter()
        {
            try
            {
                FooterDTO footerDTO = new FooterDTO();
                var queryCategories = _categoryRepository.Queryable().Where(it => !it.IsDeleted).OrderBy(it => it.Name).ThenBy(it => it.Name.Length).ToList();
                var categories = _mapper.Map<List<CategoryDTO>>(queryCategories);

                var querySocialMedias = _socialMediaRepository.Queryable().Where(it => !it.IsDeleted).OrderBy(it => it.DisplayOrder).ToList();
                var socialmedias = _mapper.Map<List<SocialMediaDTO>>(querySocialMedias);

                var queryPageContent = _pageRepository.Queryable().Where(it => !it.IsDeleted && it.Order >= -1).ToList();
                var pagecontents = _mapper.Map<List<PageContentDTO>>(queryPageContent);

                var queryInformationWeb = _informationRepository.Find(CommonConstants.WebSiteInformationId);
                var informationWeb = _mapper.Map<InformationWebsite, InformationWebDTO>(queryInformationWeb);

                footerDTO.categories = categories;
                footerDTO.socialMedias = socialmedias;
                footerDTO.pageContents = pagecontents;
                footerDTO.informationWeb = informationWeb;
                return new ReturnMessage<FooterDTO>(false, footerDTO, MessageConstants.ListSuccess);
            }
            catch(Exception ex)
            {
                return new ReturnMessage<FooterDTO>(true, null, ex.Message);
            }
        }
    }
}
