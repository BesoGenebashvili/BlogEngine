using AutoMapper;
using BlogEngine.Core.Data.DatabaseContexts;
using BlogEngine.Core.Services.Abstractions;
using BlogEngine.Core.Services.Implementations;
using BlogEngine.Server.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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

            // db
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DbConnection"),
                    b => b.MigrationsAssembly("BlogEngine.Core"));
            });

            // json configuration
            services.AddMvc().AddNewtonsoftJson(jsonOptions => jsonOptions.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            // mapper
            services.AddAutoMapper(configuration=> 
            {
                configuration.AllowNullCollections = true;
                configuration.AllowNullDestinationValues = true;
            },typeof(Startup));

            // blog EF service
            services.AddScoped<IBlogRepository, BlogRepository>();

            // reading time calculator service
            services.AddScoped<IReadingTimeEstimator, ReadingTimeEstimator>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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