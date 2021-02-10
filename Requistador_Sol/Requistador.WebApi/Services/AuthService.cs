using Microsoft.IdentityModel.Tokens;
using Requistador.Identity.Dtos;
using Requistador.WebApi.AppConfiguration;
using Requistador.WebApi.AppConfiguration.Settings;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Requistador.WebApi.Services
{
    public class AuthService
    {
        public AuthService()
        {
            // need IdentityDb
        }

        public async Task<AppUserDto> Login()
        {
            var securityKey = AppSettings.GetAppKey();
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            
            var token = new JwtSecurityToken(
                issuer: AppConstants.Auth_ValidIssuer,
                audience: AppConstants.Auth_ValidAudience,
                claims: new List<Claim>(),
                expires: DateTime.Now.AddMinutes(3),
                signingCredentials: credentials
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return await Task.FromResult(new AppUserDto { Jwt = jwt });
        }
    }
}
