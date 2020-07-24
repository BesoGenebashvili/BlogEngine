using System;
using System.Threading.Tasks;
using BlogEngine.ClientServices.Services.Abstractions;

namespace BlogEngine.ClientServices.Extensions
{
    public static class IhttpServiceExtensions
    {
        public static async Task<TResponse> GetHelperAsync<TResponse>(this IHttpService httpService, string url)
        {
            var response = await httpService.Get<TResponse>(url);

            if (!response.Success) throw new ApplicationException(await response.GetBody());

            return response.Response;
        }

        public static async Task<TResponse> PostHelperAsync<TData, TResponse>(this IHttpService httpService, string url, TData data)
        {
            var response = await httpService.Post<TData, TResponse>(url, data);

            if (!response.Success) throw new ApplicationException(await response.GetBody());

            return response.Response;

        }

        public static async Task<TResponse> PutHelperAsync<TData, TResponse>(this IHttpService httpService, string url, TData data)
        {
            var response = await httpService.Put<TData, TResponse>(url, data);

            if (!response.Success) throw new ApplicationException(await response.GetBody());

            return response.Response;
        }

        public static async Task<TResponse> DeleteHelperAsync<TResponse>(this IHttpService httpService, string url)
        {
            var response = await httpService.Delete<TResponse>(url);

            if (!response.Success) throw new ApplicationException(await response.GetBody());

            return response.Response;
        }
    }
}