using BlogEngine.Shared.DTOs.Blog;
using System.Threading.Tasks;

namespace BlogEngine.Server.Services.Abstractions
{
    public interface IBlogRatingService
    {
        Task InsertAsync(BlogRatingDTO blogRatingDTO);
    }
}