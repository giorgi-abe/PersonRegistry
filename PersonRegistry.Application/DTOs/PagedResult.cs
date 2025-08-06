using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonRegistry.Application.DTOs
{
    public class PagedResult<T>
    {
        public IReadOnlyList<T> Items { get; }
        public int TotalCount { get; }
        public int Page { get; }
        public int PageSize { get; }

        public PagedResult(IEnumerable<T> items, int totalCount, int page, int pageSize)
        {
            Items = items.ToList();
            TotalCount = totalCount;
            Page = page;
            PageSize = pageSize;
        }
    }
}
