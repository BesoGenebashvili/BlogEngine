using BlogEngine.Shared.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogEngine.Server.Services.Abstractions
{
    public interface IBlogService : IDataServiceBase<BlogDTO, BlogCreationDTO, BlogUpdateDTO>
    {
        Task<BlogEditPageDTO> GetEditPageDTOAsync(int id);
        Task<List<BlogDTO>> GetAllByUserIdAsync(int id);
    }
}