using Common.Http;
using Common.Pagination;
using Domain.DTOs.FEAdmins.Subcategory;
using Infrastructure.EntityFramework;
using Service.Common;
using System.Threading.Tasks;

namespace Service.FEAdmins.Subcategories
{
    public interface ISubcategoryService : ICommonCRUDService<SubcategoryDTO, CreateSubcategoryDTO, UpdateSubcategoryDTO, DeleteSubcategoryDTO>
    {
        Task<ReturnMessage<PaginatedList<SubcategoryDTO>>> SearchPaginationAsync(SearchPaginationDTO<SubcategoryDTO> search);
    }
}
