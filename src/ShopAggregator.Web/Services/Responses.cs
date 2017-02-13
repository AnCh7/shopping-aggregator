using System.Collections.Generic;

namespace ShopAggregator.Web.Services
{
    public class PaginationResponse<T>
    {
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public List<T> Results { get; set; }
    }
}