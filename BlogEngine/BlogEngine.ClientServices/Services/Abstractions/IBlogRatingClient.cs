using BlogEngine.Shared.DTOs;
using System.Threading.Tasks;

namespace BlogEngine.ClientServices.Services.Abstractions
{
    public interface IBlogRatingClient
    {
        Task CreateAsync(BlogRatingDTO blogRatingDTO);
    }
}