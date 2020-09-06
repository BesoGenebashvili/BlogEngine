using BlogEngine.ClientServices.Extensions;
using BlogEngine.ClientServices.Services.Abstractions;
using BlogEngine.Shared.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogEngine.ClientServices.Services.Implementations
{
    public class CommentClient : ICommentClient
    {
        private readonly IHttpService _httpService;
        private const string MainUrl = "api/Comments/main";
        private const string SubUrl = "api/Comments/sub";

        public CommentClient(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<List<MainCommentDTO>> GetMainCommentsByBlogIdAsync(int id)
        {
            return await _httpService.GetHelperAsync<List<MainCommentDTO>>($"{MainUrl}/blog/{id}");
        }

        public async Task<List<SubCommentDTO>> GetSubCommentsByBlogIdAsync(int id)
        {
            return await _httpService.GetHelperAsync<List<SubCommentDTO>>($"{SubUrl}/blog/{id}");
        }

        public async Task<MainCommentDTO> GetMainCommentByIdAsync(int id)
        {
            return await _httpService.GetHelperAsync<MainCommentDTO>($"{MainUrl}/{id}");
        }

        public async Task<SubCommentDTO> GetSubCommentByIdAsync(int id)
        {
            return await _httpService.GetHelperAsync<SubCommentDTO>($"{SubUrl}/{id}");
        }

        public async Task<MainCommentDTO> InsertMainCommentAsync(CommentCreationDTO commentCreationDTO)
        {
            return await _httpService.PostHelperAsync<CommentCreationDTO, MainCommentDTO>(MainUrl, commentCreationDTO);
        }

        public async Task<SubCommentDTO> InsertSubCommentAsync(CommentCreationDTO commentCreationDTO)
        {
            return await _httpService.PostHelperAsync<CommentCreationDTO, SubCommentDTO>(SubUrl, commentCreationDTO);
        }

        public async Task<bool> DeleteMainCommentAsync(int id)
        {
            return await _httpService.DeleteHelperAsync<bool>($"{MainUrl}/{id}");
        }

        public async Task<bool> DeleteSubCommentAsync(int id)
        {
            return await _httpService.DeleteHelperAsync<bool>($"{SubUrl}/{id}");
        }
    }
}