using System;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using BlogEngine.ClientServices.Services.Abstractions;
using BlogEngine.ClientServices.Services.Implementations;
using BlogEngine.ClientServices.Account;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlogEngine.Client.Extensions
{
    public static class ClientServiceCollection
    {
        public static IServiceCollection AddHttpClientService(this IServiceCollection services)
        {
            // TODO: HttpClient BaseAdress should parsed from  ***.JSON file
            return services.AddScoped<HttpClient>(s =>
            {
                return new HttpClient { BaseAddress = new Uri("https://localhost:44328") };
            });
        }

        public static IServiceCollection AddClientAPIServices(this IServiceCollection services)
        {
            services.AddScoped<IHttpService, HttpService>();

            services.AddScoped<IBlogClient, BlogClient>();

            services.AddScoped<ICommentClient, CommentClient>();

            services.AddScoped<INotificationReceiverClient, NotificationReceiverClient>();

            services.AddScoped<ICategoryClient, CategoryClient>();

            services.AddScoped<IPagesClient, PagesClient>();

            services.AddScoped<IAccountClient, AccountClient>();

            return services;
        }

        public static IServiceCollection AddJSInteropServices(this IServiceCollection services)
        {
            services.AddScoped<IBrowserStorageService, BrowserStorageService>();

            return services;
        }

        public static IServiceCollection AddJWTAuthenticationServices(this IServiceCollection services)
        {
            services.AddScoped<JWTAuthenticationStateProvider>();

            services.AddScoped<IJWTClaimParserService, JWTClaimParserService>();

            services.AddScoped<AuthenticationStateProvider, JWTAuthenticationStateProvider>(
                provider => provider.GetRequiredService<JWTAuthenticationStateProvider>());

            services.AddScoped<ILoginService, JWTAuthenticationStateProvider>(
                provider => provider.GetRequiredService<JWTAuthenticationStateProvider>());

            services.AddOptions();
            services.AddAuthorizationCore(opt => { });

            return services;
        }
    }
}