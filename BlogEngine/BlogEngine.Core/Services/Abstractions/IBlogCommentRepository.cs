using BlogEngine.Core.Data.Entities;
using BlogEngine.Core.Data.Entities.JoiningEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogEngine.Core.Services.Abstractions
{
    public interface IBlogCommentRepository : IRepository<BlogComment>
    {
        Task<IEnumerable<Comment>> GetAllCommentsByBlogId(int blogId);
        Task<Comment> AddComment(int blogId, Comment comment);
        Task<Comment> EditComment(Comment comment);
        Task<bool> RemoveComment(int commentId);
    }
}