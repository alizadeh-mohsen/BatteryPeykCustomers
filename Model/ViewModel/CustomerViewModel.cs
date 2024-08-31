using System.ComponentModel.DataAnnotations;

namespace BatteryPeykCustomers.Model.ViewModel
{
    public class CustomerViewModel
    {
        [MaxLength(20)]
        [Required]
        public string Phone { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public string? Address { get; set; }

        public string? Make { get; set; }

        public string? Battery { get; set; }

        public DateTime? PurchaseDate { get; set; }

        [Required]
        public int Guaranty { get; set; }

        [Required]
        public int LifeExpectancy { get; set; }

        public string? Comments { get; set; }

        public DateTime? ReplaceDate { get; set; }
        public int? VehicleId{ get; set; }
        public int? CompanyId{ get; set; }
        public int? AmperId{ get; set; }
    }
}
