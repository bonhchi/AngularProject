using Common.Http;
using Domain.DTOs.Profiles;
using Domain.DTOs.User;


namespace Service.Profiles
{
    public interface IProfileService
    {
        ReturnMessage<UserDataReturnDTO> Update(UpdateProfileDTO model);
        ReturnMessage<UpdateProfileDTO> ChangePassword(ChangePassworProfileDTO model);

    }
}
