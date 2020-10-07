using System;
using System.Collections.Generic;
using System.Globalization;
using Xunit;

namespace PayslipKata.Tests
{
    public class TaxCalculatorTests
    {
        private List<TaxBracket> _taxTable = SetUpTestTaxTable();
        [Theory]
        [InlineData(60050, 11063.25)]
        [InlineData(18200, 0)]
        [InlineData(18201, 0.19)]
        [InlineData(37000, 3572)]
        [InlineData(37001, 3572.325)]
        [InlineData(87000, 19822)]
        [InlineData(87001, 19822.37)]
        [InlineData(180000, 54232)]
        [InlineData(180001, 54232.45)]
        [InlineData(int.MaxValue, 966340873.15)]

        public void CalculatesCorrectTaxForGivenAnnualSalary(decimal annualSalary, decimal expected)
        {
            var taxCalculator = new TaxCalculator(_taxTable);

            var actual = taxCalculator.CalculateAnnual(annualSalary);
            Assert.Equal(expected, actual);
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