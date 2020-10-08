using System.Linq;
using BlogEngine.Shared.DTOs;
using System.Collections.Generic;
using BlogEngine.Core.Helpers;

namespace BlogEngine.Server.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> Paginate<T>(this IEnumerable<T> enumerable, PaginationDTO paginationDTO)
        {
            if (enumerable == null)
            {
                Throw.ArgumentNullException(nameof(enumerable));
            }

            if (paginationDTO == null)
            {
                Throw.ArgumentNullException(nameof(paginationDTO));
            }

            return enumerable
                .Skip((paginationDTO.Page - 1) * paginationDTO.RecordsPerPage)
                .Take(paginationDTO.RecordsPerPage);
        }
    }
}