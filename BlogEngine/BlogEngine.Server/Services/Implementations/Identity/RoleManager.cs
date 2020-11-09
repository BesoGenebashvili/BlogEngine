using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using BlogEngine.Core.Data.Entities;
using BlogEngine.Server.Common.Models;
using BlogEngine.Shared.DTOs.Identity;
using BlogEngine.Shared.Helpers;
using BlogEngine.Server.Services.Abstractions.Identity;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BlogEngine.Server.Services.Implementations.Identity
{
    public class RoleManager : IRoleManager
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public RoleManager(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<AccountOperationResult> AssignRoleAsync(UserRoleDTO userRoleDTO)
        {
            Preconditions.NotNull(userRoleDTO, nameof(userRoleDTO));

            var user = await _userManager.FindByIdAsync(userRoleDTO.UserID.ToString());

            if (user is null)
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

            if (user is null)
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

        public async Task<List<string>> GetUserRolesAsync(int id)
        {
            var applicationUser = await _userManager.Users.FirstOrDefaultAsync(u => u.Id.Equals(id));

            if (applicationUser is null) return default;

            return await GetUserRolesAsync(applicationUser);
        }

        public async Task<List<string>> GetUserRolesAsync(ApplicationUser applicationUser)
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