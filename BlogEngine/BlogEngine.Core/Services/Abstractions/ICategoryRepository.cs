using BlogEngine.Core.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogEngine.Core.Services.Abstractions
{
    public interface ICategoryRepository : IRepository<Category>, IAsyncRepository<Category>
    {
        Task<IEnumerable<Category>> GetAllWithReferences();
    }
}