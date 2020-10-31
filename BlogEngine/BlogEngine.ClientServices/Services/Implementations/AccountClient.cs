using System.Threading.Tasks;
using BlogEngine.Shared.DTOs;
using BlogEngine.ClientServices.Extensions;
using BlogEngine.ClientServices.Services.Abstractions;
using System.Collections.Generic;
using BlogEngine.ClientServices.Helpers;

namespace BlogEngine.ClientServices.Services.Implementations
{
    public class AccountClient : IAccountClient
    {
        private IHttpService _httpService;

        public AccountClient(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<UserTokenDTO> LoginAsync(UserLoginDTO userLoginDTO)
        {
            return await _httpService.PostHelperAsync<UserLoginDTO, UserTokenDTO>(AccountClientEndpoints.Login, userLoginDTO);
        }

        public async Task<UserTokenDTO> RegisterAsync(UserRegisterDTO userRegisterDTO)
        {
            return await _httpService.PostHelperAsync<UserRegisterDTO, UserTokenDTO>(AccountClientEndpoints.Register, userRegisterDTO);
        }

        public async Task<UserProfileDTO> GetUserProfileDTOAsync(int id)
        {
            return await _httpService.GetHelperAsync<UserProfileDTO>($"{AccountClientEndpoints.UserProfile}/{id}");
        }

        public async Task<UserProfileDTO> GetUserProfileDTOAsync(string email)
        {
            return await _httpService.GetHelperAsync<UserProfileDTO>($"{AccountClientEndpoints.UserProfile}/{email}");
        }

        public async Task<List<UserInfoDetailDTO>> GetUserInfoDetailDTOsAsync()
        {
            return await _httpService.GetHelperAsync<List<UserInfoDetailDTO>>(AccountClientEndpoints.Users);
        }

        public async Task<UserProfileDTO> UpdateUserAsync(string email, UserUpdateDTO userUpdateDTO)
        {
            return await _httpService.PutHelperAsync<UserUpdateDTO, UserProfileDTO>($"{AccountClientEndpoints.Update}/{email}", userUpdateDTO);
        }

        public async Task<bool> AssignRoleAsync(UserRoleDTO userRoleDTO)
        {
            return await _httpService.PostHelperAsync<UserRoleDTO, bool>(AccountClientEndpoints.AssignRole, userRoleDTO);
        }

        public async Task<bool> RemoveRoleAsync(UserRoleDTO userRoleDTO)
        {
            return await _httpService.PostHelperAsync<UserRoleDTO, bool>(AccountClientEndpoints.RemoveRole, userRoleDTO);
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            return await _httpService.DeleteHelperAsync<bool>($"{AccountClientEndpoints.Base}/{id}");
        }
    }
}