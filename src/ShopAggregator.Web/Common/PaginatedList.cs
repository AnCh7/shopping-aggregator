using System.Collections.Generic;

namespace ShopAggregator.Web.Common
{
    public class PaginatedList<T> : List<T>
    {
        private readonly int _totalPages;

        public PaginatedList(IEnumerable<T> items, int pageIndex, int totalPages)
        {
            PageIndex = pageIndex;
            _totalPages = totalPages;
            this.AddRange(items);
        }

        public int PageIndex { get; private set; }
        public bool HasPreviousPage => (PageIndex > 1);
        public bool HasNextPage => (PageIndex < _totalPages);
    }
}