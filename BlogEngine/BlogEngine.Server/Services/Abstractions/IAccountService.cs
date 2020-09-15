using BlogEngine.Shared.DTOs;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace BlogEngine.Server.Services.Abstractions
{
    public interface IAccountService
    {
        Task<IdentityResult> RegisterAsync(UserInfoDTO userInfoDTO);
        Task<SignInResult> LoginAsync(UserInfoDTO userInfoDTO);
    }
}