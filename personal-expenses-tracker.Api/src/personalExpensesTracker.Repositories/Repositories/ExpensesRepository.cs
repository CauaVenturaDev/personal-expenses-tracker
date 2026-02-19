using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update;
using Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi;
using personalExpensesTracker.Data;
using personalExpensesTracker.Models.Models;
using personalExpensesTracker.Repositories.Interfaces;

namespace personalExpensesTracker.Repositories.Repositories;

public class ExpensesRepository(PersonalExpensesTrackerContext context) : IExpensesRepository
{
    private readonly PersonalExpensesTrackerContext _context = context;

    public async Task AddExpenseAsync(Expense expense)
    {
        await _context.Expenses.Add(expense);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteExpenseAsync(int id)
    {
        var affected = await _context.Expenses
            .Where(e => e.Id == id)
            .ExecuteDeleteAsync();

        return affected > 0;
    }

    public async Task<List<Expense>> GetAllExpensesAsync()
    {
        return await _context.Expenses
            .ToListAsync();
    }

    public Task<Expense?> GetExpenseByIdAsync(int id)
    {
        return _context.Expenses
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public Task UpdateExpenseAsync(Expense expense)
    {
        return _context.Expenses.Where(e => e.Id == expense.Id)
            .ExecuteUpdateAsync(setters =>
                setters
                    .SetProperty(e => e.Description, expense.Description)
                    .SetProperty(e => e.Amount, expense.Amount)
                    .SetProperty(e => e.Category, expense.Category)
                    .SetProperty(e => e.Date, expense.Date));
    }
}
