using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BatteryPeykCustomers.Model
{
    public class Warranty
    {
        [Key]
        public int Id { get; set; }

        public int BatteryId { get; set; }

        [ForeignKey("BatteryId")]
        public Battery? Battery { get; set; }

        public long Amount { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;



    }


}
