using Microsoft.EntityFrameworkCore;
using personalExpensesTracker.Domain.Models;
using personalExpensesTracker.Infrastructure.Interfaces;

namespace personalExpensesTracker.Infrastructure.Repositories;

public class IncomeRepository(PersonalExpensesTrackerContext context) : IIncomeRepository
{
    private readonly PersonalExpensesTrackerContext _context = context;

    public async Task<Income> AddIncomeAsync(Income income)
    {
        await _context.Incomes.AddAsync(income);
        await _context.SaveChangesAsync();
        return income;
    }

    public async Task<List<Income>> GetAllIncomes()
    {
        return await _context.Incomes.ToListAsync();
    }

    public async Task<Income?> GetIncomeByIdAsync(int id)
    {
       return await _context.Incomes.FindAsync(id);
    }

    public async Task<List<Income>> GetIncomesByMonthAsync(int month, int year)
    {
        return await _context.Incomes
            .Where(i => i.Date.Month == month && i.Date.Year == year)
            .ToListAsync();
    }

    public async Task<decimal> GetTotalByMonthAsync(int month, int year)
    {
             return await _context.Incomes
            .Where(i => i.Date.Month == month && i.Date.Year == year)
            .SumAsync(i => i.Amount);
    }

    public async Task UpdateIncomeAsync(Income income)
    {
        var existingIncome = await _context.Incomes.FindAsync(income.Id);

        if (existingIncome is null)
        {
            throw new InvalidOperationException($"Income with ID {income.Id} not found.");
        }
        existingIncome.Description = income.Description;
        existingIncome.Amount = income.Amount;
        existingIncome.Category = income.Category;
        existingIncome.Date = income.Date;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteIncomeAsync(Income income)
    { 
        _context.Incomes.Remove(income);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Income>> DeleteAllIncomesAsync()
    {
        var allIncomes = await _context.Incomes.ToListAsync();
        _context.Incomes.RemoveRange(allIncomes);
        await _context.SaveChangesAsync();
        return allIncomes;
    }
}
