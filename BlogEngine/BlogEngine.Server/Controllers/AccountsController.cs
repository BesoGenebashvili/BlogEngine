using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Collections.Generic;
using BlogEngine.Shared.Helpers;
using BlogEngine.Shared.DTOs.Identity;
using BlogEngine.Server.Services.Abstractions.Identity;

namespace BlogEngine.Server.Controllers
{
    public class AccountsController : BaseController // TODO: Separate this
    {
        private readonly IAccountService _accountService;
        private readonly IRoleManager _roleManager;
        private readonly IAuthenticationService _authenticationService;
        private readonly ITokenService _tokenService;

        public AccountsController(
            IAccountService accountService,
            IAuthenticationService authenticationService,
            IRoleManager roleManager,
            ITokenService tokenService)
        {
            _accountService = accountService;
            _authenticationService = authenticationService;
            _roleManager = roleManager;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UserTokenDTO), StatusCodes.Status200OK)]
        public async Task<ActionResult<UserTokenDTO>> Register([FromBody] UserRegisterDTO userRegisterDTO)
        {
            var identityResult = await _authenticationService.RegisterAsync(userRegisterDTO);

            if (!identityResult.Succeeded) return BadRequest(identityResult.Errors);

            return await _tokenService.BuildToken(userRegisterDTO);
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UserTokenDTO), StatusCodes.Status200OK)]
        public async Task<ActionResult<UserTokenDTO>> Login([FromBody] UserLoginDTO userLoginDTO)
        {
            var signInResult = await _authenticationService.LoginAsync(userLoginDTO);

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

            if (userProfileDTO is null) return NotFound();

            return userProfileDTO;
        }

        [HttpGet("user/profile/{email}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(UserProfileDTO), StatusCodes.Status200OK)]
        public async Task<ActionResult<UserProfileDTO>> GetUser(string email)
        {
            var userProfileDTO = await _accountService.GetUserProfileDTOAsync(email);

            if (userProfileDTO is null) return NotFound();

            return userProfileDTO;
        }

        [HttpPut("update/{email}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(UserProfileDTO), StatusCodes.Status200OK)]
        public async Task<ActionResult<UserProfileDTO>> UpdateUser(string email, [FromBody] UserUpdateDTO userUpdateDTO)
        {
            return await _accountService.UpdateUserAsync(email, userUpdateDTO);
        }

        [HttpPost("assignRole")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = UserRole.Admin)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> AssignRole([FromBody] UserRoleDTO userRoleDTO)
        {
            var assignmentResult = await _roleManager.AssignRoleAsync(userRoleDTO);

            if (assignmentResult.UserNotFound) return NotFound();

            return assignmentResult.Successed;
        }

        [HttpPost("removeRole")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = UserRole.Admin)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> RemoveRole([FromBody] UserRoleDTO userRoleDTO)
        {
            var assignationResult = await _roleManager.RemoveRoleAsync(userRoleDTO);

            if (assignationResult.UserNotFound) return NotFound();

            return assignationResult.Successed;
        }

        [HttpDelete("{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = UserRole.Admin)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> DeleteUser(int id)
        {
            var deleteResult = await _accountService.DeleteAsync(id);

            if (deleteResult.UserNotFound) return NotFound();

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