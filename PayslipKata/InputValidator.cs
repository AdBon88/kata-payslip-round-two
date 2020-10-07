using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace PayslipKata
{
    public static class InputValidator
    {
        public static bool TryParseAnnualSalary(string input, out decimal annualSalary)
        {
            var isDecimal = decimal.TryParse(input, out annualSalary);
            if (isDecimal && annualSalary > 0) return true;
            annualSalary = -1;
            return false;
        }

        public static bool TryParseSuperRate(string input, out decimal superRate)
        {
            var isDecimal = decimal.TryParse(input, out superRate);
            if (isDecimal && superRate >= 0) return true;
            superRate = -1;
            return false;
        }
        
        public static bool TryParseDate(string input, out DateTime date)
        {
            var supportedDateFormats = new[] { "d/M/yyyy", "yyyy-M-d", "d/M", "d MMMM", 
                "d MMMM yyyy", "d MMM", "d MMM yyyy" };
            return DateTime.TryParseExact(input, supportedDateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out date);
        }

        public static bool ValidateDateRange(DateTime startDate, DateTime endDate)
        {
            return startDate <= endDate;
        }
        
    }
}