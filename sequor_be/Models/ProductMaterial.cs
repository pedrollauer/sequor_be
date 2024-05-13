using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace sequor_be.Models
{
    public class ProductMaterial
    {
        [Key]
        [MaxLength(50)]
        public string ProductCode { get; set; }

        [MaxLength(50)]
        public string MaterialCode { get; set; }

        public Material Material{ get; set; }

        [NotMapped]
        public Product Product { get; set; }
    }
}
