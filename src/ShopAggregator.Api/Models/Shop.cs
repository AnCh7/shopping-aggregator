using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopAggregator.Api.Models
{
    [Table("shop")]
    public class Shop
    {
        public Shop()
        {
        }

        [Key]
        [Column("shop_id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("address")]
        public string Address { get; set; }

        [Column("working_hours")]
        public string WorkingHours { get; set; }
    }
}