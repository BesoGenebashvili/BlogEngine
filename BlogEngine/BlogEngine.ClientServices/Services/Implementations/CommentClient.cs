using BlogEngine.ClientServices.Extensions;
using BlogEngine.ClientServices.Helpers;
using BlogEngine.ClientServices.Services.Abstractions;
using BlogEngine.Shared.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogEngine.ClientServices.Services.Implementations
{
    public class CommentClient : ICommentClient
    {
        private readonly IHttpService _httpService;

        public CommentClient(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<List<MainCommentDTO>> GetMainCommentsByBlogIdAsync(int id)
        {
            return await _httpService.GetHelperAsync<List<MainCommentDTO>>($"{CommentClientEndpoints.MainWithBlog}/{id}");
        }

        public async Task<List<SubCommentDTO>> GetSubCommentsByBlogIdAsync(int id)
        {
            return await _httpService.GetHelperAsync<List<SubCommentDTO>>($"{CommentClientEndpoints.SubWithBlog}/{id}");
        }

        public async Task<MainCommentDTO> GetMainCommentByIdAsync(int id)
        {
            return await _httpService.GetHelperAsync<MainCommentDTO>($"{CommentClientEndpoints.MainBase}/{id}");
        }

        public async Task<SubCommentDTO> GetSubCommentByIdAsync(int id)
        {
            return await _httpService.GetHelperAsync<SubCommentDTO>($"{CommentClientEndpoints.SubBase}/{id}");
        }

        public async Task<MainCommentDTO> InsertMainCommentAsync(CommentCreationDTO commentCreationDTO)
        {
            return await _httpService.PostHelperAsync<CommentCreationDTO, MainCommentDTO>(CommentClientEndpoints.MainBase, commentCreationDTO);
        }

        public async Task<SubCommentDTO> InsertSubCommentAsync(CommentCreationDTO commentCreationDTO)
        {
            return await _httpService.PostHelperAsync<CommentCreationDTO, SubCommentDTO>(CommentClientEndpoints.SubBase, commentCreationDTO);
        }

        public async Task<bool> DeleteMainCommentAsync(int id)
        {
            return await _httpService.DeleteHelperAsync<bool>($"{CommentClientEndpoints.MainBase}/{id}");
        }

        public async Task<bool> DeleteSubCommentAsync(int id)
        {
            return await _httpService.DeleteHelperAsync<bool>($"{CommentClientEndpoints.SubBase}/{id}");
        }
    }
}