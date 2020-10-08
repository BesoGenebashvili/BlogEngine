using BlogEngine.Server.Helpers;
using BlogEngine.Shared.DTOs;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogEngine.Server.Services.Abstractions
{
    public interface IAccountService
    {
        Task<IdentityResult> RegisterAsync(UserRegisterDTO userRegisterDTO);
        Task<SignInResult> LoginAsync(UserLoginDTO userLoginDTO);
        Task<UserProfileDTO> GetUserProfileDTOAsync(int id);
        Task<List<UserInfoDetailDTO>> GetUserInfoDetailDTOsAsync();
        Task<AccountOperationResult> AssignRoleAsync(UserRoleDTO userRoleDTO);
        Task<AccountOperationResult> RemoveRoleAsync(UserRoleDTO userRoleDTO);
        Task<AccountOperationResult> DeleteAsync(int id);
    }
}