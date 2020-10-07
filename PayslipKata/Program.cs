using System;
using System.Collections.Generic;

namespace PayslipKata
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the payslip generator!");
            Console.WriteLine();
            var taxCalculator = SetupTaxCalculator();
            var monthlyPayslipGenerator = new MonthlyPayslipGenerator(taxCalculator);
            var employee = GetEmployeeDetails();
            var dateRange = GetDateRange();
            var payslip = monthlyPayslipGenerator.Generate(employee, dateRange);
            Console.WriteLine();
            Console.WriteLine("Your payslip has been generated: ");
            Console.WriteLine();
            Console.WriteLine(PayslipFormatter.Format(payslip));
            Console.WriteLine();
            Console.WriteLine("Thank you for using MYOB!");
        }

        private static Employee GetEmployeeDetails()
        {
            Console.Write("Please input your name: ");
            var firstName = Console.ReadLine();
            Console.Write("Please input your surname: ");
            var surname = Console.ReadLine();
            Console.Write("Please enter your annual salary: ");
            var annualSalary = GetAnnualSalary();
            Console.Write("Please enter your super rate: ");
            var superRate = GetSuperRate();
            
            return new Employee(firstName, surname, annualSalary, superRate);
        }
        private static decimal GetAnnualSalary()
        {
            var input = Console.ReadLine();
            decimal annualSalary;
            while (!InputValidator.TryParseAnnualSalary(input, out annualSalary))
            {
                Console.Write("Annual salary must be a number greater than zero!: ");
                input = Console.ReadLine();
            }
            return annualSalary;
        }
        
        private static decimal GetSuperRate()
        {
            var input = Console.ReadLine();
            decimal superRate;
            while (!InputValidator.TryParseSuperRate(input, out superRate))
            {
                Console.Write("Super rate must be a number greater than or equal to zero!: ");
                input = Console.ReadLine();
            }
            return superRate;
        }

        private static PayPeriod GetDateRange()
        {
            bool isValidDateRange;
            DateTime startDate;
            DateTime endDate;
            do
            {
                Console.Write("Please enter your payment start date: ");
                startDate = GetDate();

                Console.Write("Please enter your payment end date: ");
                endDate = GetDate();
                
                isValidDateRange = InputValidator.ValidateDateRange(startDate, endDate);
                if (!isValidDateRange)
                {
                    Console.WriteLine("Invalid input! End date must be later than start date!");
                }
            } while (!isValidDateRange);

            return new PayPeriod(startDate, endDate);
        }

        private static DateTime GetDate()
        {
            var input = Console.ReadLine();
            DateTime date;
            while (!InputValidator.TryParseDate(input, out date))
            {
                Console.Write("Invalid date! Please enter a valid date: ");
                input = Console.ReadLine();
            }

            return date;
        }

        private static TaxCalculator SetupTaxCalculator()
        {
            var taxTable = new List<TaxBracket>
            {
                new TaxBracket(0, 18200, 0, 0),
                new TaxBracket(18201, 37000, 0, 0.19m),
                new TaxBracket(37001, 87000, 3572, 0.325m),
                new TaxBracket(87001, 180000, 19822, 0.37m),
                new TaxBracket(180001, int.MaxValue, 54232, 0.45m)
            };
            return new TaxCalculator(taxTable);
        }

    }
}
