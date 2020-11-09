using System.Linq;
using System.Security.Claims;
using BlogEngine.Shared.Helpers;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlogEngine.ClientServices.Common.Extensions
{
    public static class AuthenticationStateExtensions
    {
        public static string GetClaimValue(this AuthenticationState authenticationState, string claimType)
        {
            return authenticationState.User.Claims.FirstOrDefault(c => c.Type.Equals(claimType))?.Value;
        }

        public static bool IsCurrentUserAdmin(this AuthenticationState authenticationState)
        {
            return authenticationState.User.HasClaim(ClaimTypes.Role, UserRole.Admin);
        }
    }
}