using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace fiQuiz.Core
{
    public static class PaginatedExtensions
    {
        public static async Task<IPaginatedEnumerable<T>> ToPaginatedAsync<T>(this IQueryable<T> source, int pageIndex, int pageSize = 15)
        {
            var count = await source.CountAsync();
            var items = await source.Skip(
                    (pageIndex - 1) * pageSize)
                .Take(pageSize).ToListAsync();
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
        public static IPaginatedEnumerable<T> ToPaginated<T>(this IQueryable<T> source, int pageIndex, int pageSize = 15)
        {
            var count = source.Count();
            var items = source.Skip(
                    (pageIndex - 1) * pageSize)
                .Take(pageSize).ToList();
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }
}
