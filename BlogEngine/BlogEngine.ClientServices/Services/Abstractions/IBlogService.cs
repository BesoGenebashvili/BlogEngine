using BlogEngine.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogEngine.ClientServices.Services.Abstractions
{
    public interface IBlogService
    {
        Task<int> CreateBlogAsync(BlogModel blogmodel);
        Task<BlogModel> GetBlogAsync(int id);
        Task<List<BlogModel>> GetBlogsAsync();
        Task<BlogModel> UpdateBlogAsync(BlogModel blogModel);
        Task<bool> DeleteBlogAsync(int id);
    }
}