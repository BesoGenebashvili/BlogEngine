using System;
using System.Linq;
using AutoMapper;
using System.Threading.Tasks;
using BlogEngine.Core.Data.Entities;
using BlogEngine.Server.Services.Abstractions;
using BlogEngine.Shared.DTOs;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using BlogEngine.Core.Data.DatabaseContexts;
using System.Security.Claims;
using BlogEngine.Server.Helpers;

namespace BlogEngine.Server.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public AccountService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ApplicationDbContext applicationDbContext,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _applicationDbContext = applicationDbContext;
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

        public Task<List<UserInfoDetailDTO>> GetUserInfoDetailDTOs()
        {
            // TODO: Fix thread issue

            var userInfoDetailDTOs = _applicationDbContext.Users
                .AsEnumerable()
                .OrderBy(u => u.Email)
                .Select(user =>
                {
                    var userInfoDetailDTO = _mapper.Map<UserInfoDetailDTO>(user);
                    // userInfoDetailDTO.Roles = await GetUserRoles(user);
                    return userInfoDetailDTO;
                }).ToList();

            // var userInfoDetailDTOs = await Task.WhenAll(userInfoDetailDTOsTasks);

            return Task.FromResult(userInfoDetailDTOs);
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

        public async Task<List<string>> GetUserRoles(ApplicationUser applicationUser)
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

        protected void NullCheckThrowArgumentNullException(UserRoleDTO userRoleDTO)
        {
            if (userRoleDTO == null)
            {
                throw new ArgumentNullException(nameof(userRoleDTO));
            }
        }
    }
}