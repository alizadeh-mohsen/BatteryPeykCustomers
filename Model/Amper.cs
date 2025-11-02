using System.ComponentModel.DataAnnotations;

namespace BatteryPeykCustomers.Model
{
    public class Amper
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "این فیلد اجباری است")]
        public string Title { get; set; }

        [Required(ErrorMessage = "این فیلد اجباری است")]
        public int Amperage { get; set; }
    }
}
