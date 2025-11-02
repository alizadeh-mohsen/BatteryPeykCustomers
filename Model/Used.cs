using System.ComponentModel.DataAnnotations;

namespace BatteryPeykCustomers.Model
{
    public class Used
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="این فیلد اجباری است")]
        public int Quantity { get; set; }


        [Required(ErrorMessage ="این فیلد اجباری است")]
        public int Amperage { get; set; }
    }
}
