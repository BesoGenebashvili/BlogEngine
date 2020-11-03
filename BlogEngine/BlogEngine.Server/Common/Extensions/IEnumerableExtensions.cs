using System.Linq;
using BlogEngine.Shared.DTOs;
using System.Collections.Generic;
using BlogEngine.Shared.Helpers;

namespace BlogEngine.Server.Common.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> Paginate<T>(this IEnumerable<T> enumerable, PaginationDTO paginationDTO)
        {
            Preconditions.NotNull(enumerable, nameof(enumerable));
            Preconditions.NotNull(paginationDTO, nameof(paginationDTO));

            return enumerable
                .Skip((paginationDTO.Page - 1) * paginationDTO.RecordsPerPage)
                .Take(paginationDTO.RecordsPerPage);
        }
    }
}