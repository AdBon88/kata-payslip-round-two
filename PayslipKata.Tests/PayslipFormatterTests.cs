using System;
using Xunit;

namespace PayslipKata.Tests
{
    public class PayslipFormatterTests
    {

        
        [Fact]
        public void FormatsPayslipCorrectly()
        {
            var employee = new Employee("John", "Doe", 60050, 9);
            var dateRange = new PayPeriod(new DateTime(2020, 3, 1), new DateTime(2020, 3, 31));
            var payslip = new Payslip(employee, dateRange, 5004.16667m, 921.93750m);

            const string expected = "Name: John Doe\n" +
                                    "Pay Period: 01 March 2020 – 31 March 2020\n" +
                                    "Gross Income: 5004\n" +
                                    "Income Tax: 922\n" +
                                    "Net Income: 4082\n" +
                                    "Super: 450";

            var actual = PayslipFormatter.Format(payslip);
            Assert.Equal(expected, actual);
        }
    
    }
}