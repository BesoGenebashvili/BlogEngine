using System;
using BlogEngine.Core.Data.Entities;
using BlogEngine.Core.Data.Entities.JoiningEntities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace BlogEngine.Core.Data.DatabaseContexts
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BlogCategory> BlogCategories { get; set; }
        public DbSet<MainComment> MainComments { get; set; }
        public DbSet<SubComment> SubComments { get; set; }
        public DbSet<NotificationReceiver> NotificationReceivers { get; set; }
        public DbSet<BlogRating> BlogRatings { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlogCategory>()
                .HasKey(b => new { b.BlogID, b.CategoryID });

            /*
            modelBuilder.Entity<ApplicationUser>()
                .Property(u => u.Id).HasColumnName("ID");
            */

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
                .Where(e => e.State == EntityState.Added && e.Entity is BaseEntity)
                .ToList();

            addedEntities.ForEach(e =>
            {
                e.Property(BaseEntityFields.DateCreated).CurrentValue = DateTime.Now;
                e.Property(BaseEntityFields.LastUpdateDate).CurrentValue = DateTime.Now;
            });

            var editedEntities = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Modified && e.Entity is BaseEntity)
                .ToList();

            editedEntities.ForEach(e =>
            {
                var dateCreatedOriginalValue = e.GetDatabaseValues().GetValue<DateTime>(BaseEntityFields.DateCreated);

                #region Need better solution
                var createdByoriginalValue = e.GetDatabaseValues().GetValue<string>(BaseEntityFields.CreatedBy);
                e.Property(BaseEntityFields.CreatedBy).CurrentValue = createdByoriginalValue;
                #endregion

                e.Property(BaseEntityFields.DateCreated).CurrentValue = dateCreatedOriginalValue;

                e.Property(BaseEntityFields.LastUpdateDate).CurrentValue = DateTime.Now;
            });
        }
    }
}