using Microsoft.EntityFrameworkCore;
using personalExpensesTracker.Domain.Models;
using personalExpensesTracker.Infrastructure.Data;
using personalExpensesTracker.Infrastructure.Interfaces;


namespace personalExpensesTracker.Infrastructure.Repositories;

public class ExpensesRepository(PersonalExpensesTrackerContext context) : IRepository<Expense>
{
    private readonly PersonalExpensesTrackerContext _context = context;


    public async Task<Expense> AddAsync(Expense expense)
    {
    }

    public async Task<IEnumerable<Expense>> GetAllAsync()
    {
        return await _context.Expenses.ToListAsync();
    }

    // Refectored
    /*public async Task<List<Expense>> GetByMonthAsync(int month, int year)
    {
    }
    */
    public async Task<decimal> GetTotalByMonthAsync(int month, int year)
    {
        return await _context.Expenses
            .Where(e => e.Date.Month == month && e.Date.Year == year)
            .SumAsync(e => e.Amount);
    }

    public async Task<Expense?> GetByIdAsync(int id)
    {
        return await _context.Expenses.FindAsync(id);
    }

    public async Task UpdateAsync(Expense expense)
    {
        var existingExpense = await _context.Expenses.FindAsync(expense.Id);
        if (existingExpense == null)
        {
            throw new Exception("Expense not found");
        }

        existingExpense.Description = expense.Description;
        existingExpense.Amount = expense.Amount;
        existingExpense.Category = expense.Category;
        existingExpense.Date = expense.Date;

        await _context.SaveChangesAsync();

    }

    public async Task DeleteAsync(Expense expense)
    {
        _context.Expenses.Remove(expense);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Expense>> DeleteAllAsync()
    {
        var allExpenses = await _context.Expenses.ToListAsync();
        _context.Expenses.RemoveRange(allExpenses);
        await _context.SaveChangesAsync();
        return allExpenses;
    }
}

