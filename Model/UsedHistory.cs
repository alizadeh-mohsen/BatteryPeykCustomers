using System.ComponentModel.DataAnnotations;

namespace BatteryPeykCustomers.Model
{
    public class UsedHistory
    {
        [Key]
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int Amper { get; set; }
        public string Brand { get; set; }
        public DateTime Date { get; set; }
    }
}
