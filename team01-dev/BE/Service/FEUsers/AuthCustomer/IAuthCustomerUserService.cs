using Common.Http;
using Domain.DTOs.Customer;
using Domain.DTOs.CustomerFE;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Service.AuthCustomer
{
    public interface IAuthCustomerUserService
    {
        ReturnMessage<CustomerDataReturnDTO> CheckLogin(CustomerLoginDTO data);
        ReturnMessage<CustomerDataReturnDTO> CheckRegister(CustomerRegisterDTO data);
        ReturnMessage<CustomerDataReturnDTO> GetCustomerDataReturnDTO();
        ReturnMessage<String> ForgetPassword(CustomerEmailDTO model);
    }
}
