using Common.Http;
using Domain.DTOs.ProductsFeUser;
using Service.Common;

namespace Service.ProductDetailsFeUser
{
    public interface IProductDetailsFeService : ICommonCRUDService<ProductDTOFeUser>
    {
        ReturnMessage<ProductDTOFeUser> GetDetails(ProductDTOFeUser search);
    }
}
