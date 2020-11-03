using System.Threading.Tasks;
using BlogEngine.ClientServices.Common.Endpoints;
using BlogEngine.ClientServices.Common.Extensions;
using BlogEngine.ClientServices.Services.Abstractions;
using BlogEngine.Shared.DTOs.Blog;

namespace BlogEngine.ClientServices.Services.Implementations
{
    public class BlogRatingClient : IBlogRatingClient
    {
        private readonly IHttpService _httpService;

        public BlogRatingClient(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task CreateAsync(BlogRatingDTO blogRatingDTO)
        {
            await _httpService.PostHelperAsync(BlogRatingClientEndpoints.Base, blogRatingDTO);
        }
    }
}