using Common.Http;
using Domain.DTOs.Customer;
using Domain.DTOs.CustomerFE;
using Domain.DTOs.CustomerProfileFeUser;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Service.CustomerProfileFeUser
{
    public interface ICustomerProfileFeUserService
    {
        ReturnMessage<CustomerDataReturnDTO> UpdateProfile(UpdateCustomerProfileFeUserDTO model);
        ReturnMessage<CustomerDataReturnDTO> ChangePassword(ChangePasswordCustomerProfileFeUserDTO model);
    }
}
