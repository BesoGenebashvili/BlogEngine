using System;
using System.Linq;
using AutoMapper;
using System.Threading.Tasks;
using BlogEngine.Core.Data.Entities;
using BlogEngine.Server.Services.Abstractions;
using BlogEngine.Shared.DTOs;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Claims;
using BlogEngine.Server.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using BlogEngine.Core.Helpers;

namespace BlogEngine.Server.Services.Implementations
{
    public class AccountService : IAccountService, ICurrentUserProvider
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
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

        public async Task<UserProfileDTO> GetUserProfileDTOAsync(int id)
        {
            var applicationUser = await _userManager.FindByIdAsync(id.ToString());

            if (applicationUser == null) return null;

            var userProfileDTO = _mapper.Map<UserProfileDTO>(applicationUser);

            userProfileDTO.Roles = await GetUserRoles(applicationUser);

            return userProfileDTO;
        }

        public async Task<List<UserInfoDetailDTO>> GetUserInfoDetailDTOsAsync()
        {
            var applicationUsers = await _userManager.Users.ToListAsync();

            var userInfoDetalDTOs = new List<UserInfoDetailDTO>();

            // Because of threading issue...
            foreach (var applicationUser in applicationUsers)
            {
                // We should use Task.Whenall there but we can not
                userInfoDetalDTOs.Add(await MapWithRolesAsync(applicationUser));
            }

            return userInfoDetalDTOs;
        }

        public async Task<AccountOperationResult> AssignRoleAsync(UserRoleDTO userRoleDTO)
        {
            NullCheckThrowArgumentNullException(userRoleDTO);

            var user = await _userManager.FindByIdAsync(userRoleDTO.UserID.ToString());

            if (user == null)
            {
                return new AccountOperationResult()
                {
                    UserNotFound = true,
                    Errors = $"User with a ID = '{userRoleDTO.UserID}' was not found in the Database"
                };
            }

            var identityResult = await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, userRoleDTO.RoleName));

            return new AccountOperationResult()
            {
                Successed = identityResult.Succeeded,
                Errors = string.Join(", ", identityResult.Errors)
            };
        }

        public async Task<AccountOperationResult> RemoveRoleAsync(UserRoleDTO userRoleDTO)
        {
            NullCheckThrowArgumentNullException(userRoleDTO);

            var user = await _userManager.FindByIdAsync(userRoleDTO.UserID.ToString());

            if (user == null)
            {
                return new AccountOperationResult()
                {
                    UserNotFound = true,
                    Errors = $"User with a ID = '{userRoleDTO.UserID}' was not found in the Database"
                };
            }

            var identityResult = await _userManager.RemoveClaimAsync(user, new Claim(ClaimTypes.Role, userRoleDTO.RoleName));

            return new AccountOperationResult()
            {
                Successed = identityResult.Succeeded,
                Errors = string.Join(", ", identityResult.Errors)
            };
        }

        public async Task<AccountOperationResult> DeleteAsync(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null)
            {
                return new AccountOperationResult()
                {
                    UserNotFound = true,
                    Errors = $"User with a ID = '{id}' was not found in the Database"
                };
            }

            var identityResult = await _userManager.DeleteAsync(user);

            return new AccountOperationResult()
            {
                Successed = identityResult.Succeeded,
                Errors = string.Join(", ", identityResult.Errors)
            };
        }

        public async Task<ApplicationUser> GetCurrentUser()
        {
            var user = _httpContextAccessor.HttpContext.User;

            if (user == null)
            {
                throw new InvalidOperationException("User is not logged in");
            }

            var emailClaim = user.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Email));

            var email = emailClaim.Value;

            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<int> GetCurrentUserID() => (await GetCurrentUser()).Id;

        protected async Task<UserInfoDetailDTO> MapWithRolesAsync(ApplicationUser applicationUser)
        {
            var userInfoDetailDTO = _mapper.Map<UserInfoDetailDTO>(applicationUser);

            userInfoDetailDTO.Roles = await GetUserRoles(applicationUser);

            return userInfoDetailDTO;
        }

        protected async Task<List<string>> GetUserRoles(ApplicationUser applicationUser)
        {
            var claims = await _userManager.GetClaimsAsync(applicationUser);

            var roles = claims
                .Where(c => c.Type.Equals(ClaimTypes.Role))
                .Select(c => c.Value)
                .ToList();

            return roles;
        }

        protected void NullCheckThrowArgumentNullException(UserRegisterDTO userRegisterDTO)
        {
            if (userRegisterDTO == null)
            {
                Throw.ArgumentNullException(nameof(userRegisterDTO));
            }
        }

        protected void NullCheckThrowArgumentNullException(UserLoginDTO userLoginDTO)
        {
            if (userLoginDTO == null)
            {
                Throw.ArgumentNullException(nameof(userLoginDTO));
            }
        }

        protected void NullCheckThrowArgumentNullException(UserRoleDTO userRoleDTO)
        {
            if (userRoleDTO == null)
            {
                Throw.ArgumentNullException(nameof(userRoleDTO));
            }
        }
    }
}