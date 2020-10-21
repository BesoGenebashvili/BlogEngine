using System.Threading.Tasks;
using BlogEngine.Shared.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BlogEngine.Server.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Collections.Generic;
using BlogEngine.Shared.Helpers;

namespace BlogEngine.Server.Controllers
{
    public class AccountsController : BaseController
    {
        private readonly ITokenService _tokenService;
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _accountService = accountService;
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UserTokenDTO), StatusCodes.Status200OK)]
        public async Task<ActionResult<UserTokenDTO>> Register([FromBody] UserRegisterDTO userRegisterDTO)
        {
            var identityResult = await _accountService.RegisterAsync(userRegisterDTO);

            if (!identityResult.Succeeded) return BadRequest(identityResult.Errors);

            return await _tokenService.BuildToken(userRegisterDTO);
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UserTokenDTO), StatusCodes.Status200OK)]
        public async Task<ActionResult<UserTokenDTO>> Login([FromBody] UserLoginDTO userLoginDTO)
        {
            var signInResult = await _accountService.LoginAsync(userLoginDTO);

            if (!signInResult.Succeeded) return BadRequest("Invalid Login attempt");

            return await _tokenService.BuildToken(userLoginDTO);
        }

        [HttpGet("users")]
        [ProducesResponseType(typeof(List<UserInfoDetailDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<UserInfoDetailDTO>>> GetUsers()
        {
            return await _accountService.GetUserInfoDetailDTOsAsync();
        }

        [HttpGet("user/profile/{id:int}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(UserProfileDTO), StatusCodes.Status200OK)]
        public async Task<ActionResult<UserProfileDTO>> GetUser(int id)
        {
            var userProfileDTO = await _accountService.GetUserProfileDTOAsync(id);

            if (userProfileDTO == null) return NotFound();

            return userProfileDTO;
        }

        [HttpPost("assignRole")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = UserRole.Admin)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> AssignRole([FromBody] UserRoleDTO userRoleDTO)
        {
            var assignmentResult = await _accountService.AssignRoleAsync(userRoleDTO);

            if (assignmentResult.UserNotFound)
            {
                return NotFound();
            }

            return assignmentResult.Successed;
        }

        [HttpPost("removeRole")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = UserRole.Admin)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> RemoveRole([FromBody] UserRoleDTO userRoleDTO)
        {
            var assignationResult = await _accountService.RemoveRoleAsync(userRoleDTO);

            if (assignationResult.UserNotFound)
            {
                return NotFound();
            }

            return assignationResult.Successed;
        }

        [HttpDelete("{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = UserRole.Admin)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> DeleteUser(int id)
        {
            var deleteResult = await _accountService.DeleteAsync(id);

            if (deleteResult.UserNotFound)
            {
                return NotFound();
            }

            return deleteResult.Successed;
        }

        [HttpPost("renewToken")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<UserTokenDTO>> RenewToken()
        {
            var userInfo = new UserInfoDTO()
            {
                EmailAddress = HttpContext.User.Identity.Name
            };

            return await _tokenService.BuildToken(userInfo);
        }
    }
}