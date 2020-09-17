using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using BlogEngine.ClientServices.Services.Abstractions;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlogEngine.ClientServices.Account
{
    public class JWTAuthenticationStateProvider : AuthenticationStateProvider, ILoginService
    {
        private readonly IBrowserStorageService _browserStorageService;
        private readonly IJWTClaimParserService _JWTClaimParserService;
        private readonly HttpClient _httpClient;

        private const string TOKENKEY = "TOKENKEY";

        public JWTAuthenticationStateProvider(
            IBrowserStorageService browserStorageService,
            IJWTClaimParserService JWTClaimParserService,
            HttpClient httpClient)
        {
            _browserStorageService = browserStorageService;
            _JWTClaimParserService = JWTClaimParserService;
            _httpClient = httpClient;
        }

        private AuthenticationState Anonymous => BuildAnonymousAuthenticationState();

        private AuthenticationState BuildAnonymousAuthenticationState()
        {
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _browserStorageService.GetFromLocalStorage(TOKENKEY);

            if (string.IsNullOrWhiteSpace(token)) return Anonymous;

            return await BuildAuthenticationStateAsync(token);
        }

        public async Task<AuthenticationState> BuildAuthenticationStateAsync(string token)
        {
            var claims = await _JWTClaimParserService.Parse(token);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt")));
        }

        #region LoginService

        // TODO: This must be separated service
        public async Task Login(string token)
        {
            await _browserStorageService.SetInLocalStorage(TOKENKEY, token);
            var authStateTask = BuildAuthenticationStateAsync(token);

            NotifyAuthenticationStateChanged(authStateTask);
        }

        public async Task Logout()
        {
            await _browserStorageService.RemoveFromLocalStorage(TOKENKEY);
            _httpClient.DefaultRequestHeaders.Authorization = null;

            NotifyAuthenticationStateChanged(Task.FromResult(Anonymous));
        }

        #endregion
    }
}