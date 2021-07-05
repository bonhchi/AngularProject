using Domain.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Service.Auth
{
    public interface IUserManager
    {
        Task<string> GenerateToken(IEnumerable<Claim> claims, DateTime now);
        Task<UserInformationDTO> GetInformationUser();
        Guid AuthorizedUserId { get; }
    }
}
