using AutoMapper;
using Domain.DTOs.User;
using Domain.DTOs.Users;
using Domain.Entities;
using Infrastructure.Auth;
using Infrastructure.EntityFramework;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Service.Auth
{
    public class UserManager : IUserManager
    {
        private IRepository<User> _userRepository;
        private IMapper _mapper;
        private readonly JwtTokenConfig _jwtTokenConfig;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly byte[] _secret;

        public UserManager(JwtTokenConfig jwtTokenConfig, IMapper mapper, IRepository<Domain.Entities.User> repository, IHttpContextAccessor httpContextAccessor)
        {
            _jwtTokenConfig = jwtTokenConfig;
            _secret = Encoding.ASCII.GetBytes(jwtTokenConfig.Secret); // Secret key
            _mapper = mapper;
            _userRepository = repository;
            _httpContextAccessor = httpContextAccessor;
        }

        public string GenerateToken(IEnumerable<Claim> claims, DateTime now)
        {
            // Setup JWT generate parameters
            var jwtToken = new JwtSecurityToken(
                issuer: _jwtTokenConfig.Issuer,
                audience: _jwtTokenConfig.Audience,
                claims: claims,
                notBefore: now,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(_secret), SecurityAlgorithms.HmacSha256Signature));
            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return accessToken;
        }

        public Guid AuthorizedUserId
        {
            get
            {
                try
                {
                    var claims = _httpContextAccessor.HttpContext.User.Claims;
                    return Guid.Parse(claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value);
                }
                catch
                {
                    return Guid.Empty;
                }
            }
        }

        public UserDataReturnDTO GetInformationAuth(Guid userId)
        {
            try
            {
                var data = _userRepository.Find(userId);
                var result = _mapper.Map<Domain.Entities.User, UserDataReturnDTO>(data);
                return result;
            }
            catch (Exception ex)
            {
                return new UserDataReturnDTO();
            }
        }

        public UserInformationDTO GetInformationUser()
        {
            try
            {
                var user = _userRepository.Queryable().Include(it => it.Customer).Where(it => !it.IsDeleted && it.Id == AuthorizedUserId).FirstOrDefault();
                if(user.IsNullOrEmpty())
                {
                    return new UserInformationDTO();
                }
                var result = _mapper.Map<User, UserInformationDTO>(user);
                return result;
            }
            catch
            {
                return new UserInformationDTO();
            }
        }
    }
}
