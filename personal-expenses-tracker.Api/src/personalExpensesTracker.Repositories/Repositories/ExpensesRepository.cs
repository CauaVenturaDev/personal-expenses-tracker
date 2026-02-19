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

    public async Task<Expense> AddExpenseAsync(Expense expense)
    {
        await _context.Expenses.Add(expense);
        await _context.SaveChangesAsync();
        return expense;
    }

    public async Task DeleteExpenseAsync(int id)
    {
        var expense = await _context.Expenses.FindAsync(id);

        if (expense == null)
            throw new KeyNotFoundException("Expense not found.");

        _context.Expenses.Remove(expense);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Expense>> GetAllExpensesAsync()
    {
        return await _context.Expenses
            .ToListAsync();
    }

    public async Task<Expense?> GetExpenseByIdAsync(int id)
    {
        var expense = await _context.Expenses.FindAsync(id);

        if (expense == null)
            throw new KeyNotFoundException("Expense not found.");

        return expense;
    }

    public async Task UpdateExpenseAsync(Expense expense)
    {
        var existingExpense = await _context.Expenses.FindAsync(expense.Id);

        if (existingExpense == null)
        {
            throw new KeyNotFoundException("Expense not found.");
        }
            existingExpense.Description = expense.Description;
            existingExpense.Amount = expense.Amount;
            existingExpense.Category = expense.Category;
            existingExpense.Date = expense.Date;

        await _context.SaveChangesAsync();

    }
}
