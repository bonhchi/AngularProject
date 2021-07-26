using Common.Http;
using Domain.DTOs.CustomerWishList;
using Domain.DTOs.Products;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.CustomerWishLists
{
    public interface ICustomerWishListService
    {
        Task<ReturnMessage<List<SubcategoryDTO>>> GetByCustomer();
        Task<ReturnMessage<CustomerWishListDTO>> CreateOrDelete(CreateOrDeleteCustomerWishListDTO model);
    }
}
