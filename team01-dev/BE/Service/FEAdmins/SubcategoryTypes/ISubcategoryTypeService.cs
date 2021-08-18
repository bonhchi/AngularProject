using Common.Http;
using Domain.DTOs.FEAdmins.SubcategoryType;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.FEAdmins.SubcategoryTypes
{
    public interface ISubcategoryTypeService
    {
        Task<ReturnMessage<List<SubcategoryTypeDTO>>> GetSubcategoryType(List<SubcategoryTypeDTO> model);
    }
}
