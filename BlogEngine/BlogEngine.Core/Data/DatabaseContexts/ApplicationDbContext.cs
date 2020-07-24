using BlogEngine.Core.Data.Entities;
using BlogEngine.Core.Data.Entities.JoiningEntities;
using Microsoft.EntityFrameworkCore;

namespace BlogEngine.Core.Data.DatabaseContexts
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<BlogComment> BlogComments { get; set; }
        public DbSet<BlogGenre> BlogGenres { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlogGenre>().HasKey(b => new { b.BlogID, b.GenreID });
            modelBuilder.Entity<BlogComment>().HasKey(b => new { b.BlogID, b.CommentID });

            base.OnModelCreating(modelBuilder);
        }
    }
}