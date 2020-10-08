using BlogEngine.Shared.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogEngine.ClientServices.Services.Abstractions
{
    public interface IAccountClient
    {
        Task<UserProfileDTO> GetUserProfileDTOAsync(int id);
        Task<List<UserInfoDetailDTO>> GetUserInfoDetailDTOsAsync();
        Task<UserTokenDTO> LoginAsync(UserLoginDTO userLoginDTO);
        Task<UserTokenDTO> RegisterAsync(UserRegisterDTO userRegisterDTO);
        Task<bool> AssignRoleAsync(UserRoleDTO userRoleDTO);
        Task<bool> RemoveRoleAsync(UserRoleDTO userRoleDTO);
        Task<bool> DeleteUserAsync(int id);
    }
}