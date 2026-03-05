using Microsoft.EntityFrameworkCore;
using personalExpensesTracker.Application.DTOs.IncomeDTOs.Requests;
using personalExpensesTracker.Application.Services.Interfaces;
using personalExpensesTracker.Domain.Models;
using personalExpensesTracker.Domain.NonEntity;
using personalExpensesTracker.Infrastructure.Data;

namespace personalExpensesTracker.Application.Services;

public class IncomesServices(PersonalExpensesTrackerContext context) : IIncomesServices
{

    private readonly PersonalExpensesTrackerContext _context = context;

    public async Task<Income> AddAsync(
        Income income
        )
    {
        if (income is null)
        {
            throw new Exception ("Income cannot be null");
        }
        await _context.Incomes.AddAsync(income);
        await _context.SaveChangesAsync();
        return income;
    }


    public async Task<IEnumerable<MonthlyIncomes>> GetAllDetailed()
    {
       return await _context.Incomes
            .GroupBy(i => new { i.Date.Year, i.Date.Month })
            .Select(g => new MonthlyIncomes
            {
                Year = g.Key.Year,
                Month = g.Key.Month,
                Incomes = g.ToList()
            }).OrderBy(x => x.Year)
            .ThenBy(x => x.Month)
            .ToListAsync();
    }

    public async Task<IEnumerable<Income>> GetByMonth(
        int month, 
        int year
        )
    {
        ValidateMonth(month);
        ValidateYear(year);

        return await _context.Incomes.Where(
            i => i.Date.Month == month && 
            i.Date.Year == year).ToListAsync() ?? throw new Exception ("Income not founded");

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

    public async Task<IEnumerable<CategorySumaryIncomeDto>> GetTotalByCategoryAsync(
        int month, 
        int year
        )
    {
        return await _context.Incomes
            .Where(i => i.Date.Month == month && i.Date.Year == year)
            .GroupBy(i => i.Category)
            .Select(g => new CategorySumaryIncomeDto
            {
                Category = g.Key,
                Total = g.Sum(i => i.Amount),
                Percentage = (double)(g.Sum(i => i.Amount) / _context.Incomes.Where(i => i.Date.Month == month && i.Date.Year == year).Sum(i => i.Amount)) * 100
            }).ToListAsync();
    }

    public async Task<decimal> GetTotalByMonthAsync(
        int month, 
        int year
        )
    {
        return await _context.Incomes
            .Where(i => i.Date.Month == month && i.Date.Year == year)
            .SumAsync(i => i.Amount);
    }

    public async Task<Income> UpdateAsync(int id, IncomeCreateRequest dto)
    {
        var existingIncome = _context.Incomes.Find(id);
        if (existingIncome == null)
        {
            throw new Exception("Income not found");
        }
        existingIncome.Amount = dto.Amount;
        existingIncome.Category = dto.Category;
        existingIncome.Description = dto.Description;
        existingIncome.Date = dto.Date;

        await _context.SaveChangesAsync();
        return existingIncome;
    }

    public async Task DeleteAsync(int id)
    {
        var income = await _context.Incomes.Where(i => i.Id == id).FirstOrDefaultAsync() ?? throw new Exception("Income not founded");
        _context.Incomes.Remove(income);
        await _context.SaveChangesAsync();
        
    }


    public async Task<List<Income>> DeleteAllAsync()
    {
        var AllIncomes = _context.Incomes.ToList();
        _context.Incomes.RemoveRange(AllIncomes);
        await _context.SaveChangesAsync();
        return AllIncomes;
    }


}
