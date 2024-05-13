using System.ComponentModel.DataAnnotations;
namespace sequor_be.Models
{
    public class Product
    {
        [Key]
        [MaxLength(50)]
        public string ProductCode { get; set; }

        [MaxLength(50)]
        public string ProductDescription { get; set; }

        [MaxLength(500)]
        public string Image { get; set; }

        public decimal CycleTime { get; set; }

        // Navigation properties
        public ICollection<ProductMaterial> ProductMaterials { get; set; }
    }
}
