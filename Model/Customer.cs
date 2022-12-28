using System.ComponentModel.DataAnnotations;

namespace BatteryPeykCustomers.Model
{
    public class Customer
    {
        [Key]
        public int Id{ get; set; }

        [MaxLength(20)]
        [Required]
        public string Phone { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public string? Address { get; set; }

        [MaxLength(50)]
        public string? Car { get; set; }

        [Required]
        [MaxLength(50)]
        public string Battery { get; set; }

        [Required]
        [Display(Name = "Purchase Date")]
        public DateTime PurchaseDate { get; set; } = DateTime.Today;

        [Required]
        public int Guaranty{ get; set; }

        [Required]
        [Display(Name = "Life Expectancy")]
        public int LifeExpectancy { get; set; }

        public string? Comments { get; set; }
    }

}
