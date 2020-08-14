using System;
using BlogEngine.Core.Helpers;
using BlogEngine.Core.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BlogEngine.Core.Data.Entities;

namespace BlogEngine.Core.Services.Implementations
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
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
            return await FindAsync(id);
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
            IQueryable<TEntity> query = _dbSet.AsQueryable();

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
            SaveChanges();

            return entity;
        }

        public virtual async Task<TEntity> InsertAsync(TEntity entity)
        {
            NullCheckThrowEntityNullException(entity);

            await _dbSet.AddAsync(entity);
            await SaveChangesAsync();

            return entity;
        }

        public virtual TEntity Update(TEntity entity)
        {
            NullCheckThrowEntityNullException(entity);

            _dbSet.Update(entity);
            SaveChanges();

            return entity;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            NullCheckThrowEntityNullException(entity);

            _dbSet.Update(entity);
            await SaveChangesAsync();

            return entity;
        }

        public virtual bool Delete(int id)
        {
            TEntity entityFromDb = Find(id);

            NullCheckThrowNotFoundException(entityFromDb);

            _dbSet.Remove(entityFromDb);
            return SaveChanges();
        }

        public virtual async Task<bool> DeleteAsync(int id)
        {
            TEntity entityFromDb = await FindAsync(id);

            NullCheckThrowNotFoundException(entityFromDb);

            _dbSet.Remove(entityFromDb);
            return await SaveChangesAsync();
        }

        protected TEntity Find(int id) => _dbSet.Find(id);

        protected async Task<TEntity> FindAsync(int id) => await _dbSet.FindAsync(id);

        protected void NullCheckThrowNotFoundException(TEntity entity)
        {
            if (entity == null)
            {
                ThrowHelper.ThrowEntityNotFoundException(typeof(TEntity).Name);
            }
        }

        protected void NullCheckThrowEntityNullException(TEntity entity)
        {
            if (entity == null)
            {
                ThrowHelper.ThrowEntityNullException(typeof(TEntity).Name);
            }
        }

        /*
        protected void NullCheckThrowArgumentNullException(TEntity entity)
        {
            if (entity == null)
            {
                ThrowHelper.ThrowArgumentNullException(typeof(TEntity).Name);
            }
        }
        */

        protected async Task<bool> SaveChangesAsync()
        {
            try
            {
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected bool SaveChanges()
        {
            try
            {
                return _context.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}