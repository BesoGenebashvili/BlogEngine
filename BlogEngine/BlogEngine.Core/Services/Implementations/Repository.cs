using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using BlogEngine.Core.Services.Abstractions;
using BlogEngine.Shared.Helpers;
using BlogEngine.Core.Common.Exceptions;
using BlogEngine.Core.Data.Entities.Common;

namespace BlogEngine.Core.Services.Implementations
{
    public class Repository<TEntity> : IRepository<TEntity>, IAsyncRepository<TEntity> where TEntity : BaseEntity
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

        public virtual IEnumerable<TEntity> GetByRawSql(string query, params object[] parameters)
        {
            Preconditions.NotNullOrWhiteSpace(query, nameof(query));

            return _dbSet.FromSqlRaw(query, parameters);
        }

        public virtual Task<IEnumerable<TEntity>> GetByRawSqlAsync(string query, params object[] parameters)
        {
            return Task.FromResult(GetByRawSql(query, parameters));
        }

        public virtual TEntity Insert(TEntity entity)
        {
            Preconditions.NotNull(entity, typeof(TEntity).Name);

            _dbSet.Add(entity);
            SaveChanges();

            return entity;
        }

        public virtual async Task<TEntity> InsertAsync(TEntity entity)
        {
            Preconditions.NotNull(entity, typeof(TEntity).Name);

            await _dbSet.AddAsync(entity);
            await SaveChangesAsync();

            return entity;
        }

        public virtual TEntity Update(TEntity entity)
        {
            Preconditions.NotNull(entity, typeof(TEntity).Name);

            _dbSet.Update(entity);
            SaveChanges();

            return entity;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            Preconditions.NotNull(entity, typeof(TEntity).Name);

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
            if (entity is null)
            {
                throw new EntityNotFoundException(typeof(TEntity).Name);
            }
        }

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