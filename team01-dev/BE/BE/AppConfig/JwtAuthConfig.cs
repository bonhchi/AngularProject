using Infrastructure.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.AppConfig
{
    public static class JwtAuthConfig
    {
        public static void Setup(IServiceCollection services, IConfiguration configuration)
        {
            //jwt
            var jwtConfig = configuration.GetSection("JwtConfig").Get<JwtTokenConfig>();
            services.AddSingleton(jwtConfig); // Dependancy Injection!!
            // Enable Authentication
            services.AddAuthentication(it =>
            {
                // Use JWT Bearer schema
                it.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                it.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(it =>
            {
                // Setup JWT validation options

                var secret = Encoding.ASCII.GetBytes(jwtConfig.Secret); // Keys use to validate signature

                it.RequireHttpsMetadata = true; // Not mandatory => may be skip
                it.SaveToken = true; // Not mandatory => may be skip

                // Token validation parameters
                it.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true, // Require validate issuer, if true then mis specify ValidIssuer
                    ValidIssuer = jwtConfig.Issuer, // Issuer of token

                    ValidateIssuerSigningKey = true, // Verify signature, if false then no need Secret Key to verify token
                    IssuerSigningKey = new SymmetricSecurityKey(secret),

                    ValidateAudience = true, // Validate audience, if true, must specify ValidAudience
                    ValidAudience = jwtConfig.Audience,

                    ValidateLifetime = false, // Validate JWT lifetime, if false will not check nbf and exp

                    //ClockSkew = TimeSpan.FromMinutes(1) // Tolerance to validate JWT lifetime
                };
            });
        }
    }
}

