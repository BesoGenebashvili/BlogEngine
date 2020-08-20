using System.Collections.Generic;
using System.Threading.Tasks;
using BlogEngine.Core.Data.Entities;

namespace BlogEngine.Core.Services.Abstractions
{
    public interface ICommentRepository
    {
        Task<MainComment> GetMainCommentByIdAsync(int id);
        Task<SubComment> GetSubCommentByIdAsync(int id);
        Task<IEnumerable<MainComment>> GetMainCommentsByBlogIdAsync(int id);
        Task<IEnumerable<SubComment>> GetSubCommentsByBlogIdAsync(int id);
        Task<MainComment> InsertMainCommentAsync(MainComment mainComment);
        Task<SubComment> InsertSubCommentAsync(SubComment subComment);
        Task<bool> DeleteMainCommentAsync(int id);
        Task<bool> DeleteSubCommentAsync(int id);
    }
}