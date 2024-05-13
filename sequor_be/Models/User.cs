using System.ComponentModel.DataAnnotations;

namespace sequor_be.Models
{
    public class User
    {
        [Key]
        [MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        public DateTime InitialDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
