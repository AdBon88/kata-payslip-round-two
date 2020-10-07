using System;

namespace PayslipKata
{
    public readonly struct PayPeriod
    {
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }

        public PayPeriod(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate) throw new ArgumentException("Start date cannot be later than end date!");
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}