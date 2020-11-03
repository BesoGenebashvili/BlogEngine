using BlogEngine.ClientServices.Common.Models;
using BlogEngine.Shared.DTOs;
using BlogEngine.Shared.DTOs.Blog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogEngine.ClientServices.Services.Abstractions
{
    public interface IBlogClient
    {
        Task<BlogDTO> GetAsync(int id);
        Task<List<BlogDTO>> GetAllByUserIdAsync(int id);
        Task<PaginatedResponse<List<BlogDTO>>> GetAllByUserIdPaginatedAsync(int id, PaginationDTO paginationDTO);
        Task<List<BlogDTO>> GetAllAsync();
        Task<PaginatedResponse<List<BlogDTO>>> GetAllPaginatedAsync(PaginationDTO paginationDTO);
        Task<BlogEditPageDTO> GetEditPageDTOAsync(int id);
        Task<List<BlogDTO>> SearchAsync(BlogSearchDTO blogSearchDTO);
        Task<BlogDTO> CreateAsync(BlogCreationDTO blogCreationDTO);
        Task<BlogDTO> UpdateAsync(int id, BlogUpdateDTO blogUpdateDTO);
        Task<bool> DeleteAsync(int id);
    }
}