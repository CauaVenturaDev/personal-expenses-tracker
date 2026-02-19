using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update;
using personalExpensesTracker.Domain.Models;
using personalExpensesTracker.Infrastructure;
using personalExpensesTracker.Infrastructure.Interfaces;

namespace personalExpensesTracker.Infrastructure.Repositories;

public class ExpensesRepository(PersonalExpensesTrackerContext context) : IExpensesRepository
{
    private readonly PersonalExpensesTrackerContext _context = context;

    public async Task<Expense> AddExpenseAsync(Expense expense)
    {
        await _context.AddAsync(expense);
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

    public async Task<Expense> GetExpenseByIdAsync(int id)
    {
        var expense = await _context.Expenses.FindAsync(id);

        if (expense == null)
            throw new KeyNotFoundException("Expense not found.");

        return expense;
    }

    public Task<List<Expense>> GetExpensesByCategoryAndMonthAsync(string category, int month)
    {
        throw new NotImplementedException();
    }

    public Task<List<Expense>> GetExpensesByCategoryAsync(string category)
    {
        throw new NotImplementedException();
    }

    public Task<List<Expense>> GetExpensesByMonthAsync(int month, int year)
    {
        throw new NotImplementedException();
    }

    public Task<List<Expense>> GetTotalByCategoryAsync(int month, int year)
    {
        throw new NotImplementedException();
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
