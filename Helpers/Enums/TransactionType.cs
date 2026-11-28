using System.ComponentModel.DataAnnotations;

namespace BatteryPeykCustomers.Helpers.Enums
{
    public enum TransactionType
    {

        [Display(Name = "واریز")]
        Deposit = 1,
        [Display(Name = "برداشت")]
        Withdrawal = 2
    }
}
