using System;
using System.Linq;
using System.Threading.Tasks;
using BlogEngine.ClientServices.Common.Models;
using BlogEngine.ClientServices.Services.Abstractions;
using BlogEngine.Shared.DTOs;
using BlogEngine.Shared.Helpers;

namespace BlogEngine.ClientServices.Common.Extensions
{
    public static class IhttpServiceExtensions
    {
        public static async Task<TResponse> GetHelperAsync<TResponse>(this IHttpService httpService, string url)
        {
            var response = await httpService.Get<TResponse>(url);

            if (!response.Success) throw new ApplicationException(await response.GetBody());

            return response.Response;
        }

        public static async Task<PaginatedResponse<TResponse>> GetHelperAsync<TResponse>(
            this IHttpService httpService,
            string url,
            PaginationDTO paginationDTO)
        {
            string newUrl;

            var paginationDTOQueryString = paginationDTO.ToQueryString();

            if (url.Contains("?"))
            {
                newUrl = $"{url}&{paginationDTOQueryString}";
            }
            else
            {
                newUrl = $"{url}?{paginationDTOQueryString}";
            }

            var httpResponse = await httpService.Get<TResponse>(newUrl);

            if (!httpResponse.Success) throw new ApplicationException(await httpResponse.GetBody());

            var totalAmoutPagesString = httpResponse
                .HttpResponseMessage
                .Headers
                .GetValues(Pagination.TotalAmountPagesHeaderKey)
                .FirstOrDefault();

            int totalAmountPages = int.Parse(totalAmoutPagesString);

            return new PaginatedResponse<TResponse>()
            {
                Response = httpResponse.Response,
                TotalAmountPages = totalAmountPages
            };
        }

        public static async Task<TResponse> PostHelperAsync<TData, TResponse>(this IHttpService httpService, string url, TData data)
        {
            var response = await httpService.Post<TData, TResponse>(url, data);

            if (!response.Success) throw new ApplicationException(await response.GetBody());

            return response.Response;
        }

        public static async Task PostHelperAsync<TData>(this IHttpService httpService, string url, TData data)
        {
            var response = await httpService.Post(url, data);

            if (!response.Success) throw new ApplicationException(await response.GetBody());
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