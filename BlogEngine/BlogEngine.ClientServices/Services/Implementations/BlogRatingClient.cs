using System;
using System.Threading.Tasks;
using BlogEngine.ClientServices.Extensions;
using BlogEngine.ClientServices.Helpers;
using BlogEngine.ClientServices.Services.Abstractions;
using BlogEngine.Shared.DTOs;

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