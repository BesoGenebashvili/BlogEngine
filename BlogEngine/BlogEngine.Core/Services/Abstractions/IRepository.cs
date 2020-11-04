using System.Collections.Generic;
using BlogEngine.Core.Data.Entities.Common;

namespace BlogEngine.Core.Services.Abstractions
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        TEntity GetById(int id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetByRawSql(string query, params object[] parameters);
        TEntity Insert(TEntity entity);
        TEntity Update(TEntity entity);
        bool Delete(int id);
    }
}