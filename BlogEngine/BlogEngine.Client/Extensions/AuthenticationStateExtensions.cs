using Microsoft.AspNetCore.Components.Authorization;
using System.Linq;

namespace BlogEngine.Client.Extensions
{
    public static class AuthenticationStateExtensions
    {
        public static string GetClaimValue(this AuthenticationState authenticationState, string claimType)
        {
            return authenticationState.User.Claims.FirstOrDefault(c => c.Type.Equals(claimType))?.Value;
        }
    }
}