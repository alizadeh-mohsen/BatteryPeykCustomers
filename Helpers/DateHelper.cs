using System.Globalization;

namespace BatteryPeykCustomers.Helpers
{
    public class DateHelper
    {
        public static string ToPersianDate(DateTime? date)
        {
            if (date == null)
                return string.Empty;
            DateTime dateResult;

            var culture = CultureInfo.CreateSpecificCulture("en-US");
            var styles = DateTimeStyles.None;
            if (DateTime.TryParse(date.ToString(), culture, styles, out dateResult))
            {
                PersianCalendar pc = new PersianCalendar();
                return string.Format("{0}/{1}/{2}", pc.GetYear(dateResult), pc.GetMonth(dateResult), pc.GetDayOfMonth(dateResult));
            }
            return string.Empty;
        }
        public static int CalcLife(DateTime purchaseDate)
        {
            return purchaseDate == null
                ? 0
                : ((DateTime.Today.Year - purchaseDate.Year) * 12) + DateTime.Today.Month - purchaseDate.Month;
        }
    }
}
