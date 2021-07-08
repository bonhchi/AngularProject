using Common.Http;
using Infrastructure.EntityFramework;
using System;
using Domain.DTOs.InfomationWeb;
using Domain.Entities;
using AutoMapper;
using Common.Constants;
using Infrastructure.Extensions;
using Common.StringEx;
using System.Threading.Tasks;

namespace Service.InformationWebsiteServices
{
    public class InformationWebService : IInfomationWebService
    {
        private readonly IRepositoryAsync<InformationWebsite> _informationWebRepository;
        private readonly IUnitOfWorkAsync _unitOfWork;
        private readonly IMapper _mapper;
        public InformationWebService(IRepositoryAsync<InformationWebsite> informationWebRepository, IMapper mapper, IUnitOfWorkAsync unitOfWork)
        {
            _informationWebRepository = informationWebRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<ReturnMessage<InformationWebDTO>> GetInfo()
        {
            var entity = await _informationWebRepository.FindAsync(CommonConstants.WebSiteInformationId);

            if(entity == null)
            {
                return new ReturnMessage<InformationWebDTO>(true, null, MessageConstants.CommonError);
            }

            var data = _mapper.Map<InformationWebsite, InformationWebDTO>(entity);

            var result = new ReturnMessage<InformationWebDTO>(false, data, MessageConstants.ListSuccess);

            return result;
        }

        public async Task<ReturnMessage<InformationWebDTO>> UpdateAsync(UpdateInformationWebDTO model)
        {
            model.Title = StringExtension.CleanString(model.Title);
            model.Description = StringExtension.CleanString(model.Description);
            model.Email = StringExtension.CleanString(model.Email);
            model.Phone = StringExtension.CleanString(model.Phone);
            model.Fax = StringExtension.CleanString(model.Fax);
            if(model.Title == null||
               model.Description == null ||
               model.Email == null ||
               model.Phone == null ||
               model.Fax == null)
            {
                return new ReturnMessage<InformationWebDTO>(true, null, MessageConstants.UpdateFail);
            }
            try
            {
                var entity = await _informationWebRepository.FindAsync(CommonConstants.WebSiteInformationId);
                if(entity.IsNotNullOrEmpty())
                {
                    entity.Update(model);
                    await _informationWebRepository.UpdateAsync(entity);
                    await _unitOfWork.SaveChangesAsync();

                    var result = new ReturnMessage<InformationWebDTO>(false, _mapper.Map<InformationWebsite, InformationWebDTO>(entity), MessageConstants.UpdateSuccess);
                    return result;
                }
                return new ReturnMessage<InformationWebDTO>(true, null, MessageConstants.Error);
            }
            catch(Exception ex)
            {
                return new ReturnMessage<InformationWebDTO>(true, null, ex.Message);
            }

        }
    }
}
