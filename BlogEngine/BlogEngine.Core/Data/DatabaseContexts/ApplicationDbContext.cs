using System;
using BlogEngine.Core.Data.Entities;
using BlogEngine.Core.Data.Entities.JoiningEntities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using BlogEngine.Core.Services.Abstractions;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BlogEngine.Core.Data.DatabaseContexts
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        private readonly ICurrentUserProvider _currentUserProvider;

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BlogCategory> BlogCategories { get; set; }
        public DbSet<MainComment> MainComments { get; set; }
        public DbSet<SubComment> SubComments { get; set; }
        public DbSet<NotificationReceiver> NotificationReceivers { get; set; }
        public DbSet<BlogRating> BlogRatings { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
            ICurrentUserProvider currentUserProvider) : base(options)
        {
            _currentUserProvider = currentUserProvider;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlogCategory>()
                .HasKey(b => new { b.BlogID, b.CategoryID });

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            AddBaseEntityFields().GetAwaiter().GetResult();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await AddBaseEntityFields();
            return await base.SaveChangesAsync(cancellationToken);
        }

        protected async Task AddBaseEntityFields()
        {
            var addedEntities = GetAddedEntities();
            var editedEntities = GetEditedEntities();

            AddTimestamps(addedEntities, editedEntities);
            await AddIdentityFields(addedEntities, editedEntities);
        }

        private List<EntityEntry> GetEditedEntities()
        {
            return ChangeTracker.Entries()
                   .Where(e => e.State == EntityState.Modified && e.Entity is BaseEntity)
                   .ToList();
        }

        private List<EntityEntry> GetAddedEntities()
        {
            return ChangeTracker.Entries()
                  .Where(e => e.State == EntityState.Added && e.Entity is BaseEntity)
                  .ToList();
        }

        protected void AddTimestamps(List<EntityEntry> addedEntities, List<EntityEntry> editedEntities)
        {
            var now = DateTime.Now;

            addedEntities.ForEach(e =>
            {
                e.Property(BaseEntityFields.DateCreated).CurrentValue = now;
                e.Property(BaseEntityFields.LastUpdateDate).CurrentValue = now;
            });

            editedEntities.ForEach(e =>
            {
                var dateCreatedOriginalValue = e.GetDatabaseValues().GetValue<DateTime>(BaseEntityFields.DateCreated);
                e.Property(BaseEntityFields.DateCreated).CurrentValue = dateCreatedOriginalValue;

                e.Property(BaseEntityFields.LastUpdateDate).CurrentValue = now;
            });
        }

        protected async Task AddIdentityFields(List<EntityEntry> addedEntities, List<EntityEntry> editedEntities)
        {
            var user = await _currentUserProvider.GetCurrentUserAsync();
            string userName = user is null ? "Anonymous" : user.FullName;

            addedEntities.ForEach(e =>
            {
                e.Property(BaseEntityFields.CreatedBy).CurrentValue = userName;
                e.Property(BaseEntityFields.LastUpdateBy).CurrentValue = userName;
            });

            editedEntities.ForEach(e =>
            {
                var createdByoriginalValue = e.GetDatabaseValues().GetValue<string>(BaseEntityFields.CreatedBy);
                e.Property(BaseEntityFields.CreatedBy).CurrentValue = createdByoriginalValue;

                e.Property(BaseEntityFields.LastUpdateBy).CurrentValue = userName;
            });
        }
    }
}