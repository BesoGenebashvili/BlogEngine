using System.Threading.Tasks;
using BlogEngine.Shared.DTOs;
using BlogEngine.ClientServices.Services.Abstractions;
using BlogEngine.ClientServices.Extensions;
using BlogEngine.ClientServices.Helpers;

namespace BlogEngine.ClientServices.Services.Implementations
{
    public class PageClient : IPageClient
    {
        private readonly IHttpService _httpService;

        public PageClient(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<IndexPageDTO> GetIndexPageDTOAsync()
        {
            return await _httpService.GetHelperAsync<IndexPageDTO>(PageClientEndpoints.Index);
        }
    }
}