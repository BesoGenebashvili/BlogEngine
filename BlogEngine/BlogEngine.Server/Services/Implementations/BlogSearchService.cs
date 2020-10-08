using AutoMapper;
using BlogEngine.Core.Data.Entities;
using BlogEngine.Core.Helpers;
using BlogEngine.Core.Services.Abstractions;
using BlogEngine.Server.Services.Abstractions;
using BlogEngine.Shared.DTOs;
using BlogEngine.Shared.Helpers;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogEngine.Server.Services.Implementations
{
    public class BlogSearchService : IBlogSearchService
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IMapper _mapper;

        public BlogSearchService(IBlogRepository blogRepository, IMapper mapper)
        {
            _blogRepository = blogRepository;
            _mapper = mapper;
        }

        public async Task<List<BlogDTO>> SearchAsync(BlogSearchDTO blogSearchDTO)
        {
            if (blogSearchDTO == null)
            {
                Throw.ArgumentNullException(nameof(blogSearchDTO));
            }

            var blogs = await _blogRepository.GetAllWithReferences();

            if (!string.IsNullOrWhiteSpace(blogSearchDTO.Title))
            {
                blogs = blogs.Where(b => b.Title.Contains(blogSearchDTO.Title, System.StringComparison.OrdinalIgnoreCase));
            }

            if (blogSearchDTO.CategoryID.HasValue)
            {
                blogs = blogs.Where(b => b.BlogCategories
                                          .Any(bc => bc.CategoryID.Equals(blogSearchDTO.CategoryID.Value)));
            }

            blogs = OrderBlogs(blogs, blogSearchDTO.SortOrder, blogSearchDTO.BlogOrderBy);

            return _mapper.Map<List<BlogDTO>>(blogs);
        }


        protected IEnumerable<Blog> OrderBlogs(IEnumerable<Blog> blogs, SortOrder sortOrder, BlogOrderBy blogOrderBy)
        {
            switch (sortOrder)
            {
                case SortOrder.Ascending: return OrderByAscending(blogs, blogOrderBy);
                case SortOrder.Descending: return OrderByDescending(blogs, blogOrderBy);
                default: return OrderByDescending(blogs, blogOrderBy);
            }
        }

        protected IEnumerable<Blog> OrderByAscending(IEnumerable<Blog> blogs, BlogOrderBy blogOrderBy)
        {
            switch (blogOrderBy)
            {
                case BlogOrderBy.DateCreated: return blogs.OrderBy(b => b.DateCreated);
                case BlogOrderBy.LastUpdateDate: return blogs.OrderBy(b => b.LastUpdateDate);
                default: return blogs;
            }
        }

        protected IEnumerable<Blog> OrderByDescending(IEnumerable<Blog> blogs, BlogOrderBy blogOrderBy)
        {
            switch (blogOrderBy)
            {
                case BlogOrderBy.DateCreated: return blogs.OrderByDescending(b => b.DateCreated);
                case BlogOrderBy.LastUpdateDate: return blogs.OrderByDescending(b => b.LastUpdateDate);
                default: return blogs;
            }
        }
    }
}