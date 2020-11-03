using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Collections.Generic;
using BlogEngine.ClientServices.Services.Abstractions;
using BlogEngine.Shared.Helpers;

namespace BlogEngine.ClientServices.Services.Implementations
{
    public class JWTClaimParserService : IJWTClaimParserService
    {
        public virtual async Task<IEnumerable<Claim>> ParseAsync(string jwtToken)
        {
            Preconditions.NotNullOrWhiteSpace(jwtToken, nameof(jwtToken));

            return await Task.Run(() => ProcessParse(jwtToken));
        }

        protected IEnumerable<Claim> ProcessParse(string jwtToken)
        {
            var claims = new List<Claim>();
            var payload = jwtToken.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            keyValuePairs.TryGetValue(ClaimTypes.Role, out object roles);

            if (roles != null)
            {
                if (roles.ToString().Trim().StartsWith("["))
                {
                    var parsedRoles = JsonSerializer.Deserialize<string[]>(roles.ToString());

                    foreach (var parsedRole in parsedRoles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, parsedRole));
                    }
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.Role, roles.ToString()));
                }

                keyValuePairs.Remove(ClaimTypes.Role);
            }

            claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));
            return claims;
        }

        protected byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}