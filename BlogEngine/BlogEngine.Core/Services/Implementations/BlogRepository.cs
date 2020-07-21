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
    }
}