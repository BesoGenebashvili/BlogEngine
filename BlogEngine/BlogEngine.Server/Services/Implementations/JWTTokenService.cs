using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BlogEngine.Core.Data.Entities;
using BlogEngine.Core.Helpers;
using BlogEngine.Server.Services.Abstractions;
using BlogEngine.Shared.DTOs;
using BlogEngine.Shared.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BlogEngine.Server.Services.Implementations
{
    public class JWTTokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;

        public JWTTokenService(IConfiguration configuration, UserManager<ApplicationUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task<UserTokenDTO> BuildToken(UserInfoDTO userInfoDTO)
        {
            Preconditions.NotNull(userInfoDTO, nameof(userInfoDTO));

            var identityUser = await _userManager.FindByEmailAsync(userInfoDTO.EmailAddress);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, identityUser.FirstName),
                new Claim(ClaimTypes.Surname, identityUser.LastName),
                new Claim(ClaimTypes.Email, userInfoDTO.EmailAddress),
            };

            var userClaims = await _userManager.GetClaimsAsync(identityUser);

            claims.AddRange(userClaims);

            var JWTKey = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);

            var securityKey = new SymmetricSecurityKey(JWTKey);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var expirationDate = DateTime.Now.AddDays(1);

            JwtSecurityToken JWTToken =
                new JwtSecurityToken(
                    issuer: null,
                    audience: null,
                    claims: claims,
                    expires: expirationDate,
                    signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(JWTToken);

            return new UserTokenDTO(token, expirationDate);
        }
    }
}