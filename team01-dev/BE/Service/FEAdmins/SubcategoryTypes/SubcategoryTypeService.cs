using AutoMapper;
using Common.Constants;
using Common.Http;
using Domain.DTOs.Blogs;
using Domain.DTOs.FEAdmins.SubcategoryType;
using Domain.Entities;
using Infrastructure.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.FEAdmins.SubcategoryTypes
{
    public class SubcategoryTypeService : ISubcategoryTypeService
    {
        private readonly IRepositoryAsync<SubcategoryType> _subcategoryTypeRepository;
        private readonly IMapper _mapper;

        public SubcategoryTypeService(IRepositoryAsync<SubcategoryType> subcategoryTypeRepository, IMapper mapper)
        {
            _subcategoryTypeRepository = subcategoryTypeRepository;
            _mapper = mapper;
        }

        public async Task<ReturnMessage<List<SubcategoryTypeDTO>>> GetSubcategoryType(List<SubcategoryTypeDTO> model)
        {
            if (model == null)
            {
                return new ReturnMessage<List<SubcategoryTypeDTO>>(false, null, MessageConstants.DataError);
            }
            var entity = _subcategoryTypeRepository.Queryable().ToList();
            var data = _mapper.Map<List<SubcategoryType>, List<SubcategoryTypeDTO>>(entity);
            var result = new ReturnMessage<List<SubcategoryTypeDTO>>(false, data, MessageConstants.GetSuccess);

            await Task.CompletedTask;
            return result;
        }
    }
}
