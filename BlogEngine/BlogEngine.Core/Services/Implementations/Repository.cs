using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Linq;
using BlogEngine.Core.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
using BlogEngine.Core.Helpers;

namespace BlogEngine.Core.Services.Implementations
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public virtual TEntity GetById(int id)
        {
            return Find(id);
        }

        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return _dbSet.AsEnumerable();
        }

        public virtual Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return Task.FromResult(_dbSet.AsEnumerable());
        }

        public virtual IEnumerable<TEntity> GetByQuery(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return query.AsEnumerable();
        }

        public virtual Task<IEnumerable<TEntity>> GetByQueryAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            return Task.FromResult(GetByQuery(filter, orderBy));
        }

        public virtual IEnumerable<TEntity> GetByRawSql(string query, params object[] parameters)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                ThrowHelper.ThrowArgumentNullException(nameof(query));
            }

            return _dbSet.FromSqlRaw(query, parameters);
        }

        public virtual Task<IEnumerable<TEntity>> GetByRawSqlAsync(string query, params object[] parameters)
        {
            return Task.FromResult(GetByRawSql(query, parameters));
        }

        public virtual TEntity Insert(TEntity entity)
        {
            NullCheckThrowEntityNullException(entity);

            _dbSet.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public virtual async Task<TEntity> InsertAsync(TEntity entity)
        {
            NullCheckThrowEntityNullException(entity);

            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual TEntity Update(TEntity entity)
        {
            NullCheckThrowEntityNullException(entity);

            _dbSet.Update(entity);
            _context.SaveChanges();
            return entity;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            NullCheckThrowEntityNullException(entity);

            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual bool Delete(int id)
        {
            TEntity entityFromDb = _dbSet.Find(id);

            NullCheckThrowNotFoundException(entityFromDb);

            _dbSet.Remove(entityFromDb);
            return _context.SaveChanges() != 0;
        }

        public virtual async Task<bool> DeleteAsync(int id)
        {
            TEntity entityFromDb = await _dbSet.FindAsync(id);

            NullCheckThrowNotFoundException(entityFromDb);

            _dbSet.Remove(entityFromDb);
            return await _context.SaveChangesAsync() != 0;
        }

        private TEntity Find(int id) => _dbSet.Find(id);

        private void NullCheckThrowNotFoundException(TEntity entity)
        {
            if (entity == null)
            {
                ThrowHelper.ThrowEntityNotFoundException(nameof(TEntity));
            }
        }

        private void NullCheckThrowEntityNullException(TEntity entity)
        {
            if (entity == null)
            {
                ThrowHelper.ThrowEntityNullException(nameof(TEntity));
            }
        }
    }
}