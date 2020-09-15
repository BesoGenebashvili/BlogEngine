using System;
using System.Threading.Tasks;
using AutoMapper;
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

        public async Task<IdentityResult> RegisterAsync(UserInfoDTO userInfoDTO)
        {
            NullCheckThrowArgumentNullException(userInfoDTO);

            var applicationUser = _mapper.Map<ApplicationUser>(userInfoDTO);

            return await _userManager.CreateAsync(applicationUser, userInfoDTO.Password);
        }

        public async Task<SignInResult> LoginAsync(UserInfoDTO userInfoDTO)
        {
            NullCheckThrowArgumentNullException(userInfoDTO);

            return await _signInManager
                .PasswordSignInAsync(userInfoDTO.EmailAddress, userInfoDTO.Password, false, false);
        }

        protected void NullCheckThrowArgumentNullException(UserInfoDTO userInfoDTO)
        {
            if (userInfoDTO == null)
            {
                throw new ArgumentNullException(nameof(userInfoDTO));
            }
        }
    }
}