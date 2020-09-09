using System.Threading.Tasks;
using BlogEngine.Shared.DTOs;
using BlogEngine.ClientServices.Services.Abstractions;
using BlogEngine.ClientServices.Extensions;

namespace BlogEngine.ClientServices.Services.Implementations
{
    public class PagesClient : IPagesClient
    {
        private readonly IHttpService _httpService;
        private const string Url = "api/pages";

        public PagesClient(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<IndexPageDTO> GetIndexPageDTOAsync()
        {
            return await _httpService.GetHelperAsync<IndexPageDTO>($"{Url}/index");
        }
    }
}