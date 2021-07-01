using Common.Http;
using Domain.DTOs.Profiles;
using Domain.DTOs.User;
using System.Threading.Tasks;

namespace Service.Profiles
{
    public interface IProfileService
    {
        Task<ReturnMessage<UserDataReturnDTO>> UpdateAsync(UpdateProfileDTO model);
        ReturnMessage<UpdateProfileDTO> ChangePassword(ChangePassworProfileDTO model);

    }
}
