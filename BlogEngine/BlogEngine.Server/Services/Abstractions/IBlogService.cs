using BlogEngine.Shared.DTOs;
using System.Threading.Tasks;

namespace BlogEngine.Server.Services.Abstractions
{
    public interface IBlogService : IDataServiceBase<BlogDTO, BlogCreationDTO, BlogUpdateDTO>
    {
        Task<BlogEditPageDTO> GetEditPageDTOAsync(int id);
    }
}