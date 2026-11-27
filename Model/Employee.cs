using BatteryPeykCustomers.Helpers.Enums;
using System.ComponentModel.DataAnnotations;

namespace BatteryPeykCustomers.Model
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "این فیلد اجباری است")]
        [MaxLength(50)]
        public required string Name { get; set; }

        [Required(ErrorMessage = "این فیلد اجباری است")]
        public StaffType Stafftype { get; set; }

        [Required(ErrorMessage = "این فیلد اجباری است")]
        public int FixedSalary { get; set; }

        [Required(ErrorMessage = "این فیلد اجباری است")]
        public int FeePerdelivery { get; set; }

        [Required(ErrorMessage = "این فیلد اجباری است")]
        public int OvertimeFeePerDelivery { get; set; }


    }

}
