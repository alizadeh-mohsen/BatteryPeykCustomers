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

        public string? Make { get; set; }

        public string? Battery { get; set; }

        public DateTime PurchaseDate { get; set; }

        public int Guaranty { get; set; }

        public int LifeExpectancy { get; set; }

        public DateTime ReplaceDate { get; set; }

        public string? Comments { get; set; }
        public int Sms { get; set; }

        //public int? VehicleId { get; set; }
        //public int? CompanyId { get; set; }
        //public int? AmperId { get; set; }

        //[ForeignKey("VehicleId")]
        //public Vehicle? Vehicle { get; set; }

        //[ForeignKey("CompanyId")]
        //public Company? Company { get; set; }

        //[ForeignKey("AmperId")]
        //public Amper? Amper { get; set; }



    }
}
