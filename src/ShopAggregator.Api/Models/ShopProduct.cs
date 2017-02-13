using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopAggregator.Api.Models
{
    [Table("shop_product")]
    public class ShopProduct
    {
        public ShopProduct()
        {
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("shop_id")]
        public int ShopId { get; set; }

        [Column("product_id")]
        public int ProductId { get; set; }
    }
}