using BlogEngine.Shared.DTOs.Blog;
using System.Threading.Tasks;

namespace BlogEngine.ClientServices.Services.Abstractions
{
    public interface IBlogRatingClient
    {
        Task CreateAsync(BlogRatingDTO blogRatingDTO);
    }
}