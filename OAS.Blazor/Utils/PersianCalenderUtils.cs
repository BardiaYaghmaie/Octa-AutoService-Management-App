using System.Globalization;

namespace OAS.Blazor.Utils
{
    public static class PersianFormatting
    {
        public static string AppendRialIran(this string price)
        {
            return price + " ریال";
        }
        public static string Fa2En(this string str)
        {
            return str.Replace("۰", "0")
                      .Replace("۱", "1")
                      .Replace("۲", "2")
                      .Replace("۳", "3")
                      .Replace("۴", "4")
                      .Replace("۵", "5")
                      .Replace("۶", "6")
                      .Replace("۷", "7")
                      .Replace("۸", "8")
                      .Replace("۹", "9");
        }
        public static string En2Fa(this string str)
        {
            return str.Replace("0","۰")
                      .Replace("1","۱")
                      .Replace("2","۲")
                      .Replace("3","۳")
                      .Replace("4","۴")
                      .Replace("5","۵")
                      .Replace("6","۶")
                      .Replace("7","۷")
                      .Replace("8","۸")
                      .Replace("9", "۹");
        }
    }
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
