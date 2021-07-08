using Common.Http;
using Domain.DTOs.CustomerFE;
using System.Threading.Tasks;

namespace Service.AuthCustomer
{
    public interface IAuthCustomerUserService
    {
        Task<ReturnMessage<CustomerDataReturnDTO>> CheckLogin(CustomerLoginDTO data);
        Task<ReturnMessage<CustomerDataReturnDTO>> CheckRegister(CustomerRegisterDTO data);
        Task<ReturnMessage<CustomerDataReturnDTO>> GetCustomerDataReturnDTO();
        Task<ReturnMessage<string>> ForgetPassword(CustomerEmailDTO model);
    }
}
