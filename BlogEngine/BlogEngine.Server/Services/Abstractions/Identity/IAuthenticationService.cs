using System.Threading.Tasks;
using BlogEngine.Shared.DTOs.Identity;
using Microsoft.AspNetCore.Identity;

namespace BlogEngine.Server.Services.Abstractions.Identity
{
    public interface IAuthenticationService
    {
        Task<IdentityResult> RegisterAsync(UserRegisterDTO userRegisterDTO);
        Task<SignInResult> LoginAsync(UserLoginDTO userLoginDTO);
    }
}