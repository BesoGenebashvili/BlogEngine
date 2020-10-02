using System.Collections.Generic;
using System.Threading.Tasks;
using BlogEngine.Core.Data.Entities;

namespace BlogEngine.Core.Services.Abstractions
{
    public interface IBlogRepository : IRepository<Blog>, IAsyncRepository<Blog>
    {
        Task<IEnumerable<Blog>> GetAllWithReferences();
    }
}