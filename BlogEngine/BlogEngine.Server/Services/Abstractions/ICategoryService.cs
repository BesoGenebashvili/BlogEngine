using BlogEngine.Shared.DTOs;

namespace BlogEngine.Server.Services.Abstractions
{
    public interface ICategoryService : IDataServiceBase<CategoryDTO, CategoryCreationDTO, CategoryUpdateDTO>
    {
    }
}