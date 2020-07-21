using BlogEngine.Core.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogEngine.Core.Services.Abstractions
{
    public interface IBlogRepository : IRepository<Blog>
    {
        Task<IEnumerable<Blog>> GetAllWithAllReferenceEntityes();
    }
}