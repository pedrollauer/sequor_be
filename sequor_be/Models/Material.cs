using System.ComponentModel.DataAnnotations;

namespace sequor_be.Models
{
    public class Material
    {
        [Key]
        [MaxLength(50)]
        public string MaterialCode { get; set; }

        [MaxLength(500)]
        public string MaterialDescription { get; set; }
    }
}
