using BlogEngine.Core.Data.DatabaseContexts;
using BlogEngine.Core.Data.Entities;
using BlogEngine.Core.Data.Entities.JoiningEntities;
using BlogEngine.Core.Helpers;
using BlogEngine.Core.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogEngine.Core.Services.Implementations
{
    public class BlogCommentRepository : Repository<BlogComment>, IBlogCommentRepository
    {
        private readonly ApplicationDbContext _context;

        public BlogCommentRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public Task<IEnumerable<Comment>> GetAllCommentsByBlogId(int blogId)
        {
            return Task.FromResult(
                _context.BlogComments
                .Where(bc => bc.BlogID == blogId)
                .Select(bc => bc.Comment)
                .AsEnumerable());
        }

        public async Task<Comment> AddComment(int blogId, Comment comment)
        {
            if (comment == null)
            {
                ThrowHelper.ThrowArgumentNullException(nameof(comment));
            }

            var blogEntity = await _context.Blogs
                .Where(b => b.ID == blogId)
                .Include(b => b.BlogComments)
                .ThenInclude(b => b.Comment)
                .FirstOrDefaultAsync();

            if (blogEntity == null)
            {
                ThrowHelper.ThrowEntityNotFoundException(nameof(Blog));
            }

            var blogComment = new BlogComment()
            {
                Comment = comment
            };

            blogEntity.BlogComments.Add(new BlogComment() { Comment = comment });

            await SaveChangesAsync();

            return comment;
        }

        public async Task<Comment> EditComment(Comment comment)
        {
            if (comment == null)
            {
                ThrowHelper.ThrowArgumentNullException(nameof(comment));
            }

            var entityFromDb = await _context.Comments.FindAsync(comment.ID);

            if (entityFromDb == null)
            {
                ThrowHelper.ThrowEntityNotFoundException(nameof(Comment));
            }

            _context.Comments.Update(comment);

            await SaveChangesAsync();

            return comment;
        }

        public async Task<bool> RemoveComment(int id)
        {
            var commentEntity = await _context.Comments
                .Where(c => c.ID == id)
                .FirstOrDefaultAsync();

            if (commentEntity == null)
            {
                ThrowHelper.ThrowEntityNotFoundException(nameof(Comment));
            }

            _context.Comments.Remove(commentEntity);

            return await SaveChangesAsync();
        }
    }
}
