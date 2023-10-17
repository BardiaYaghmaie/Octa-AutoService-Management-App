using System.Globalization;

namespace OAS.Blazor.Utils
{
    public  static class PersianCalenderUtils
    {
        public static string ToPersianDateString(this DateTime dateTime)
        {
            PersianCalendar pc = new();
            string answer = $"{pc.GetYear(dateTime).ToString("0000")}/{pc.GetMonth(dateTime).ToString("00")}/{pc.GetDayOfMonth(dateTime).ToString("00")}";
            return answer;
        }
    }
}
