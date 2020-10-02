using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using BlogEngine.Core.Data.Entities;

namespace BlogEngine.Core.Services.Abstractions
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        TEntity GetById(int id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetByQuery(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null);
        IEnumerable<TEntity> GetByRawSql(string query, params object[] parameters);
        TEntity Insert(TEntity entity);
        TEntity Update(TEntity entity);
        bool Delete(int id);
    }
}