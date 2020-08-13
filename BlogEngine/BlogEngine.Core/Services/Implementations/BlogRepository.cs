using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BlogEngine.Core.Data.DatabaseContexts;
using BlogEngine.Core.Data.Entities;
using BlogEngine.Core.Services.Abstractions;
using BlogEngine.Core.Helpers;
using BlogEngine.Core.Data.Entities.JoiningEntities;

namespace BlogEngine.Core.Services.Implementations
{
    public class BlogRepository : Repository<Blog>, IBlogRepository
    {
        private readonly ApplicationDbContext _context;

        public BlogRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public Task<IEnumerable<Blog>> GetAllWithReferences()
        {
            return Task.FromResult(_context.Blogs
                 .Include(b => b.BlogCategories).ThenInclude(bc => bc.Category)
                 .Include(b => b.BlogComments).ThenInclude(bc => bc.Comment)
                 .AsEnumerable());
        }

        public override Blog GetById(int id)
        {
            return _context.Blogs
                .Where(b => b.ID.Equals(id))
                .Include(b => b.BlogCategories).ThenInclude(bc => bc.Category)
                .Include(b => b.BlogComments).ThenInclude(bc => bc.Comment)
                .FirstOrDefault();
        }

        public override async Task<Blog> GetByIdAsync(int id)
        {
            return await _context.Blogs
                .Where(b => b.ID.Equals(id))
                .Include(b => b.BlogCategories).ThenInclude(bc => bc.Category)
                .Include(b => b.BlogComments).ThenInclude(bc => bc.Comment)
                .FirstOrDefaultAsync();
        }

        public async Task<Comment> AddCommentAsync(int id, Comment comment)
        {
            var entityFromDb = await GetByIdAsync(id);

            NullCheckThrowNotFoundException(entityFromDb);

            entityFromDb.BlogComments.Add(new BlogComment() { Comment = comment });

            await SaveChangesAsync();

            return comment;
        }

        public async Task<Comment> EditCommentAsync(Comment comment)
        {
            var entityFromDb = await _context.Comments.FindAsync(comment.ID);

            if (entityFromDb == null)
            {
                ThrowHelper.ThrowEntityNotFoundException(nameof(Comment));
            }

            _context.Comments.Update(comment);

            await SaveChangesAsync();

            return comment;
        }

        public async Task<IEnumerable<Comment>> GetAllCommentsByBlogIdAsync(int id)
        {
            var entities = await GetByIdAsync(id);
            return entities.BlogComments.Select(bc => bc.Comment);
        }

        public async Task<bool> RemoveCommentAsync(int commentId)
        {
            var entityFromDb = await _context.Comments.FindAsync(commentId);

            if (entityFromDb == null)
            {
                ThrowHelper.ThrowEntityNotFoundException(nameof(Comment));
            }

            _context.Comments.Remove(entityFromDb);

            return await SaveChangesAsync();
        }
    }
}