using System;

namespace PayslipKata
{
    public class MonthlyPayslipGenerator
    {
        private static TaxCalculator _taxCalculator;

        public MonthlyPayslipGenerator(TaxCalculator taxCalculator)
        {
            _taxCalculator = taxCalculator;
        }

        public Payslip Generate(Employee employee, PayPeriod payPeriod)
        {
            var grossIncome = CalculateGrossIncomeForPayPeriod(employee.AnnualSalary, payPeriod);
            var annualTax = _taxCalculator.CalculateAnnual(employee.AnnualSalary);
            var incomeTax = CalculateTaxForPayPeriod(annualTax, payPeriod);
            return new Payslip(employee, payPeriod, grossIncome, incomeTax);
        }
        
        public static decimal CalculateGrossIncomeForPayPeriod(decimal annualSalary, PayPeriod payPeriod)
        {
            var monthsInPeriod = CalculateMonthsInPeriod(payPeriod);
            return Math.Round(annualSalary * monthsInPeriod/12, 5, MidpointRounding.AwayFromZero);
        }

        public static decimal CalculateTaxForPayPeriod(decimal annualTax, PayPeriod payPeriod)
        {
            var monthsInPeriod = CalculateMonthsInPeriod(payPeriod);
            return annualTax / 12 * monthsInPeriod;
        }

        private static int CalculateMonthsInPeriod(PayPeriod payPeriod)
        {
            return (payPeriod.EndDate.Month - payPeriod.StartDate.Month + 1) + 12 * (payPeriod.EndDate.Year - payPeriod.StartDate.Year);
        }

    }
}