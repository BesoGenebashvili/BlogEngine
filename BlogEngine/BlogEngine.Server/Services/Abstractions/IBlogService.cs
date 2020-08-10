using BlogEngine.Shared.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogEngine.Server.Services.Abstractions
{
    public interface IBlogService
    {
        Task<BlogDTO> GetByIdAsync(int id);
        Task<List<BlogDTO>> GetAllAsync();
        Task<BlogDTO> InsertAsync(BlogCreationDTO blogCreationDTO);
        Task<BlogDTO> UpdateAsync(int id, BlogUpdateDTO blogUpdateDTO);
        Task<bool> DeleteAsync(int id);
        Task<BlogUpdateDTO> GetUpdateDTOAsync(int id);
    }
}