using System;
using BlogEngine.Core.Data.Entities;
using BlogEngine.Core.Data.Entities.JoiningEntities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace BlogEngine.Core.Data.DatabaseContexts
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BlogComment> BlogComments { get; set; }
        public DbSet<BlogCategory> BlogCategories { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlogCategory>().HasKey(b => new { b.BlogID, b.CategoryID });
            modelBuilder.Entity<BlogComment>().HasKey(b => new { b.BlogID, b.CommentID });

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            AddTimestamps();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AddTimestamps();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            AddTimestamps();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected virtual void AddTimestamps()
        {
            var now = DateTime.Now;

            var addedEntities = ChangeTracker.Entries()
                .Where(E => E.State == EntityState.Added)
                .ToList();

            addedEntities.ForEach(e =>
            {
                e.Property(BaseEntityFields.DateCreated).CurrentValue = DateTime.Now;
                e.Property(BaseEntityFields.LastUpdateDate).CurrentValue = DateTime.Now;
            });

            var editedEntities = ChangeTracker.Entries()
                .Where(E => E.State == EntityState.Modified)
                .ToList();

            editedEntities.ForEach(e =>
            {
                var originalValue = e.GetDatabaseValues().GetValue<DateTime>(BaseEntityFields.DateCreated);

                e.Property(BaseEntityFields.DateCreated).CurrentValue = originalValue;

                e.Property(BaseEntityFields.LastUpdateDate).CurrentValue = DateTime.Now;
            });
        }
    }
}