using System;
using System.Linq;
using AutoMapper;
using System.Threading.Tasks;
using BlogEngine.Core.Data.Entities;
using BlogEngine.Server.Services.Abstractions;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using BlogEngine.Shared.Helpers;
using BlogEngine.Core.Services.Abstractions;
using BlogEngine.Server.Common.Models;
using BlogEngine.Shared.DTOs.Identity;
using BlogEngine.Core.Common.Exceptions;

namespace BlogEngine.Server.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ICurrentUserProvider _currentUserProvider;
        private readonly IMapper _mapper;

        public AccountService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ICurrentUserProvider currentUserProvider,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _currentUserProvider = currentUserProvider;
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

        public async Task<UserProfileDTO> GetUserProfileDTOAsync(int id)
        {
            var applicationUser = await _userManager.FindByIdAsync(id.ToString());

            if (applicationUser == null) return null;

            var userProfileDTO = _mapper.Map<UserProfileDTO>(applicationUser);

            userProfileDTO.Roles = await GetUserRoles(applicationUser);

            return userProfileDTO;
        }

        public async Task<UserProfileDTO> GetUserProfileDTOAsync(string email)
        {
            Preconditions.NotNull(email, nameof(email));

            var applicationUser = await _userManager.FindByEmailAsync(email);

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

        public async Task<UserProfileDTO> UpdateUserAsync(string email, UserUpdateDTO userUpdateDTO)
        {
            Preconditions.NotNullOrWhiteSpace(email, nameof(email));
            Preconditions.NotNull(userUpdateDTO, nameof(userUpdateDTO));

            var applicationUser = await _userManager.FindByEmailAsync(email);

            if (applicationUser is null)
            {
                throw new EntityNotFoundException(nameof(ApplicationUser));
            }

            var currentUser = await _currentUserProvider.GetCurrentUserAsync();

            if (currentUser is null)
            {
                throw new UnauthorizedAccessException();
            }

            if (!applicationUser.Equals(currentUser))
            {
                throw new InvalidOperationException();
            }

            applicationUser.FirstName = userUpdateDTO.FirstName;
            applicationUser.LastName = userUpdateDTO.LastName;
            applicationUser.ProfilePicture = userUpdateDTO.ProfilePicture;

            var identityResult = await _userManager.UpdateAsync(applicationUser);

            if (identityResult.Succeeded)
            {
                var userProfileDTO = _mapper.Map<UserProfileDTO>(applicationUser);

                userProfileDTO.Roles = await GetUserRoles(applicationUser);

                return userProfileDTO;
            }

            return null;
        }

        public async Task<AccountOperationResult> AssignRoleAsync(UserRoleDTO userRoleDTO)
        {
            Preconditions.NotNull(userRoleDTO, nameof(userRoleDTO));

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
            Preconditions.NotNull(userRoleDTO, nameof(userRoleDTO));

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
    }
}