using BlogEngine.ClientServices.Services.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlogEngine.Shared.DTOs;
using BlogEngine.ClientServices.Common.Endpoints;
using BlogEngine.ClientServices.Common.Models;
using BlogEngine.ClientServices.Common.Extensions;
using BlogEngine.Shared.DTOs.Blog;

namespace BlogEngine.ClientServices.Services.Implementations
{
    public class BlogClient : IBlogClient
    {
        private readonly IHttpService _httpService;

        public BlogClient(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<BlogDTO> GetAsync(int id)
        {
            return await _httpService.GetHelperAsync<BlogDTO>($"{BlogClientEndpoints.Base}/{id}");
        }

        public async Task<List<BlogDTO>> GetAllByUserIdAsync(int id)
        {
            return await _httpService.GetHelperAsync<List<BlogDTO>>($"{BlogClientEndpoints.ByUserId}/{id}");
        }

        public async Task<PaginatedResponse<List<BlogDTO>>> GetAllByUserIdPaginatedAsync(int id, PaginationDTO paginationDTO)
        {
            return await _httpService.GetHelperAsync<List<BlogDTO>>($"{BlogClientEndpoints.ByUserId}/{id}", paginationDTO);
        }

        public async Task<List<BlogDTO>> GetAllAsync()
        {
            return await _httpService.GetHelperAsync<List<BlogDTO>>(BlogClientEndpoints.Base);
        }

        public async Task<PaginatedResponse<List<BlogDTO>>> GetAllPaginatedAsync(PaginationDTO paginationDTO)
        {
            return await _httpService.GetHelperAsync<List<BlogDTO>>(BlogClientEndpoints.Base, paginationDTO);
        }

        public async Task<BlogEditPageDTO> GetEditPageDTOAsync(int id)
        {
            return await _httpService.GetHelperAsync<BlogEditPageDTO>($"{BlogClientEndpoints.Edit}/{id}");
        }

        public async Task<List<BlogDTO>> SearchAsync(BlogSearchDTO blogSearchDTO)
        {
            string blogSearchDTOQueryString = blogSearchDTO.ToQueryString();

            return await _httpService.GetHelperAsync<List<BlogDTO>>($"{BlogClientEndpoints.Search}?{blogSearchDTOQueryString}");
        }

        public async Task<BlogDTO> CreateAsync(BlogCreationDTO blogCreationDTO)
        {
            return await _httpService.PostHelperAsync<BlogCreationDTO, BlogDTO>(BlogClientEndpoints.Base, blogCreationDTO);
        }

        public async Task<BlogDTO> UpdateAsync(int id, BlogUpdateDTO blogUpdateDTO)
        {
            return await _httpService.PutHelperAsync<BlogUpdateDTO, BlogDTO>($"{BlogClientEndpoints.Base}/{id}", blogUpdateDTO);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _httpService.DeleteHelperAsync<bool>($"{BlogClientEndpoints.Base}/{id}");
        }
    }
}