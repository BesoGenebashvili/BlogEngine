using BlogEngine.ClientServices.Extensions;
using BlogEngine.ClientServices.Services.Abstractions;
using BlogEngine.Shared.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogEngine.ClientServices.Services.Implementations
{
    public class CategoryClient : ICategoryClient
    {
        private readonly IHttpService _httpService;
        private const string Url = "api/Categories";

        public CategoryClient(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<CategoryDTO> GetAsync(int id)
        {
            return await _httpService.GetHelperAsync<CategoryDTO>($"{Url}/{id}");
        }

        public async Task<CategoryEditPageDTO> GetEditPageDTOAsync(int id)
        {
            return await _httpService.GetHelperAsync<CategoryEditPageDTO>($"{Url}/edit/{id}");
        }

        public async Task<List<CategoryDTO>> GetAllAsync()
        {
            return await _httpService.GetHelperAsync<List<CategoryDTO>>(Url);
        }

        public async Task<CategoryDTO> CreateAsync(CategoryCreationDTO categoryCreationDTO)
        {
            return await _httpService.PostHelperAsync<CategoryCreationDTO, CategoryDTO>(Url, categoryCreationDTO);
        }

        public async Task<CategoryDTO> UpdateAsync(int id, CategoryUpdateDTO categoryUpdateDTO)
        {
            return await _httpService.PutHelperAsync<CategoryUpdateDTO, CategoryDTO>($"{Url}/{id}", categoryUpdateDTO);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _httpService.DeleteHelperAsync<bool>($"{Url}/{id}");
        }
    }
}