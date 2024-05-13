using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace sequor_be.Models
{
    public class Production
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(50)]
        public string Order { get; set; }

        public DateTime Date { get; set; }

        public decimal Quantity { get; set; }

        [MaxLength(50)]
        public string MaterialCode { get; set; }

        public decimal CycleTime { get; set; }

        [NotMapped]
        public Order OrderObj { get; set; }
    }
}
