using Microsoft.EntityFrameworkCore;
using personalExpensesTracker.Domain.Models;
using personalExpensesTracker.Infrastructure.Interfaces;

namespace personalExpensesTracker.Infrastructure.Repositories;

public class ExpensesRepository(PersonalExpensesTrackerContext context) : Interfaces.IExpensesRepository
{
    private readonly PersonalExpensesTrackerContext _context = context;

    public async Task<Expense> AddExpenseAsync(Expense expense)
    {
        await _context.AddAsync(expense);
        await _context.SaveChangesAsync();
        return expense;
    }


    public async Task<List<Expense>> GetExpensesByMonthAsync(int month, int year)
    {
        return await _context.Expenses
            .Where(e => e.Date.Month == month && e.Date.Year == year)
            .ToListAsync();

    }
    public async Task<decimal> GetTotalByMonthAsync(int month, int year)
    {
        return await _context.Expenses
            .Where(e => e.Date.Month == month && e.Date.Year == year)
            .SumAsync(e => e.Amount);
    }
    public async Task<Expense> GetExpenseByIdAsync(int id)
    {
        return await _context.Expenses.FindAsync(id);
    }

    public async Task DeleteExpenseAsync(Expense expense)
    {
        _context.Expenses.Remove(expense);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateExpenseAsync(Expense expense)
    {
        var existingExpense = await _context.Expenses.FindAsync(expense.Id);
            existingExpense.Description = expense.Description;
            existingExpense.Amount = expense.Amount;
            existingExpense.Category = expense.Category;
            existingExpense.Date = expense.Date;

        await _context.SaveChangesAsync();

    }
}
