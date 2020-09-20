using System.Threading.Tasks;
using BlogEngine.Shared.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BlogEngine.Server.Services.Abstractions;

namespace BlogEngine.Server.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class AccountsController : ControllerBase
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
        [ProducesResponseType(typeof(UserTokenDTO), 200)]
        public async Task<ActionResult<UserTokenDTO>> Register([FromBody] UserRegisterDTO userRegisterDTO)
        {
            var identityResult = await _accountService.RegisterAsync(userRegisterDTO);

            if (!identityResult.Succeeded) return BadRequest(identityResult.Errors);

            return await _tokenService.BuildToken(userRegisterDTO);
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UserTokenDTO), 200)]
        public async Task<ActionResult<UserTokenDTO>> Login([FromBody] UserLoginDTO userLoginDTO)
        {
            var signInResult = await _accountService.LoginAsync(userLoginDTO);

            if (!signInResult.Succeeded) return BadRequest("Invalid Login attempt");

            return await _tokenService.BuildToken(userLoginDTO);
        }
    }
}