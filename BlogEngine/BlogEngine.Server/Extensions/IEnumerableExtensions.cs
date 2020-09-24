using System.Linq;
using BlogEngine.Shared.DTOs;
using System.Collections.Generic;

namespace BlogEngine.Server.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> Paginate<T>(this IEnumerable<T> enumerable, PaginationDTO paginationDTO)
        {
            return enumerable
                .Skip((paginationDTO.Page - 1) * paginationDTO.RecordsPerPage)
                .Take(paginationDTO.RecordsPerPage);
        }
    }
}