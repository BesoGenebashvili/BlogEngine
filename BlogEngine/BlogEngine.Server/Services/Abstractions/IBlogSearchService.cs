using BlogEngine.Shared.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogEngine.Server.Services.Abstractions
{
    public interface IBlogSearchService
    {
        Task<List<BlogDTO>> SearchAsync(BlogSearchDTO blogSearchDTO);
    }
}