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

        public async Task<UserTokenDTO> LoginAsync(UserLoginDTO userLoginDTO)
        {
            return await _httpService.PostHelperAsync<UserLoginDTO, UserTokenDTO>(BaseUrl + "/login", userLoginDTO);
        }

        public async Task<UserTokenDTO> RegisterAsync(UserRegisterDTO userRegisterDTO)
        {
            return await _httpService.PostHelperAsync<UserRegisterDTO, UserTokenDTO>(BaseUrl + "/register", userRegisterDTO);
        }
    }
}