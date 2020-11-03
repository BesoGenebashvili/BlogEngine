using System;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using BlogEngine.ClientServices.Services.Abstractions;
using BlogEngine.ClientServices.Services.Implementations;
using BlogEngine.ClientServices.Account;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlogEngine.ClientServices
{
    public static class ServiceCollection
    {
        public static IServiceCollection AddClientServices(this IServiceCollection services)
        {
            services.AddHttpClientService();

            services.AddEncrypterServices();

            services.AddClientAPIServices();

            services.AddJSInteropServices();

            services.AddJWTAuthenticationServices();

            return services;
        }

        private static void AddHttpClientService(this IServiceCollection services)
        {
            // TODO: HttpClient BaseAdress should parsed from  ***.JSON file
            services.AddScoped<HttpClient>(s =>
            {
                return new HttpClient { BaseAddress = new Uri("https://localhost:44328") };
            });
        }

        private static void AddClientAPIServices(this IServiceCollection services)
        {
            services.AddScoped<IHttpService, HttpService>();

            services.AddScoped<IBlogClient, BlogClient>();

            services.AddScoped<ICommentClient, CommentClient>();

            services.AddScoped<INotificationReceiverClient, NotificationReceiverClient>();

            services.AddScoped<ICategoryClient, CategoryClient>();

            services.AddScoped<IPageClient, PageClient>();

            services.AddScoped<IAccountClient, AccountClient>();

            services.AddScoped<IBlogRatingClient, BlogRatingClient>();
        }

        private static void AddJSInteropServices(this IServiceCollection services)
        {
            services.AddScoped<IBrowserStorageService, BrowserStorageService>();
        }

        private static void AddJWTAuthenticationServices(this IServiceCollection services)
        {
            services.AddScoped<JWTAuthenticationStateProvider>();

            services.AddScoped<IJWTClaimParserService, JWTClaimParserService>();

            services.AddScoped<AuthenticationStateProvider, JWTAuthenticationStateProvider>(
                provider => provider.GetRequiredService<JWTAuthenticationStateProvider>());

            services.AddScoped<ILoginService, JWTAuthenticationStateProvider>(
                provider => provider.GetRequiredService<JWTAuthenticationStateProvider>());

            services.AddOptions();
            services.AddAuthorizationCore(opt => { });
        }

        private static void AddEncrypterServices(this IServiceCollection services)
        {
            services.AddTransient<IEncrypter, SimpleEncrypter>();

            services.AddTransient<IAsyncEncrypter, SimpleEncrypter>();
        }
    }
}