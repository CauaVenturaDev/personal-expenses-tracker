using Microsoft.EntityFrameworkCore;
using personalExpensesTracker.Application.DTOs.ExpenseDTOs.Request;
using personalExpensesTracker.Application.Services.Interfaces;
using personalExpensesTracker.Domain.Entity.Models;
using personalExpensesTracker.Domain.NonEntity;
using personalExpensesTracker.Infrastructure.Data;

namespace personalExpensesTracker.Application.Services;

public class ExpensesServices : IExpensesServices
{
    private readonly PersonalExpensesTrackerContext _context;

    public ExpensesServices(PersonalExpensesTrackerContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(PersonalExpensesTrackerContext));
    }

    public async Task<Expense> AddAsync(Expense expense)
    {
        if (expense.Amount == 0)
        {
            throw new Exception("Amount must be greater than zero.");
        }
        await _context.Expenses.AddAsync(expense);
         _context.SaveChanges();
            return expense;
    }

    public async Task<IEnumerable<MonthlyExpenses>> GetAllDetailed()
    {
        return await _context.Expenses
            .GroupBy(e => new { e.Date.Year, e.Date.Month })
            .Select(x => new MonthlyExpenses
            {
                Year = x.Key.Year,
                Month = x.Key.Month,
                Expenses = x.ToList()
            })
            .OrderBy(x => x.Year)
            .ThenBy(x => x.Month)
            .ToListAsync();
    }

    public async Task<IEnumerable<Expense>> GetByMonth(int month, int year)
    {
        ValidateMonth(month);
        ValidateYear(year);

        return await _context.Expenses.Where(x => x.Date.Month == month && x.Date.Year == year).ToListAsync() ?? throw new Exception("Expense not founded");

        void ValidateMonth(int month)
        {
            if (month <= 0 && month > 12)
                throw new Exception("Month invalid");
        }

        void ValidateYear(int year)
        {
            if (year <= 2015)
                throw new Exception("Year invalid, need be more then 2015");
        }
    }

    public async Task<IEnumerable<CategorySumaryExpenseDto>> GetTotalByCategoryAsync(int month, int year)
    {
        return await _context.Expenses
            .Where(x => x.Date.Month == month && x.Date.Year == year)
            .GroupBy(e => e.Category)
            .Select(g => new CategorySumaryExpenseDto
            {
                Category = g.Key,
                Total = g.Sum(e => e.Amount),
                Percentage = (double)(g.Sum(e => e.Amount) / _context.Expenses.Where(x => x.Date.Month == month && x.Date.Year == year).Sum(e => e.Amount)) * 100
            }).ToListAsync();
    }

    public async Task<decimal> GetTotalByMonthAsync(int month, int year)
    {
        return await _context.Expenses
            .Where(x => x.Date.Month == month && x.Date.Year == year)
            .SumAsync(x => x.Amount);
    }


    public async Task<Expense> UpdateAsync(int id, ExpenseCreateRequest dto)
    {
        var existingExpense = await _context.Expenses.FindAsync(id);

        if (existingExpense == null)
        {
            throw new KeyNotFoundException($"Expense with id {id} not found.");
        }
        existingExpense.Description = dto.Description;
        existingExpense.Amount = dto.Amount;
        existingExpense.Category = dto.Category;
        existingExpense.Date = dto.Date;

        await _context.SaveChangesAsync();
        return existingExpense;
    }

    public async Task DeleteAsync(int id)
    { 

        var expense = await _context.Expenses.Where(x => x.Id == id).FirstOrDefaultAsync() ?? throw new Exception("Expense not founded");

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
