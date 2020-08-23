using BlogEngine.Shared.DTOs;
using System.Threading.Tasks;

namespace BlogEngine.ClientServices.Services.Abstractions
{
    public interface ICommentClient
    {
        Task<MainCommentDTO> GetMainCommentByIdAsync(int id);
        Task<SubCommentDTO> GetSubCommentByIdAsync(int id);
        Task<MainCommentDTO> InsertMainCommentAsync(CommentCreationDTO commentCreationDTO);
        Task<SubCommentDTO> InsertSubCommentAsync(CommentCreationDTO commentCreationDTO);
        Task<bool> DeleteMainCommentAsync(int id);
        Task<bool> DeleteSubCommentAsync(int id);
    }
}