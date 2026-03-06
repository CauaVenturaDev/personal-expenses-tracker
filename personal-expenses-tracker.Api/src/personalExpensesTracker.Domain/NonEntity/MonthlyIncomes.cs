using personalExpensesTracker.Domain.Entity.Models;

namespace personalExpensesTracker.Domain.NonEntity
{
    public class MonthlyIncomes
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public IEnumerable<Income>? Incomes { get; set; }
    }
}
