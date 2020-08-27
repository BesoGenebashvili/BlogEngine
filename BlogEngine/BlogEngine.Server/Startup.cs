using System;
using System.IO;
using System.Reflection;
using AutoMapper;
using BlogEngine.Core.Data.DatabaseContexts;
using BlogEngine.Core.Services.Abstractions;
using BlogEngine.Core.Services.Implementations;
using BlogEngine.Server.Extensions;
using BlogEngine.Server.Services.Abstractions;
using BlogEngine.Server.Services.Implementations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace BlogEngine.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DbConnection"),
                    b => b.MigrationsAssembly("BlogEngine.Core"));
            });

            services.AddMvc().AddNewtonsoftJson(jsonOptions => jsonOptions.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            services.AddAutoMapper(configuration =>
            {
                configuration.AllowNullCollections = true;
                configuration.AllowNullDestinationValues = true;
            }, typeof(Startup));

            services.AddScoped<IBlogRepository, BlogRepository>();

            services.AddScoped<IReadingTimeEstimator, ReadingTimeEstimator>();

            services.AddScoped<IBlogService, BlogService>();

            services.AddScoped<IBlogSearchService, BlogSearchService>();

            services.AddScoped<ICategoryRepository, CategoryRepository>();

            services.AddScoped<ICategoryService, CategoryService>();

            services.AddScoped<ICommentRepository, CommentRepository>();

            services.AddScoped<ICommentService, CommentService>();

            services.AddScoped<IPageService, PageService>();

            services.AddSwaggerGen(config =>
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

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();

            app.UseSwaggerUI(config =>
            {
                config.SwaggerEndpoint("/swagger/v1/swagger.json", "BlogEngine.API");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // custom exception handler
            app.ConfigureExceptionMiddleware();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}