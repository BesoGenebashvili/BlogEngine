using BlogEngine.Shared.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogEngine.Server.Services.Abstractions
{
    public interface ICommentService
    {
        Task<MainCommentDTO> GetMainCommentByIdAsync(int id);
        Task<SubCommentDTO> GetSubCommentByIdAsync(int id);
        Task<List<MainCommentDTO>> GetMainCommentsByBlogIdAsync(int id);
        Task<List<SubCommentDTO>> GetSubCommentsByBlogIdAsync(int id);
        Task<MainCommentDTO> InsertMainCommentAsync(CommentCreationDTO commentCreationDTO);
        Task<SubCommentDTO> InsertSubCommentAsync(CommentCreationDTO commentCreationDTO);
        Task<bool> DeleteMainCommentAsync(int id);
        Task<bool> DeleteSubCommentAsync(int id);
    }
}