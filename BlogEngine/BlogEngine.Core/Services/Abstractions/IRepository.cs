using System;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace BlogEngine.Core.Services.Abstractions
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity GetById(object id);
        Task<TEntity> GetByIdAsync(object id);
        IEnumerable<TEntity> GetAll();
        Task<IEnumerable<TEntity>> GetAllAsync();
        IEnumerable<TEntity> GetByQuery(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null);
        Task<IEnumerable<TEntity>> GetByQueryAsync(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null);
        IEnumerable<TEntity> GetByRawSql(string query, params object[] parameters);
        Task<IEnumerable<TEntity>> GetByRawSqlAsync(string query, params object[] parameters);
        TEntity Insert(TEntity entity);
        Task<TEntity> InsertAsync(TEntity entity);
        TEntity Update(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        bool Delete(object id);
        Task<bool> DeleteAsync(object id);
    }
}