using Common.Http;
using Domain.DTOs.CustomerWishList;
using Domain.DTOs.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.CustomerWishLists
{
    public interface ICustomerWishListService
    {
        public ReturnMessage<List<ProductDTO>> GetByCustomer();
        public ReturnMessage<CustomerWishListDTO> CreateOrDelete(CreateOrDeleteCustomerWishListDTO model);
    }
}
