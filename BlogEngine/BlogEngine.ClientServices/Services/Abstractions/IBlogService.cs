using BlogEngine.Shared.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogEngine.ClientServices.Services.Abstractions
{
    public interface IBlogService
    {
        Task<BlogDTO> CreateBlogAsync(BlogCreationDTO blogCreationDTO);
        Task<BlogDTO> GetBlogAsync(int id);
        Task<List<BlogDTO>> GetBlogsAsync();
        Task<BlogDTO> UpdateBlogAsync(int id ,BlogUpdateDTO blogUpdateDTO);
        Task<bool> DeleteBlogAsync(int id);
        Task<BlogUpdateDTO> GetBlogUpdateDTOAsync(int id);
    }
}