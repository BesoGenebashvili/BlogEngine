using System;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using BlogEngine.ClientServices.Services.Abstractions;
using BlogEngine.ClientServices.Services.Implementations;

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

            return services;
        }
    }
}