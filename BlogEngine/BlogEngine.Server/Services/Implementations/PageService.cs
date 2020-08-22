using System.Linq;
using BlogEngine.Server.Services.Abstractions;
using BlogEngine.Shared.DTOs;
using System.Threading.Tasks;

namespace BlogEngine.Server.Services.Implementations
{
    public class PageService : IPageService
    {
        private readonly IBlogService _blogService;
        private readonly ICategoryService _categoryService;

        public PageService(IBlogService blogService, ICategoryService categoryService)
        {
            _blogService = blogService;
            _categoryService = categoryService;
        }

        public virtual async Task<IndexPageDTO> GetIndexPageDTOAsync()
        {
            var blogDTOs = (await _blogService.GetAllAsync());

            var newBlogDTOs = blogDTOs
                .OrderByDescending(b => b.DateCreated)
                .ThenByDescending(b => b.LastUpdateDate)
                .Take(10)
                .ToList();

            var featuredCategoryDTOs = blogDTOs
                .SelectMany(b => b.CategoryDTOs)
                .GroupBy(c => c.ID)
                .OrderByDescending(c => c.Count())
                .SelectMany(g => g)
                .Take(10)
                .ToList();

            return new IndexPageDTO()
            {
                NewBlogDTOs = newBlogDTOs,
                FeaturedCategoryDTOs = featuredCategoryDTOs
            };
        }
    }
}