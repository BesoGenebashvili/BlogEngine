using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BlogEngine.Core.Data.DatabaseContexts;
using BlogEngine.Core.Data.Entities;
using BlogEngine.Core.Services.Abstractions;

namespace BlogEngine.Core.Services.Implementations
{
    public class BlogRatingRepository : Repository<BlogRating>, IBlogRatingRepository
    {
        private readonly ApplicationDbContext _context;

        public BlogRatingRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public Task<IEnumerable<BlogRating>> GetAllWithReferences()
        {
            return Task.FromResult(_context.BlogRatings
                .Include(br => br.Blog)
                .AsEnumerable());
        }

        public override BlogRating GetById(int id)
        {
            return _context.BlogRatings
                .Where(br => br.ID.Equals(id))
                .Include(br => br.Blog)
                .FirstOrDefault();
        }

        public override async Task<BlogRating> GetByIdAsync(int id)
        {
            return await _context.BlogRatings
                .Where(br => br.ID.Equals(id))
                .Include(br => br.Blog)
                .FirstOrDefaultAsync();
        }

        public Task<BlogRating> GetByBlogIdAndUserIdAsync(int blogId, int userId)
        {
            return _context.BlogRatings
                .Where(br => br.BlogID.Equals(blogId) && br.ApplicationUserID.Equals(userId))
                .Include(br => br.Blog)
                .FirstOrDefaultAsync();
        }

        public Task<IEnumerable<BlogRating>> GetAllByBlogIdAsync(int id)
        {
            return Task.FromResult(_context.BlogRatings
                .Where(br => br.BlogID.Equals(id))
                .Include(br => br.Blog)
                .AsEnumerable());
        }
    }
}