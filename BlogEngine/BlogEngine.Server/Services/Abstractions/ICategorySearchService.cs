using System.Threading.Tasks;
using System.Collections.Generic;
using BlogEngine.Shared.DTOs.Category;

namespace BlogEngine.Server.Services.Abstractions
{
    public interface ICategorySearchService
    {
        Task<List<CategoryDTO>> SearchAsync(CategorySearchDTO categorySearchDTO);
    }
}