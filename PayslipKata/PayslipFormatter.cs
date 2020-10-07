using System;

namespace PayslipKata
{
    public static class PayslipFormatter
    {
        public static string Format(Payslip payslip)
        {
            var fullName = $"{payslip.Employee.FirstName} {payslip.Employee.Surname}";
            var payPeriod = $"{payslip.PayPeriod.StartDate:dd MMMM yyyy} – {payslip.PayPeriod.EndDate:dd MMMM yyyy}";
            var grossIncome = payslip.GrossIncome.ToString("0");
            var incomeTax = payslip.IncomeTax.ToString("0");
            var netIncome = payslip.NetIncome.ToString("0");
            var super = payslip.Super.ToString("0");

            return $"Name: {fullName}\n" +
                   $"Pay Period: {payPeriod}\n" +
                   $"Gross Income: {grossIncome}\n" +
                   $"Income Tax: {incomeTax}\n" +
                   $"Net Income: {netIncome}\n" +
                   $"Super: {super}";
        }
    }
}