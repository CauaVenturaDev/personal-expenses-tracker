using personalExpensesTracker.Domain.Models;

namespace personalExpensesTracker.Infrastructure.Interfaces;

public interface IExpensesRepository
{
    Task<Expense> AddExpenseAsync(Expense expense);
    Task<Expense?> GetExpenseByIdAsync(int id);
    Task<decimal> GetTotalByMonthAsync(int month, int year);
    Task<List<Expense>> GetExpensesByMonthAsync(int month, int year);
    Task UpdateExpenseAsync(Expense expense);
    Task DeleteExpenseAsync(Expense expense);
}
