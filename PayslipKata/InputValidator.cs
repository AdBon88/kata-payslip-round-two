using System.Runtime.InteropServices;

namespace PayslipKata
{
    public static class InputValidator
    {
        public static bool TryParseAnnualSalary(string annualSalaryString, out decimal annualSalary)
        {
            var isDecimal = decimal.TryParse(annualSalaryString, out annualSalary);
            if (isDecimal && annualSalary > 0) return true;
            annualSalary = -1;
            return false;
        }
    }
}