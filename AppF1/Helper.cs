using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace AppF1
{
    public static class Helper
    {
        public static int GetNumbersInString(string str)
        {
            return Int32.Parse(Regex.Match(str, @"\d+").Value);
        }

        public static DateTime ConvertToDateTime(string dateString)
        {
            return DateTime.ParseExact(dateString, "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
        }
    }
}
