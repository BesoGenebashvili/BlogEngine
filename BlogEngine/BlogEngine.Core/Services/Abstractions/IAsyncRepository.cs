using System.Collections.Generic;
using System.Threading.Tasks;
using BlogEngine.Core.Data.Entities;

namespace BlogEngine.Core.Services.Abstractions
{
    public interface IAsyncRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> GetByIdAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetByRawSqlAsync(string query, params object[] parameters);
        Task<TEntity> InsertAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(int id);
    }
}