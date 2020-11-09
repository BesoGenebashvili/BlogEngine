using BlogEngine.Core.Data.Entities;
using BlogEngine.Core.Services.Abstractions;
using BlogEngine.Server.Services.Abstractions.Identity;
using BlogEngine.Shared.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlogEngine.Server.Services.Implementations.Identity
{
    public class CurrentUserProvider : ICurrentUserProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApplicationUser> GetCurrentUserAsync()
        {
            var user = _httpContextAccessor.HttpContext.User;

            if (!user.Identity.IsAuthenticated)
            {
                return null;
            }

            var emailClaim = user.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Email));

            if (emailClaim is null)
            {
                return null;
            }

            var email = emailClaim.Value;

            // Do not use a constructor injection for UserManager because of circular dependency
            var userManager = _httpContextAccessor.HttpContext.RequestServices
                .GetService<UserManager<ApplicationUser>>();

            return await userManager.FindByEmailAsync(email);
        }

        public async Task<int> GetCurrentUserIDAsync()
        {
            var currentUser = await GetCurrentUserAsync();

            return currentUser is null ? default : currentUser.Id;
        }

        public async Task<bool> IsCurrentUserAdmin()
        {
            var currentUser = await GetCurrentUserAsync();

            // Do not use a constructor injection for IRoleManager because of circular dependency
            var roleManager = _httpContextAccessor.HttpContext.RequestServices
                .GetService<IRoleManager>();

            var currentUserRoles = await roleManager.GetUserRolesAsync(currentUser);

            return currentUserRoles.Contains(UserRole.Admin);
        }
    }
}