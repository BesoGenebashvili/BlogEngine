using BlogEngine.Core.Data.DatabaseContexts;
using BlogEngine.Core.Services.Abstractions;
using BlogEngine.Core.Services.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlogEngine.Core
{
    public static class ServiceCollection
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDataAcess(configuration);

            services.AddRepositories();

            return services;
        }

        public static void AddDataAcess(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DbConnection"),
                    b => b.MigrationsAssembly("BlogEngine.Core"));
            });
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBlogRepository, BlogRepository>();

            services.AddScoped<ICategoryRepository, CategoryRepository>();

            services.AddScoped<ICommentRepository, CommentRepository>();

            services.AddScoped<INotificationReceiverRepository, NotificationReceiverRepository>();

            services.AddScoped<IBlogRatingRepository, BlogRatingRepository>();
        }
    }
}