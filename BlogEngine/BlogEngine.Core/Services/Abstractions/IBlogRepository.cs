using System.Collections.Generic;
using System.Threading.Tasks;
using BlogEngine.Core.Data.Entities;

namespace BlogEngine.Core.Services.Abstractions
{
    public interface IBlogRepository : IRepository<Blog>
    {
        Task<IEnumerable<Blog>> GetAllWithReferences();
        Task<IEnumerable<Comment>> GetAllCommentsByBlogIdAsync(int id);
        Task<Comment> AddCommentAsync(int id, Comment comment);
        Task<Comment> EditCommentAsync(Comment comment);
        Task<bool> RemoveCommentAsync(int commentId);
    }
}