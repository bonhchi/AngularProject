using Common.Http;
using Domain.DTOs.ProductsFeUser;
using Service.Common;
using System.Threading.Tasks;

namespace Service.ProductDetailsFeUser
{
    public interface IProductDetailsFeService : ICommonCRUDService<ProductDTOFeUser>
    {
        Task<ReturnMessage<ProductDTOFeUser>> GetDetails(ProductDTOFeUser search);
    }
}
