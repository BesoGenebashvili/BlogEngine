using System;
using AutoMapper;
using System.Threading.Tasks;
using BlogEngine.Core.Data.Entities;
using BlogEngine.Server.Services.Abstractions;
using BlogEngine.Shared.DTOs;
using Microsoft.AspNetCore.Identity;

namespace BlogEngine.Server.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;

        public AccountService(
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
            NullCheckThrowArgumentNullException(userRegisterDTO);

            var applicationUser = _mapper.Map<ApplicationUser>(userRegisterDTO);

            return await _userManager.CreateAsync(applicationUser, userRegisterDTO.Password);
        }

        public async Task<SignInResult> LoginAsync(UserLoginDTO userLoginDTO)
        {
            NullCheckThrowArgumentNullException(userLoginDTO);

            return await _signInManager
                .PasswordSignInAsync(userLoginDTO.EmailAddress, userLoginDTO.Password, false, false);
        }

        protected void NullCheckThrowArgumentNullException(UserRegisterDTO userRegisterDTO)
        {
            if (userRegisterDTO == null)
            {
                throw new ArgumentNullException(nameof(userRegisterDTO));
            }
        }

        protected void NullCheckThrowArgumentNullException(UserLoginDTO userLoginDTO)
        {
            if (userLoginDTO == null)
            {
                throw new ArgumentNullException(nameof(userLoginDTO));
            }
        }
    }
}