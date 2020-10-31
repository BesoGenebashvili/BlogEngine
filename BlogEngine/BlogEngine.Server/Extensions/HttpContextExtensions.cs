using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using BlogEngine.Shared.Helpers;
using BlogEngine.Core.Helpers;

namespace BlogEngine.Server.Extensions
{
    public static class HttpContextExtensions
    {
        public async static Task InsertPaginationParametersInResponseAsync<T>(
            this HttpContext httpContext, IEnumerable<T> enumerable, int recordsPerPage)
        {
            Preconditions.NotNull(httpContext, nameof(httpContext));
            Preconditions.NotNull(enumerable, nameof(enumerable));

            if (recordsPerPage <= 0)
            {
                Throw.InvalidOperationException($"'{nameof(recordsPerPage)}' must be greater than zero");
            }

            await Task.Run(() =>
            {
                ProcessInsertPaginationParametersInResponse(httpContext, enumerable, recordsPerPage);
            });
        }

        private static void ProcessInsertPaginationParametersInResponse<T>(
            HttpContext httpContext, IEnumerable<T> enumerable, int recordsPerPage)
        {
            double count = enumerable.Count();
            double totalAmountPages = Math.Ceiling(count / recordsPerPage);

            httpContext.Response.Headers.Add(Pagination.TotalAmountPagesHeaderKey, totalAmountPages.ToString());
        }
    }
}