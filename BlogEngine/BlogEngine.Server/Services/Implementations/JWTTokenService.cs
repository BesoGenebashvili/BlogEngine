using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BlogEngine.Server.Services.Abstractions;
using BlogEngine.Shared.DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BlogEngine.Server.Services.Implementations
{
    public class JWTTokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public JWTTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task<UserTokenDTO> BuildToken(UserInfoDTO userInfoDTO)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, userInfoDTO.EmailAddress),
                new Claim(ClaimTypes.Email, userInfoDTO.EmailAddress),
            };

            var JWTKey = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);

            var securityKey = new SymmetricSecurityKey(JWTKey);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var expirationDate = DateTime.Now.AddHours(1);

            JwtSecurityToken JWTToken =
                new JwtSecurityToken(
                    issuer: null,
                    audience: null,
                    claims: claims,
                    expires: expirationDate,
                    signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(JWTToken);

            return Task.FromResult(new UserTokenDTO(token, expirationDate));
        }
    }
}