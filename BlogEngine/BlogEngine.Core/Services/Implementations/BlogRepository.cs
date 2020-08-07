using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BlogEngine.Core.Data.DatabaseContexts;
using BlogEngine.Core.Data.Entities;
using BlogEngine.Core.Services.Abstractions;

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
                 .Include(b => b.BlogCategories)
                 .Include(b => b.BlogComments)
                 .AsEnumerable());
        }

        public override Blog GetById(int id)
        {
            return _context.Blogs
                .Where(b => b.ID.Equals(id))
                .Include(b => b.BlogCategories)
                .Include(b => b.BlogComments)
                .FirstOrDefault();
        }

        public async override Task<Blog> GetByIdAsync(int id)
        {
            return await _context.Blogs
                .Where(b => b.ID.Equals(id))
                .Include(b => b.BlogCategories)
                .Include(b => b.BlogComments)
                .FirstOrDefaultAsync();
        }
    }
}