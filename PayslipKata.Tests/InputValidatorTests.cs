using System;
using System.Collections.Generic;
using System.Globalization;
using Xunit;
using Xunit.Sdk;

namespace PayslipKata.Tests
{
    public class InputValidatorTests
    {
        [Theory]
        [InlineData("60050", true, 60050.00)]
        [InlineData("60,050", true, 60050.00)]
        [InlineData("60,050.82", true, 60050.82)]
        [InlineData("0", false, -1)]
        [InlineData("-1", false, -1)]
        [InlineData("Not a number", false, -1)]
        public void AnnualSalaryMustBePositiveDecimal(string input, bool expected, decimal expectedOutput)
        {
            var actual = InputValidator.TryParseAnnualSalary(input, out var actualOutput);

            Assert.Equal(expected, actual);
            Assert.Equal(expectedOutput, actualOutput);
        }
        
        [Theory]
        [InlineData("1", true, 1.00)]
        [InlineData("9.5", true, 9.50)]
        [InlineData("16.34", true, 16.34)]
        [InlineData("0", true, 0.00)]
        [InlineData("-1", false, -1)]
        [InlineData("Not a number", false, -1)]
        public void SuperRateMustBeDecimalOfZeroOrGreater(string input, bool expected, decimal expectedOutput)
        {
            var actual = InputValidator.TryParseSuperRate(input, out var actualOutput);

            Assert.Equal(expected, actual);
            Assert.Equal(expectedOutput, actualOutput);
        }
        
        public static IEnumerable<object[]> DateValidationTestData()
        {
            yield return new object[] {"1 March", true, new DateTime(2020,3,1)};
            yield return new object[] {"31 March", true, new DateTime(2020,3,31)};
            yield return new object[] {"31 March 2019", true, new DateTime(2019,3,31)}; 
            yield return new object[] {"28 Feb", true, new DateTime(2020,2,28)};
            yield return new object[] {"28 Feb 2019", true, new DateTime(2019,2,28)};
            yield return new object[] {"1/4", true, new DateTime(2020,4,1)};
            yield return new object[] {"1/04", true, new DateTime(2020,4,1)};
            yield return new object[] {"01/04", true, new DateTime(2020,4,1)};
            yield return new object[] {"1/4/2019", true, new DateTime(2019,4,1)};
            yield return new object[] {"1/04/2019", true, new DateTime(2019,4,1)};
            yield return new object[] {"01/4/2019", true, new DateTime(2019,4,1)};
            yield return new object[] {"01/04/2019", true, new DateTime(2019,4,1)};
            yield return new object[] {"2019-05-06", true, new DateTime(2019,5,6)};
            yield return new object[] {"2019-5-06", true, new DateTime(2019,5,6)};
            yield return new object[] {"2019-05-6", true, new DateTime(2019,5,6)};
            yield return new object[] {"3/28/2019", false, DateTime.MinValue};
            yield return new object[] {"???", false, DateTime.MinValue};
        }
        [Theory]
        [MemberData(nameof(DateValidationTestData))]
        public void PaySlipDatesHaveMultipleCorrectFormats(string input, bool expected, DateTime expectedOutput)
        {
            var actual = InputValidator.TryParseDate(input, out var actualOutput);

            Assert.Equal(expected, actual);
            Assert.Equal(expectedOutput, actualOutput);
        }
        
        public static IEnumerable<object[]> DateRangeValidationTestData()
        {
            var startDate = new DateTime(2020,3,1);
            var endDate = new DateTime(2020,3,31);
            yield return new object[] {startDate,  endDate, true};
            
            startDate = new DateTime(2020,3,1);
            endDate = new DateTime(2020,3,1);
            yield return new object[] {startDate,  endDate, true};
            
            startDate = new DateTime(2020,3,31);
            endDate = new DateTime(2020,3,1);
            yield return new object[] {startDate,  endDate, false};
        }
        
        [Theory]
        [MemberData(nameof(DateRangeValidationTestData))]
        public void EndDateMustEqualToOrLaterThanStartDate(DateTime startDate, DateTime endDate, bool expected)
        {
            var actual = InputValidator.ValidateDateRange(startDate, endDate);
            
            Assert.Equal(expected, actual);
        }
    }
}
