using System;

namespace PayslipKata
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the payslip generator!");
            Console.WriteLine();
            Console.Write("Please input your name: ");
            var firstName = Console.ReadLine();
            Console.Write("Please input your surname: ");
            var surname = Console.ReadLine();
            Console.Write("Please input your annual salary: ");
            //var annualSalary = GetAnnualSalary();

        }

        private static int GetAnnualSalary()
        {
            return 0;
        }
    }
}
