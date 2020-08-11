using BlogEngine.Core.Data.DatabaseContexts;
using BlogEngine.Core.Data.Entities;
using BlogEngine.Core.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogEngine.Core.Services.Implementations
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public Task<IEnumerable<Category>> GetAllWithReferences()
        {
            return Task.FromResult(_context.Categories
                 .Include(c => c.BlogCategories).ThenInclude(bc => bc.Blog)
                 .AsEnumerable());
        }

        public override Category GetById(int id)
        {
            return _context.Categories
                .Where(c => c.ID.Equals(id))
                .Include(c => c.BlogCategories).ThenInclude(bc => bc.Blog)
                .FirstOrDefault();
        }

        public override async Task<Category> GetByIdAsync(int id)
        {
            return await _context.Categories
                .Where(c => c.ID.Equals(id))
                .Include(c => c.BlogCategories).ThenInclude(bc => bc.Blog)
                .FirstOrDefaultAsync();
        }
    }
}