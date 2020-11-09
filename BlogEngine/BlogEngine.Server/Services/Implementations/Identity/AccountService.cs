using System;
using AutoMapper;
using System.Threading.Tasks;
using BlogEngine.Core.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using BlogEngine.Shared.Helpers;
using BlogEngine.Core.Services.Abstractions;
using BlogEngine.Server.Common.Models;
using BlogEngine.Shared.DTOs.Identity;
using BlogEngine.Core.Common.Exceptions;
using BlogEngine.Server.Services.Abstractions.Identity;

namespace BlogEngine.Server.Services.Implementations.Identity
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICurrentUserProvider _currentUserProvider;
        private readonly IRoleManager _roleManager;
        private readonly IMapper _mapper;

        public AccountService(
            UserManager<ApplicationUser> userManager,
            ICurrentUserProvider currentUserProvider,
            IRoleManager roleManager,
            IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
            _currentUserProvider = currentUserProvider;
            _roleManager = roleManager;
        }

        public async Task<UserProfileDTO> GetUserProfileDTOAsync(int id)
        {
            var applicationUser = await _userManager.FindByIdAsync(id.ToString());

            if (applicationUser is null) return null;

            var userProfileDTO = _mapper.Map<UserProfileDTO>(applicationUser);

            userProfileDTO.Roles = await _roleManager.GetUserRolesAsync(applicationUser);

            return userProfileDTO;
        }

        public async Task<UserProfileDTO> GetUserProfileDTOAsync(string email)
        {
            Preconditions.NotNull(email, nameof(email));

            var applicationUser = await _userManager.FindByEmailAsync(email);

            if (applicationUser is null) return null;

            var userProfileDTO = _mapper.Map<UserProfileDTO>(applicationUser);

            userProfileDTO.Roles = await _roleManager.GetUserRolesAsync(applicationUser);

            return userProfileDTO;
        }

        public async Task<UserInfoDetailDTO> GetUserInfoDetailDTOAsync(int id)
        {
            var applicationUser = await _userManager.Users.FirstOrDefaultAsync(u => u.Id.Equals(id));

            if (applicationUser is null) return null;

            var userInfoDetailDTO = _mapper.Map<UserInfoDetailDTO>(applicationUser);

            userInfoDetailDTO.Roles = await _roleManager.GetUserRolesAsync(applicationUser);

            return userInfoDetailDTO;
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

            if (applicationUser is null) throw new EntityNotFoundException(nameof(ApplicationUser));

            var currentUser = await _currentUserProvider.GetCurrentUserAsync();

            if (currentUser is null) throw new UnauthorizedAccessException();

            if (!applicationUser.Equals(currentUser)) throw new InvalidOperationException();

            applicationUser.FirstName = userUpdateDTO.FirstName;
            applicationUser.LastName = userUpdateDTO.LastName;
            applicationUser.ProfilePicture = userUpdateDTO.ProfilePicture;

            var identityResult = await _userManager.UpdateAsync(applicationUser);

            if (!identityResult.Succeeded) return null;

            var userProfileDTO = _mapper.Map<UserProfileDTO>(applicationUser);

            userProfileDTO.Roles = await _roleManager.GetUserRolesAsync(applicationUser);

            return userProfileDTO;
        }

        public async Task<AccountOperationResult> DeleteAsync(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user is null)
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

            userInfoDetailDTO.Roles = await _roleManager.GetUserRolesAsync(applicationUser);

            return userInfoDetailDTO;
        }
    }
}