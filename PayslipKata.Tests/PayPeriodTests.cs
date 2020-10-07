using System;
using Xunit;

namespace PayslipKata.Tests
{
    public class PayPeriodTests
    {
        [Fact]
        public void ThrowsExceptionIfStartDateIsLaterThanEndDate()
        {
            var startDate = new DateTime(2020,3,31);
            var endDate = new DateTime(2020,3,1);

            const string expectedMessage = "Start date cannot be later than end date!";
            var actualException = Assert.Throws<ArgumentException>(() => new PayPeriod(startDate, endDate));
            
            Assert.Equal(expectedMessage, actualException.Message);
        }
    }
}