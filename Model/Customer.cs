using System.ComponentModel.DataAnnotations;

namespace BatteryPeykCustomers.Model
{
    public class Customer
    {
        [Key]
        [MaxLength(20)]
        public string Phone { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [MaxLength(50)]
        public string Car { get; set; }

        [Required]
        [MaxLength(50)]
        public string Battery { get; set; }

        [Required]
        [Display(Name = "Purchase Date")]
        public DateTime PurchaseDate { get; set; } = DateTime.Today;

        [Required]
        [Display(Name = "Guaranty Start Date")]
        public DateTime GuarantyStartDate { get; set; } = DateTime.Today;

        [Required]
        public int LifeExpectancy { get; set; }

        public string? Comments { get; set; }
    }

}
