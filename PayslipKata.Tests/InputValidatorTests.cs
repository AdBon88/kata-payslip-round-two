using System;
using Xunit;
using Xunit.Sdk;

namespace PayslipKata.Tests
{
    public class InputValidatorTests
    {
        [Theory]
        [InlineData("60050", true, 60050.00)]
        [InlineData("60,050", true, 60050.00)]
        [InlineData("60050.82", true, 60050.82)]
        [InlineData("0", false, -1)]
        [InlineData("-1", false, -1)]
        [InlineData("Not a number", false, -1)]
        public void SalaryMustBePositiveDecimal(string input, bool expected, decimal expectedOutput)
        {
            var actual = InputValidator.TryParseAnnualSalary(input, out var actualOutput);

            Assert.Equal(expected, actual);
            Assert.Equal(expectedOutput, actualOutput);
        }
    }
}
