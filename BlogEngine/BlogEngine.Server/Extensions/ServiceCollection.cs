using System;
using System.IO;
using System.Reflection;
using BlogEngine.Core.Data.DatabaseContexts;
using BlogEngine.Core.Services.Abstractions;
using BlogEngine.Core.Services.Implementations;
using BlogEngine.Server.Services.Abstractions;
using BlogEngine.Server.Services.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using AutoMapper;

namespace BlogEngine.Server.Extensions
{
    public static class ServiceCollection
    {
        public static IServiceCollection AddDataAcess(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DbConnection"),
                    b => b.MigrationsAssembly("BlogEngine.Core"));
            });
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBlogRepository, BlogRepository>();

            services.AddScoped<ICategoryRepository, CategoryRepository>();

            services.AddScoped<ICommentRepository, CommentRepository>();

            return services;
        }

        public static IServiceCollection AddBLLServices(this IServiceCollection services)
        {
            services.AddScoped<IReadingTimeEstimator, ReadingTimeEstimator>();

            services.AddScoped<IBlogService, BlogService>();

            services.AddScoped<IBlogSearchService, BlogSearchService>();

            services.AddScoped<ICategoryService, CategoryService>();

            services.AddScoped<ICommentService, CommentService>();

            services.AddScoped<IPageService, PageService>();

            return services;
        }

        public static IMvcBuilder AddJson(this IServiceCollection services)
        {
            return services.AddMvc()
                .AddNewtonsoftJson(jsonOptions =>
                {
                    jsonOptions.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });
        }

        public static IServiceCollection AddMapper(this IServiceCollection services)
        {
            return services.AddAutoMapper(configuration =>
            {
                configuration.AllowNullCollections = true;
                configuration.AllowNullDestinationValues = true;
            }, typeof(Startup));
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            return services.AddSwaggerGen(config =>
            {
                var openApiContact = new OpenApiContact()
                {
                    Name = "Beso Genebashvili",
                    Email = "genebashvili99@gmail.com",
                    Url = new Uri("https://github.com/BesoGenebashvili")
                };

                var openApiInfo = new OpenApiInfo()
                {
                    Version = "v1",
                    Title = "BlogEngine.API",
                    Description = "This is a Web API for BlogEngine project",
                    Contact = openApiContact
                };

                config.SwaggerDoc("v1", openApiInfo);

                string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                config.IncludeXmlComments(xmlPath);
            });
        }
    }
}