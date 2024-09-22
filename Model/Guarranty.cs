using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BatteryPeykCustomers.Model
{
    public class Guarranty
    {
        [Key]
        public int Id { get; set; }
        
        public int AmperId { get; set; }
        public int CompanyId { get; set; }

        [ForeignKey("AmperId")]
        public Amper? Amper { get; set; }

        [ForeignKey("CompanyId")]
        public Company? Company { get; set; }

        public int Amount{ get; set; }
        
        public DateTime Date { get; set; } = DateTime.Today;



    }


}
