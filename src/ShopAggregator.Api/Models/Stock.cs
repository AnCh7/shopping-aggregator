using System.Collections.Generic;

namespace ShopAggregator.Api.Models
{
    public class Stock
    {
        public Stock(Shop shop, IEnumerable<Product> products)
        {
            this.Shop = shop;
            this.Products = products;
        }

        public Shop Shop { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}