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
        [Obsolete]
        public string? Make { get; set; }

        [Obsolete]
        [MaxLength(50)]
        public string Battery { get; set; }

        [Required]
        public DateTime PurchaseDate { get; set; }

        [Obsolete]
        public int Guaranty { get; set; }

        [Obsolete]
        public int LifeExpectancy { get; set; }

        public DateTime ReplaceDate { get; set; }

        public string? Comments { get; set; }
        public int Sms { get; set; }

        public int VehicleId { get; set; }
        public int CompanyId { get; set; }
        public int AmperId { get; set; }

        [ForeignKey("VehicleId")]
        public Vehicle? Vehicle { get; set; }

        [ForeignKey("CompanyId")]
        public Company? Company { get; set; }

        [ForeignKey("AmperId")]
        public Amper? Amper { get; set; }



    }
}
