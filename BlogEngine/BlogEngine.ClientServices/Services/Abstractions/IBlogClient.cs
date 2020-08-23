using BlogEngine.Shared.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogEngine.ClientServices.Services.Abstractions
{
    public interface IBlogClient
    {
        Task<BlogDTO> GetAsync(int id);
        Task<List<BlogDTO>> GetAllAsync();
        Task<BlogUpdateDTO> GetUpdateDTOAsync(int id);
        Task<BlogDTO> CreateAsync(BlogCreationDTO blogCreationDTO);
        Task<BlogDTO> UpdateAsync(int id ,BlogUpdateDTO blogUpdateDTO);
        Task<bool> DeleteAsync(int id);
    }
}