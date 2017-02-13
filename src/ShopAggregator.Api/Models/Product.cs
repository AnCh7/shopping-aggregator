using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopAggregator.Api.Models
{
    [Table("product")]
    public class Product
    {
        public Product()
        {
        }

        [Key]
        [Column("product_id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("description")]
        public string Description { get; set; }
    }
}