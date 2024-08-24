using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BatteryPeykCustomers.Model
{
    public class Battery
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public int Profit { get; set; }
        
        [Required]
        public int Quantity { get; set; }
        
        public int AlertQuantity { get; set; }
        
        public byte AmperId { get; set; }
        public int CompanyId { get; set; }

        [ForeignKey("AmperId")]
        public Amper? Amper { get; set; }

        [ForeignKey("CompanyId")]
        public Company? Company { get; set; }


    }
}
