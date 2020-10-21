using BlogEngine.Core.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogEngine.Core.Services.Abstractions
{
    public interface IBlogRatingRepository : IRepository<BlogRating>, IAsyncRepository<BlogRating>
    {
        Task<IEnumerable<BlogRating>> GetAllWithReferences();
        Task<IEnumerable<BlogRating>> GetAllByBlogIdAsync(int id);
        Task<BlogRating> GetByBlogIdAndUserIdAsync(int blogId, int userId);
    }
}