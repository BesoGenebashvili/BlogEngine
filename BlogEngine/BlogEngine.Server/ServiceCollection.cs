using System;
using System.IO;
using System.Reflection;
using BlogEngine.Core.Data.DatabaseContexts;
using BlogEngine.Core.Services.Abstractions;
using BlogEngine.Core.Services.Implementations;
using BlogEngine.Server.Services.Abstractions;
using BlogEngine.Server.Services.Implementations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using BlogEngine.Shared.Models;
using BlogEngine.Core.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BlogEngine.Server.Common.Attributes;
using BlogEngine.Core;
using BlogEngine.Server.Services.Abstractions.Identity;
using BlogEngine.Server.Services.Abstractions.Utilities;
using BlogEngine.Server.Services.Implementations.Identity;
using BlogEngine.Server.Services.Implementations.Utilities;

namespace BlogEngine.Server
{
    public static class ServiceCollection
    {
        public static IServiceCollection AddServerServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCoreServices(configuration);

            services.AddIdentity();

            services.AddUserProviders();

            services.AddJWTAuthentication(configuration);

            services.AddJWTServices();

            services.AddApplicationServices();

            services.AddJson();

            services.AddMapper();

            services.AddSwagger();

            services.AddHATEOASServices();

            services.AddMailServices(configuration);

            services.AddNotificationServices();

            services.AddPDFGenerator();

            return services;
        }

        private static void AddIdentity(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole<int>>(config =>
            {
                // Temporary simple validation
                config.Password.RequiredLength = 1;
                config.Password.RequireDigit = false;
                config.Password.RequireLowercase = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<ApplicationDbContext>()
              .AddDefaultTokenProviders();
        }

        private static void AddJWTAuthentication(this IServiceCollection services, IConfiguration configuration)
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
        }

        private static void AddJWTServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, JWTTokenService>();
        }

        private static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IReadingTimeEstimator, ReadingTimeEstimator>();

            services.AddScoped<IBlogService, BlogService>();

            services.AddScoped<IBlogSearchService, BlogSearchService>();

            services.AddScoped<ICategoryService, CategoryService>();

            services.AddScoped<ICommentService, CommentService>();

            services.AddScoped<IPageService, PageService>();

            services.AddScoped<INotificationReceiverService, NotificationReceiverService>();

            services.AddScoped<IBlogRatingService, BlogRatingService>();

            services.AddScoped<IAccountService, AccountService>();

            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddScoped<IRoleManager, RoleManager>();

            services.AddScoped<ICustomerReviewService, CustomerReviewService>();
        }

        private static void AddJson(this IServiceCollection services)
        {
            services.AddMvc()
               .AddNewtonsoftJson(jsonOptions =>
               {
                   jsonOptions.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
               });
        }

        private static void AddMailServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SMTPConfig>(configuration.GetSection("SMTPConfig"));

            services.AddTransient<IMailService, MailService>();
        }

        private static void AddNotificationServices(this IServiceCollection services)
        {
            services.AddTransient<INotificationBodyBuilder, NotificationBodyBuilder>();

            services.AddTransient<INotificationSender, NotificationSender>();
        }

        private static void AddMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(configuration =>
            {
                configuration.AllowNullCollections = true;
                configuration.AllowNullDestinationValues = true;
            }, typeof(Startup));
        }

        private static void AddHATEOASServices(this IServiceCollection services)
        {
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            services.AddTransient<CategoryHATEOASAttribute>();

            services.AddTransient<ILinksGenerator, LinksGenerator>();
        }

        private static void AddUserProviders(this IServiceCollection services)
        {
            services.AddScoped<ICurrentUserProvider, CurrentUserProvider>();
        }

        private static void AddPDFGenerator(this IServiceCollection services)
        {
            services.AddTransient<IPDFGenerator, PDFGenerator>();
        }

        private static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(config =>
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