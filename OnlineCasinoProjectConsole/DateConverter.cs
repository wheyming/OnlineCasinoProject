using System;
using System.Globalization;

namespace OnlineCasinoProjectConsole
{
    public static class DateConverter
    {
        public static DateTime InputDayConvert(string input3_5)
        {
            if (DateTime.TryParse(input3_5, out DateTime input3_5a))
            {
                return input3_5a;
            }
            else if (DateTime.TryParseExact(input3_5, "ddMMyyyy", CultureInfo.CurrentCulture, 0, out input3_5a))
            {
                return input3_5a;
            }
            else if (DateTime.TryParseExact(input3_5, "ddMMyy", CultureInfo.CurrentCulture, 0, out input3_5a))
            {
                return input3_5a;
            }
            else
            {
                return input3_5a;
            }
        }

        public static DateTime InputMonthConvert(string input3_6)
        {
            if (DateTime.TryParse(input3_6, out DateTime input3_6a))
            {
                return input3_6a;
            }
            else if (DateTime.TryParseExact(input3_6, "MMyyyy", CultureInfo.CurrentCulture, 0, out input3_6a))
            {
                return input3_6a;
            }
            else if (DateTime.TryParseExact(input3_6, "yyMM", CultureInfo.CurrentCulture, 0, out input3_6a))
            {
                return input3_6a;
            }
            else if (DateTime.TryParseExact(input3_6, "MMyy", CultureInfo.CurrentCulture, 0, out input3_6a))
            {
                return input3_6a;
            }
            else
            {
                return input3_6a;
            }
        }

        public static DateTime InputYearConvert(string input3_7)
        {
            if (DateTime.TryParse(input3_7, out DateTime input3_7a))
            {
                return input3_7a;
            }
            else if (DateTime.TryParseExact(input3_7, "yyyy", CultureInfo.CurrentCulture, 0, out input3_7a))
            {
                return input3_7a;
            }
            else if (DateTime.TryParseExact(input3_7, "yy", CultureInfo.CurrentCulture, 0, out input3_7a))
            {
                return input3_7a;
            }
            else
            {
                return input3_7a;
            }
        }
    }
}
