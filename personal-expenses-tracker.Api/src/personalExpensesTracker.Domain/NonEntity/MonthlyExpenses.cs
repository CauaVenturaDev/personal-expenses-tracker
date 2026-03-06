using personalExpensesTracker.Domain.Entity.Models;

namespace personalExpensesTracker.Domain.NonEntity
{
    public class MonthlyExpenses
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public IEnumerable<Expense>? Expenses { get; set; }
    }
}