namespace PayslipKata
{
    public struct TaxBracket
    {
        public int LowerBound { get; }
        public int UpperBound { get; }
        public int BaseAmount { get; }
        public decimal TaxRate { get; }
        
        public int SalaryReductionAmount { get; }
        

        public TaxBracket(int lowerBound, int upperBound, int baseTaxAmount, decimal taxRate)
        {
            LowerBound = lowerBound;
            UpperBound = upperBound;
            BaseAmount = baseTaxAmount;
            TaxRate = taxRate;
            if (TaxRate > 0)
            {
                SalaryReductionAmount = 0;
            }
            SalaryReductionAmount = LowerBound - 1;
        }
    }
}