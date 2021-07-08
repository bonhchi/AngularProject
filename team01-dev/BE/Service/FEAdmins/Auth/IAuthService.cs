using Common.Http;
using Domain.DTOs.User;
using System.Threading.Tasks;

namespace Service.Auth
{
    public interface IAuthService
    {
        Task<ReturnMessage<UserDataReturnDTO>> CheckLogin(UserLoginDTO data);
        Task<ReturnMessage<UserDataReturnDTO>> GetInformationUser();
    }
}
