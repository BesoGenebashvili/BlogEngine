using System;
using System.Text;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BlogEngine.Core.Data.Entities;
using BlogEngine.Shared.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BlogEngine.Server.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountsController(
            IMapper mapper,
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration,
            SignInManager<ApplicationUser> signInManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
            _signInManager = signInManager;
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UserTokenDTO), 200)]
        [HttpPost("register")]
        public async Task<ActionResult<UserTokenDTO>> Register([FromBody] UserInfoDTO userInfoDTO)
        {
            var applicationUser = _mapper.Map<ApplicationUser>(userInfoDTO);

            var identityResult = await _userManager.CreateAsync(applicationUser, userInfoDTO.Password);

            if (!identityResult.Succeeded)
            {
                return BadRequest(identityResult.Errors);
            }

            return BuildToken(userInfoDTO);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserTokenDTO>> Login([FromBody] UserInfoDTO userInfoDTO)
        {
            var signInResult = await _signInManager
                .PasswordSignInAsync(userInfoDTO.EmailAddress, userInfoDTO.Password, false, false);

            if (!signInResult.Succeeded)
            {
                return BadRequest("Invalid Login attempt");
            }

            return BuildToken(userInfoDTO);
        }

        // TODO: Token should be generated from service
        private UserTokenDTO BuildToken(UserInfoDTO userInfoDTO)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, userInfoDTO.EmailAddress),
                new Claim(ClaimTypes.Email, userInfoDTO.EmailAddress),
            };

            var JWTKey = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);

            var securityKey = new SymmetricSecurityKey(JWTKey);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var expirationDate = DateTime.Now.AddMonths(1);

            JwtSecurityToken JWTToken =
                new JwtSecurityToken(
                    issuer: null,
                    audience: null,
                    claims: claims,
                    expires: expirationDate,
                    signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(JWTToken);

            return new UserTokenDTO()
            {
                ExpirationDate = expirationDate,
                Token = token
            };
        }
    }
}