using System;
using System.Collections.Generic;

namespace PayslipKata
{
    public readonly struct Payslip
    {
        public Employee Employee { get; }
        public PayPeriod PayPeriod { get; }
        public decimal GrossIncome { get; }
        public decimal IncomeTax { get; }
        public decimal NetIncome { get; }
        public decimal Super { get; }

        public Payslip(Employee employee, PayPeriod payPeriod, decimal grossIncome, decimal incomeTax)
        {
            Employee = employee;
            PayPeriod = payPeriod;
            GrossIncome = grossIncome;
            IncomeTax = incomeTax;
            NetIncome = GrossIncome - IncomeTax;
            Super = GrossIncome * (employee.SuperRate/100);
        }
    }
}