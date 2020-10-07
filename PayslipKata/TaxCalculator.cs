using System.Collections.Generic;
using System.Linq;

namespace PayslipKata
{
    public class TaxCalculator
    {
        public List<TaxBracket> AnnualTaxTable { get; }

        public TaxCalculator(List<TaxBracket> annualTaxTable)
        {
            AnnualTaxTable = annualTaxTable;
            //TODO consider throwing exception for invalid table
        }

        public decimal CalculateAnnual(decimal annualSalary)
        {
            var applicableTaxBracket = AnnualTaxTable.First(taxBracket => annualSalary >= taxBracket.LowerBound
                                                                          && annualSalary <= taxBracket.UpperBound);
            return applicableTaxBracket.BaseAmount + (annualSalary - applicableTaxBracket.SalaryReductionAmount) * applicableTaxBracket.TaxRate;
        }
    }
}