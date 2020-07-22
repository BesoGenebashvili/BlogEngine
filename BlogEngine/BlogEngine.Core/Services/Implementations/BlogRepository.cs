using BlogEngine.Core.Data.DatabaseContexts;
using BlogEngine.Core.Data.Entities;
using BlogEngine.Core.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogEngine.Core.Services.Implementations
{
    public class BlogRepository : Repository<Blog>, IBlogRepository
    {
        private readonly ApplicationDbContext _context;

        public BlogRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public Task<IEnumerable<Blog>> GetAllWithAllReferenceEntityes()
        {
            return Task.FromResult(_context.Blogs
                 .Include(b => b.BlogGenres)
                 .Include(b => b.BlogComments)
                 .AsEnumerable());
        }

        public override Blog GetById(object id)
        {
            return _context.Blogs
                .Where(b => b.ID.Equals(id))
                .Include(b => b.BlogGenres)
                .Include(b => b.BlogComments)
                .FirstOrDefault();
        }

        public async override Task<Blog> GetByIdAsync(object id)
        {
            return await _context.Blogs
                .Where(b => b.ID.Equals(id))
                .Include(b => b.BlogGenres)
                .Include(b => b.BlogComments)
                .FirstOrDefaultAsync();
        }
    }
}