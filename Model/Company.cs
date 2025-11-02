using System.ComponentModel.DataAnnotations;

namespace BatteryPeykCustomers.Model
{
    public class Company
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="این فیلد اجباری است")]
        public string Title { get; set; }
        
        public int? Guarranty { get; set; }
        public int? LifeTime { get; set; }

    }
}
