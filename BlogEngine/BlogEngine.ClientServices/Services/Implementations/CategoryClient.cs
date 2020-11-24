using BlogEngine.ClientServices.Common.Endpoints;
using BlogEngine.ClientServices.Common.Extensions;
using BlogEngine.ClientServices.Services.Abstractions;
using BlogEngine.Shared.DTOs.Category;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogEngine.ClientServices.Services.Implementations
{
    public class CategoryClient : ICategoryClient
    {
        private readonly IHttpService _httpService;

        public CategoryClient(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<CategoryDTO> GetAsync(int id)
        {
            return await _httpService.GetHelperAsync<CategoryDTO>($"{CategoryClientEndpoints.Base}/{id}");
        }

        public async Task<CategoryEditPageDTO> GetEditPageDTOAsync(int id)
        {
            return await _httpService.GetHelperAsync<CategoryEditPageDTO>($"{CategoryClientEndpoints.Edit}/{id}");
        }

        public async Task<List<CategoryDTO>> GetAllAsync()
        {
            return await _httpService.GetHelperAsync<List<CategoryDTO>>(CategoryClientEndpoints.Base);
        }

        public async Task<List<CategoryDTO>> SearchAsync(CategorySearchDTO categorySearchDTO)
        {
            return await _httpService.GetHelperAsync<List<CategoryDTO>>($"{CategoryClientEndpoints.Search}?{categorySearchDTO.ToQueryString()}");
        }

        public async Task<CategoryDTO> CreateAsync(CategoryCreationDTO categoryCreationDTO)
        {
            return await _httpService.PostHelperAsync<CategoryCreationDTO, CategoryDTO>(CategoryClientEndpoints.Base, categoryCreationDTO);
        }

        public async Task<CategoryDTO> UpdateAsync(int id, CategoryUpdateDTO categoryUpdateDTO)
        {
            return await _httpService.PutHelperAsync<CategoryUpdateDTO, CategoryDTO>($"{CategoryClientEndpoints.Base}/{id}", categoryUpdateDTO);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _httpService.DeleteHelperAsync<bool>($"{CategoryClientEndpoints.Base}/{id}");
        }
    }
}