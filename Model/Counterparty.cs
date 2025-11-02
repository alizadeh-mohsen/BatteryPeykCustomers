using System.ComponentModel.DataAnnotations;

namespace BatteryPeykCustomers.Model
{
    public class Counterparty
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="این فیلد اجباری است")] 
        public string Title { get; set; }
    }
}
