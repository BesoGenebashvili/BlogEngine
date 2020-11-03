using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Syncfusion.Blazor;
using Syncfusion.Licensing;
using Blazor.FileReader;
using BlogEngine.ClientServices.Services.Abstractions;
using BlogEngine.ClientServices;

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

            services.AddClientServices();

            services.AddFileReaderService();

            services.AddSyncfusionBlazor();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (File.Exists(Directory.GetCurrentDirectory() + "/SyncfusionLicense.txt"))
            {
                var encriptor = app.ApplicationServices.GetRequiredService<IEncrypter>();

                string licenseKey = File.ReadAllText(Directory.GetCurrentDirectory() + "/SyncfusionLicense.txt");
                var operationResult = encriptor.Decrypt(licenseKey);

                SyncfusionLicenseProvider.RegisterLicense(operationResult.Result);
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