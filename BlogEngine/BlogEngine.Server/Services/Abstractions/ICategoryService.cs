using BlogEngine.Shared.DTOs;
using System.Threading.Tasks;

namespace BlogEngine.Server.Services.Abstractions
{
    public interface ICategoryService : IDataServiceBase<CategoryDTO, CategoryCreationDTO, CategoryUpdateDTO>
    {
        Task<CategoryEditPageDTO> GetEditPageDTOAsync(int id);
    }
}