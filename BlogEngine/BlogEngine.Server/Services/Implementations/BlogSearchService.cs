using AutoMapper;
using BlogEngine.Core.Services.Abstractions;
using BlogEngine.Server.Services.Abstractions;
using BlogEngine.Shared.DTOs;
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
            var blogs = await _blogRepository.GetAllWithReferences();

            if (!string.IsNullOrWhiteSpace(blogSearchDTO.Title))
            {
                blogs = blogs.Where(b => b.Title.Contains(blogSearchDTO.Title, System.StringComparison.OrdinalIgnoreCase));
            }

            return _mapper.Map<List<BlogDTO>>(blogs);
        }
    }
}