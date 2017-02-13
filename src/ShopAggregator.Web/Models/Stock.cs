using System.Collections.Generic;

namespace ShopAggregator.Web.Models
{
    public class Stock
    {
        public Shop Shop { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}