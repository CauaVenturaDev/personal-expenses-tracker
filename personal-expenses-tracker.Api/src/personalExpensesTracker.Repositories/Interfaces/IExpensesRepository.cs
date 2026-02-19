using personalExpensesTracker.Models.Models;

namespace personalExpensesTracker.Repositories.Interfaces;

public interface IExpensesRepository
{
    Task AddExpenseAsync(Expense expense);
    Task<List<Expense>> GetAllExpensesAsync();
    Task<Expense?> GetExpenseByIdAsync(int id);
    Task UpdateExpenseAsync(Expense expense);
    Task DeleteExpenseAsync(int id);
}
