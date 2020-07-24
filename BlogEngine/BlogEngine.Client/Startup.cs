using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Net.Http;
using Syncfusion.Blazor;
using Syncfusion.Licensing;
using Blazor.FileReader;
using BlogEngine.ClientServices.Services.Implementations;
using BlogEngine.ClientServices.Services.Abstractions;

namespace BlogEngine.Client
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
            services.AddRazorPages();
            services.AddServerSideBlazor();

            // TODO: HttpClient BaseAdress should parsed from  ***.JSON file
            services.AddScoped<HttpClient>(s =>
            {
                return new HttpClient { BaseAddress = new Uri("https://localhost:44328") };
            });

            // syncfusion service
            services.AddSyncfusionBlazor();

            // file reader service
            services.AddFileReaderService();

            // http service for api calls
            services.AddScoped<IHttpService, HttpService>();

            // blog service for api calls
            services.AddScoped<IBlogService, BlogService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (File.Exists(Directory.GetCurrentDirectory() + "/SyncfusionLicense.txt"))
            {
                string licenseKey = File.ReadAllText(Directory.GetCurrentDirectory() + "/SyncfusionLicense.txt");
                SyncfusionLicenseProvider.RegisterLicense(licenseKey);
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}