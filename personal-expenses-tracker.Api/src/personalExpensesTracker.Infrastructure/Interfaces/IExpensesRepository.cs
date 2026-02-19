using personalExpensesTracker.Domain.Models;

namespace personalExpensesTracker.Infrastructure.Interfaces;

public interface IExpensesRepository
{
    Task<Expense> AddExpenseAsync(Expense expense);
    Task<List<Expense>> GetAllExpensesAsync();
    Task<Expense?> GetExpenseByIdAsync(int id);
    Task<List<Expense>> GetExpensesByMonthAsync(int month, int year);
    Task<List<Expense>> GetExpensesByCategoryAsync(string category);
    Task<List<Expense>> GetExpensesByCategoryAndMonthAsync(string category, int month);
     Task<List<Expense>> GetTotalByCategoryAsync(int month, int year);
    Task UpdateExpenseAsync(Expense expense);
    Task DeleteExpenseAsync(int id);
}
