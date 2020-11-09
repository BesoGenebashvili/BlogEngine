using BlogEngine.Server.Common.Models;
using BlogEngine.Shared.DTOs.Identity;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogEngine.Server.Services.Abstractions.Identity
{
    public interface IAccountService
    {
        Task<UserProfileDTO> GetUserProfileDTOAsync(int id);
        Task<UserProfileDTO> GetUserProfileDTOAsync(string email);
        Task<UserInfoDetailDTO> GetUserInfoDetailDTOAsync(int id);
        Task<List<UserInfoDetailDTO>> GetUserInfoDetailDTOsAsync();
        Task<UserProfileDTO> UpdateUserAsync(string email, UserUpdateDTO userUpdateDTO);
        Task<AccountOperationResult> DeleteAsync(int id);
    }
}