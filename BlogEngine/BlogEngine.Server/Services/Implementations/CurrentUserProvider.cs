using BlogEngine.Core.Data.Entities;
using BlogEngine.Core.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlogEngine.Server.Services.Implementations
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
    }
}