using BlogEngine.ClientServices.Services.Abstractions;
using BlogEngine.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlogEngine.ClientServices.Extensions;

namespace BlogEngine.ClientServices.Services.Implementations
{
    public class BlogService : IBlogService
    {
        private readonly IHttpService _httpService;
        private const string Url = "api/blog";

        public BlogService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<int> CreateBlogAsync(BlogModel blogmodel)
        {
            return await _httpService.PostHelperAsync<BlogModel, int>(Url, blogmodel);
        }

        public async Task<BlogModel> GetBlogAsync(int id)
        {
            return await _httpService.GetHelperAsync<BlogModel>($"{Url}/{id}");
        }

        public async Task<List<BlogModel>> GetBlogsAsync()
        {
            return await _httpService.GetHelperAsync<List<BlogModel>>(Url);
        }

        public async Task<BlogModel> UpdateBlogAsync(BlogModel blogModel)
        {
            return await _httpService.PutHelperAsync<BlogModel, BlogModel>(Url, blogModel);
        }

        public async Task<bool> DeleteBlogAsync(int id)
        {
            return await _httpService.DeleteHelperAsync<bool>($"{Url}/{id}");
        }
    }
}