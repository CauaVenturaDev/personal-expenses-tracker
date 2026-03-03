using personalExpensesTracker.Domain.Models;
using personalExpensesTracker.Domain.NonEntity;

namespace personalExpensesTracker.Application.Services
{
    public interface IExpensesServices
    {
        Task<IEnumerable<MonthlyExpenses>> GetAllDetailed();

        Task<IEnumerable<Expense>> GetByMonth(int month, int year);
    }
}