using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using BlogEngine.Core.Data.Entities;
using BlogEngine.Core.Services.Abstractions;
using BlogEngine.Core.Data.DatabaseContexts;
using Microsoft.EntityFrameworkCore;
using BlogEngine.Shared.Helpers;
using BlogEngine.Core.Common.Exceptions;

namespace BlogEngine.Core.Services.Implementations
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;

        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public virtual async Task<MainComment> GetMainCommentByIdAsync(int id)
        {
            return await _context.MainComments
                .Include(mc => mc.SubComments)
                .FirstOrDefaultAsync(mc => mc.ID.Equals(id));
        }

        public virtual async Task<SubComment> GetSubCommentByIdAsync(int id)
        {
            return await _context.SubComments.FirstOrDefaultAsync(sc => sc.ID.Equals(id));
        }

        public virtual Task<IEnumerable<MainComment>> GetMainCommentsByBlogIdAsync(int id)
        {
            return Task.FromResult(_context.MainComments
                .Where(mc => mc.BlogID.Equals(id))
                .Include(mc => mc.SubComments)
                .AsEnumerable());
        }

        public Task<IEnumerable<SubComment>> GetSubCommentsByBlogIdAsync(int id)
        {
            return Task.FromResult(_context.SubComments
                .Where(sc => sc.BlogID.Equals(id))
                .AsEnumerable());
        }

        public virtual async Task<MainComment> InsertMainCommentAsync(MainComment mainComment)
        {
            Preconditions.NotNull(mainComment, nameof(mainComment));

            await BlogMustExist(mainComment.BlogID);
            await ApplicationUserMustExist(mainComment.ApplicationUserID);

            await _context.MainComments.AddAsync(mainComment);
            await SaveChangesAsync();

            return mainComment;
        }

        public virtual async Task<SubComment> InsertSubCommentAsync(SubComment subComment)
        {
            Preconditions.NotNull(subComment, nameof(subComment));

            await BlogMustExist(subComment.BlogID);
            await MainCommentMustExit(subComment.MainCommentID);
            await ApplicationUserMustExist(subComment.ApplicationUserID);

            await _context.SubComments.AddAsync(subComment);
            await SaveChangesAsync();

            return subComment;
        }

        public virtual async Task<bool> DeleteMainCommentAsync(int id)
        {
            var entityFromDb = await GetMainCommentByIdAsync(id);

            if (entityFromDb is null)
            {
                throw new EntityNotFoundException(nameof(MainComment));
            }

            _context.MainComments.Remove(entityFromDb);
            return await SaveChangesAsync();
        }

        public virtual async Task<bool> DeleteSubCommentAsync(int id)
        {
            var entityFromDb = await GetSubCommentByIdAsync(id);

            if (entityFromDb is null)
            {
                throw new EntityNotFoundException(nameof(SubComment));
            }

            _context.SubComments.Remove(entityFromDb);
            return await SaveChangesAsync();
        }

        protected async Task MainCommentMustExit(int id)
        {
            var mainCommentExists = await _context.MainComments.AnyAsync(m => m.ID.Equals(id));

            if (!mainCommentExists) throw new EntityNotFoundException(nameof(MainComment));
        }

        protected async Task BlogMustExist(int id)
        {
            var blogExists = await _context.Blogs.AnyAsync(b => b.ID.Equals(id));

            if (!blogExists) throw new EntityNotFoundException(nameof(Blog));
        }

        protected async Task ApplicationUserMustExist(int id)
        {
            bool userExists = await _context.Users.AnyAsync(u => u.Id.Equals(id));

            if (!userExists) throw new EntityNotFoundException(nameof(ApplicationUser));
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
    }
}