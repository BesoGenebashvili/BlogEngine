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
using Microsoft.AspNetCore.Mvc.Infrastructure;
using BlogEngine.Server.Attributes;
using BlogEngine.Shared.Models;
using BlogEngine.Core.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole<int>>(config =>
            {
                config.Password.RequiredLength = 3;
                config.Password.RequireDigit = false;
                config.Password.RequireLowercase = false;
                config.Password.RequireNonAlphanumeric = false;
            }).AddEntityFrameworkStores<ApplicationDbContext>()
              .AddDefaultTokenProviders();

            return services;
        }

        public static IServiceCollection AddJWTAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var JWTKey = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,

                        IssuerSigningKey = new SymmetricSecurityKey(JWTKey),
                    };
                });

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBlogRepository, BlogRepository>();

            services.AddScoped<ICategoryRepository, CategoryRepository>();

            services.AddScoped<ICommentRepository, CommentRepository>();

            services.AddScoped<INotificationReceiverRepository, NotificationReceiverRepository>();

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

            services.AddScoped<INotificationReceiverService, NotificationReceiverService>();

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

        public static IServiceCollection AddMailServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SMTPConfig>(configuration.GetSection("SMTPConfig"));

            services.AddTransient<IMailService, MailService>();

            return services;
        }

        public static IServiceCollection AddNotificationServices(this IServiceCollection services)
        {
            services.AddTransient<INotificationBodyBuilder, NotificationBodyBuilder>();

            services.AddTransient<INotificationSender, NotificationSender>();

            return services;
        }

        public static IServiceCollection AddMapper(this IServiceCollection services)
        {
            return services.AddAutoMapper(configuration =>
            {
                configuration.AllowNullCollections = true;
                configuration.AllowNullDestinationValues = true;
            }, typeof(Startup));
        }

        public static IServiceCollection AddHATEOASServices(this IServiceCollection services)
        {
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            services.AddTransient<CategoryHATEOASAttribute>();

            services.AddTransient<ILinksGenerator, LinksGenerator>();

            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            return services.AddSwaggerGen(config =>
            {
                // TODO: use config.json file for this values
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