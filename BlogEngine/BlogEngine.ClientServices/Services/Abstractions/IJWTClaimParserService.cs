using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlogEngine.ClientServices.Services.Abstractions
{
    public interface IJWTClaimParserService
    {
        Task<IEnumerable<Claim>> ParseAsync(string jwtToken);
    }
}