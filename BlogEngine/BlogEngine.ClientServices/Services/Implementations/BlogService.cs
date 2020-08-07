using BlogEngine.ClientServices.Services.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlogEngine.ClientServices.Extensions;
using BlogEngine.Shared.DTOs;

namespace BlogEngine.ClientServices.Services.Implementations
{
    public class BlogService : IBlogService
    {
        private readonly IHttpService _httpService;
        private const string Url = "api/blogs";

        public BlogService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<BlogDTO> CreateBlogAsync(BlogCreationDTO blogCreationDTO)
        {
            return await _httpService.PostHelperAsync<BlogCreationDTO, BlogDTO>(Url, blogCreationDTO);
        }

        public async Task<BlogDTO> GetBlogAsync(int id)
        {
            return await _httpService.GetHelperAsync<BlogDTO>($"{Url}/{id}");
        }

        public async Task<List<BlogDTO>> GetBlogsAsync()
        {
            return await _httpService.GetHelperAsync<List<BlogDTO>>(Url);
        }

        public async Task<BlogUpdateDTO> GetBlogUpdateDTOAsync(int id)
        {
            return await _httpService.GetHelperAsync<BlogUpdateDTO>($"{Url}/update/{id}");
        }

        public async Task<BlogDTO> UpdateBlogAsync(int id, BlogUpdateDTO blogUpdateDTO)
        {
            return await _httpService.PutHelperAsync<BlogUpdateDTO, BlogDTO>($"{Url}/{id}", blogUpdateDTO);
        }

        public async Task<bool> DeleteBlogAsync(int id)
        {
            return await _httpService.DeleteHelperAsync<bool>($"{Url}/{id}");
        }
    }
}