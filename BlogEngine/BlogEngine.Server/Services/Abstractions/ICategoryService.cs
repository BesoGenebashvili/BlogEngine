using BlogEngine.Shared.DTOs.Category;
using System.Threading.Tasks;

namespace BlogEngine.Server.Services.Abstractions
{
    public interface ICategoryService : IDataServiceBase<CategoryDTO, CategoryCreationDTO, CategoryUpdateDTO>
    {
        Task<CategoryEditPageDTO> GetEditPageDTOAsync(int id);
    }
}