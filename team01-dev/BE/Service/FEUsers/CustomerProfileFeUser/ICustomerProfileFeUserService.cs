using Common.Http;
using Domain.DTOs.CustomerFE;
using Domain.DTOs.CustomerProfileFeUser;
using System.Threading.Tasks;

namespace Service.CustomerProfileFeUser
{
    public interface ICustomerProfileFeUserService
    {
        Task<ReturnMessage<CustomerDataReturnDTO>> UpdateProfile(UpdateCustomerProfileFeUserDTO model);
        Task<ReturnMessage<CustomerDataReturnDTO>> ChangePassword(ChangePasswordCustomerProfileFeUserDTO model);
    }
}
