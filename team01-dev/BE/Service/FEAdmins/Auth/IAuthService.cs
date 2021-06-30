using Common.Http;
using Domain.DTOs.User;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Service.Auth
{
    // async method
    public interface IAuthService
    {
        ReturnMessage<UserDataReturnDTO> CheckLogin(UserLoginDTO data);
        ReturnMessage<UserDataReturnDTO> GetInformationUser();
    }
}
