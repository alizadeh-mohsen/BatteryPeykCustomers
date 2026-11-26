using System.ComponentModel.DataAnnotations;

namespace BatteryPeykCustomers.Model
{
    public class BankAccount
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "این فیلد اجباری است")]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage = "این فیلد اجباری است")]
        [MaxLength(50)]
        public string AccountNumber { get; set; }

    }
}
