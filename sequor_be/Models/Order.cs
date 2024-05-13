using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sequor_be.Models
{
    public class Order
    {
        [Key]
        [MaxLength(50)]
        [Column("Order")]
        public string OrderCode { get; set; }

        public decimal Quantity { get; set; }

        [MaxLength(50)]
        public string ProductCode { get; set; }

        [NotMapped]
        public Product Product { get; set; }


    }
}
