using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BatteryPeykCustomers.Model
{
    public class Battery
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "سود را وارد کنید")]
        public int Profit { get; set; }

        [Required(ErrorMessage ="این فیلد اجباری است")]
        public int Quantity { get; set; }

        public int AlertQuantity { get; set; }

        public int AmperId { get; set; }
        public int CompanyId { get; set; }

        [ForeignKey("AmperId")]
        public Amper? Amper { get; set; }

        [ForeignKey("CompanyId")]
        public Company? Company { get; set; }


    }
}
