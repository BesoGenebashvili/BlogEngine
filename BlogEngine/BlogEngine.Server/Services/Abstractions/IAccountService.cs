using BlogEngine.Server.Common.Models;
using BlogEngine.Shared.DTOs.Identity;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogEngine.Server.Services.Abstractions
{
    public interface IAccountService
    {
        Task<IdentityResult> RegisterAsync(UserRegisterDTO userRegisterDTO);
        Task<SignInResult> LoginAsync(UserLoginDTO userLoginDTO);
        Task<UserProfileDTO> GetUserProfileDTOAsync(string email);
        Task<UserProfileDTO> GetUserProfileDTOAsync(int id);
        Task<List<UserInfoDetailDTO>> GetUserInfoDetailDTOsAsync();
        Task<UserProfileDTO> UpdateUserAsync(string email, UserUpdateDTO userUpdateDTO);
        Task<AccountOperationResult> AssignRoleAsync(UserRoleDTO userRoleDTO);
        Task<AccountOperationResult> RemoveRoleAsync(UserRoleDTO userRoleDTO);
        Task<AccountOperationResult> DeleteAsync(int id);
    }
}