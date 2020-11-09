using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using BlogEngine.Core.Data.Entities;
using BlogEngine.Shared.DTOs.Identity;
using BlogEngine.Shared.Helpers;
using BlogEngine.Server.Services.Abstractions.Identity;

namespace BlogEngine.Server.Services.Implementations.Identity
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;

        public AuthenticationService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        public async Task<IdentityResult> RegisterAsync(UserRegisterDTO userRegisterDTO)
        {
            Preconditions.NotNull(userRegisterDTO, nameof(userRegisterDTO));

            var applicationUser = _mapper.Map<ApplicationUser>(userRegisterDTO);

            return await _userManager.CreateAsync(applicationUser, userRegisterDTO.Password);
        }

        public async Task<SignInResult> LoginAsync(UserLoginDTO userLoginDTO)
        {
            Preconditions.NotNull(userLoginDTO, nameof(userLoginDTO));

            return await _signInManager
                .PasswordSignInAsync(userLoginDTO.EmailAddress, userLoginDTO.Password, false, false);
        }
    }
}