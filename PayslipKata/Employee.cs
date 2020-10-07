namespace PayslipKata
{
    public struct Employee
    {
        public string FirstName { get; } //TODO discuss if these two variables should be in a person class (Too many constructor args!)
        public string Surname { get; }
        public decimal AnnualSalary { get; }
        public decimal SuperRate { get; }

        public Employee(string firstName, string surname, decimal annualSalary, decimal superRate)
        {
            FirstName = firstName;
            Surname = surname;
            AnnualSalary = annualSalary;
            SuperRate = superRate;
        }
    }
}