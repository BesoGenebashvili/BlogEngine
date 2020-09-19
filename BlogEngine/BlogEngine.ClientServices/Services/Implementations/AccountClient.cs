using System.Threading.Tasks;
using BlogEngine.Shared.DTOs;
using BlogEngine.ClientServices.Extensions;
using BlogEngine.ClientServices.Services.Abstractions;

namespace BlogEngine.ClientServices.Services.Implementations
{
    public class AccountClient : IAccountClient
    {
        private IHttpService _httpService;
        private const string BaseUrl = "api/accounts";

        public AccountClient(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<UserTokenDTO> LoginAsync(UserInfoDTO userInfoDTO)
        {
            return await _httpService.PostHelperAsync<UserInfoDTO, UserTokenDTO>(BaseUrl + "/login", userInfoDTO);
        }

        public async Task<UserTokenDTO> RegisterAsync(UserInfoDTO userInfoDTO)
        {
            return await _httpService.PostHelperAsync<UserInfoDTO, UserTokenDTO>(BaseUrl + "/register", userInfoDTO);
        }
    }
}