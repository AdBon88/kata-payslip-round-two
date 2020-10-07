using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Security;
using System.Text;
using Xunit;

namespace PayslipKata.Tests
{
    public class PayslipGeneratorTests
    {
        [Theory] 
        [InlineData("1/3/2020", "31/3/2020", 60050, 5004.16667)]
        [InlineData("1/3/2020", "15/3/2020", 60050, 5004.16667)]
        [InlineData("1/3/2020", "31/3/2020", 80000, 6666.66667)]
        [InlineData("1/1/2020", "30/04/2020", 60050, 20016.66667)]
        [InlineData("1/1/2020", "15/04/2020", 60050, 20016.66667)]
        [InlineData("1/1/2020", "31/12/2020", 60050, 60050)]
        [InlineData("1/1/2020", "31/08/2021", 60050, 100083.33333)]
        public void CanCalculateGrossIncomeForPayPeriod_RoundsTo5DecimalPlaces(string startDateStr, string endDateStr, decimal annualSalary, decimal expected)
        {
            var startDate = DateTime.ParseExact(startDateStr, "d/M/yyyy", CultureInfo.InvariantCulture);
            var endDate = DateTime.ParseExact(endDateStr, "d/M/yyyy", CultureInfo.InvariantCulture);
            var payPeriod = new PayPeriod(startDate, endDate);

            var actual = MonthlyPayslipGenerator.CalculateGrossIncomeForPayPeriod(annualSalary, payPeriod);
            
            Assert.Equal(expected, actual);
        }
        
        [Theory] 
        [InlineData("1/3/2020", "31/3/2020", 11063.25, 921.93750)]
        [InlineData("1/3/2020", "15/3/2020", 11063.25, 921.93750)]
        [InlineData("1/3/2020", "31/3/2020", 17547, 1462.25000)]
        [InlineData("1/1/2020", "30/04/2020", 11063.25, 3687.75000)]
        [InlineData("1/1/2020", "15/04/2020", 11063.25, 3687.75000)]
        [InlineData("1/1/2020", "31/12/2020", 11063.25, 11063.25000)]
        [InlineData("1/1/2020", "31/08/2021", 11063.25, 18438.75000)]
        public void CanCalculateTaxForPayPeriod_RoundsTo5DecimalPlaces(string startDateStr, string endDateStr, decimal annualTax, decimal expected)
        {
            var startDate = DateTime.ParseExact(startDateStr, "d/M/yyyy", CultureInfo.InvariantCulture);
            var endDate = DateTime.ParseExact(endDateStr, "d/M/yyyy", CultureInfo.InvariantCulture);
            var payPeriod = new PayPeriod(startDate, endDate);
            var actual = MonthlyPayslipGenerator.CalculateTaxForPayPeriod(annualTax, payPeriod);
            
            Assert.Equal(expected, actual);
        }
        
        public static IEnumerable<object[]> TestData()
        {
            var employee = new Employee("John", "Doe", 60050, 9);
            var payPeriod = new PayPeriod(new DateTime(2020, 3, 1), new DateTime(2020,3,31) );
            var expected = new Payslip(employee, payPeriod, 5004.16667m, 0);
            yield return new object[] {employee, payPeriod, expected};
        }
        
        [Theory]
        [MemberData(nameof(TestData))]
        public void GeneratesPayslipWithCorrectData(Employee employee, PayPeriod payPeriod, Payslip expected)
        {
            var taxCalculator = new TaxCalculator(SetUpTestTaxTable());
            var monthlyPayslipGenerator = new MonthlyPayslipGenerator(taxCalculator);
            var actual = monthlyPayslipGenerator.Generate(employee, payPeriod);
            
            Assert.Equal(expected.GrossIncome, actual.GrossIncome);
        }
        
        private static List<TaxBracket> SetUpTestTaxTable()
        {
            return new List<TaxBracket>
            {
                new TaxBracket(0, 18200, 0, 0),
                new TaxBracket(18201, 37000, 0, 0.19m),
                new TaxBracket(37001, 87000, 3572, 0.325m),
                new TaxBracket(87001, 180000, 19822, 0.37m),
                new TaxBracket(180001, int.MaxValue, 54232, 0.45m)
            };
        }
    }
}


