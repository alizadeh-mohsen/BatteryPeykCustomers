using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BatteryPeykCustomers.Model
{
    public class Car
    {
        [Key]
        public int Id { get; set; }

        public int CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public Customer? Customer { get; set; }

        [MaxLength(50)]
        public string? Make{ get; set; }

        [Required]
        [MaxLength(50)]
        public string Battery { get; set; }

        [Required]
        public DateTime PurchaseDate { get; set; }

        [Required]
        public int Guaranty { get; set; }

        [Required]
        public int LifeExpectancy { get; set; }

        public string? Comments { get; set; }
        public DateTime ReplaceDate { get; set; }
    }
}
