using personalExpensesTracker.Application.DTOs.ExpenseDTOs.Request;
using personalExpensesTracker.Application.DTOs.IncomeDTOs.Requests;
using personalExpensesTracker.Application.Interfaces;
using personalExpensesTracker.Domain.Models;
using personalExpensesTracker.Infrastructure.Interfaces;

namespace personalExpensesTracker.Application.Services;

public class IncomeServices(IIncomeRepository incomeRepository) : IIncomeServices
{
    private readonly IIncomeRepository _incomeRepository = incomeRepository;

    public async Task<Income> AddAsync(Income income)
    {
        if (income.Amount == 0)
        {
            throw new Exception("Amount must be greater than zero.");
        }
        return await _incomeRepository.AddIncomeAsync(income);
    }
    public async Task<List<Income>> GetAllIncomesAsync()
    {
        return await _incomeRepository.GetAllIncomes();
    }

    public async Task<List<Income>> GetByMonthAsync(int month, int year)
    {
        return await _incomeRepository.GetIncomesByMonthAsync (month, year);
    }

    public async Task<List<CategorySumaryIncomeDto>> GetTotalByCategoryAsync(int month, int year)
    {
        var incomes = await _incomeRepository.GetIncomesByMonthAsync(month, year);

        if (!incomes.Any())
        {
            return new List<CategorySumaryIncomeDto>();
        }
        var totalMonthlyIncome = incomes.Sum(i => i.Amount);

        var results = incomes
            .GroupBy(i => i.Category)
            .Select(g => new CategorySumaryIncomeDto
            {
                Category = g.Key,
                Amount = g.Sum(i => i.Amount),
                Percentage = totalMonthlyIncome > 0 ? (g.Sum(i => i.Amount) / totalMonthlyIncome) * 100 : 0
            })
            .ToList();
        return results;
    }

    public async Task<decimal> GetTotalByMonthAsync(int month, int year)
    {
        return await _incomeRepository.GetTotalByMonthAsync(month, year);
    }

    public async Task<Income> UpdateAsync(int id, IncomeCreateDTO incomeCreateDTO)
    {
        var existingIncome = await _incomeRepository.GetIncomeByIdAsync(id);

        if (existingIncome == null)
        {
            throw new KeyNotFoundException($"Income with id {id} not found.");
        }
        existingIncome.Description = incomeCreateDTO.Description;
        existingIncome.Amount = incomeCreateDTO.Amount;
        existingIncome.Category = incomeCreateDTO.Category;
        existingIncome.Date = incomeCreateDTO.Date;

        await _incomeRepository.UpdateIncomeAsync(existingIncome);
        return existingIncome;
    }
    public async Task DeleteAsync(int id)
    {
        var existingIncome = await _incomeRepository.GetIncomeByIdAsync(id);
        if (existingIncome is null)
        {
            throw new KeyNotFoundException($"Income with id {id} not found.");
        }
        await _incomeRepository.DeleteIncomeAsync(existingIncome);
    }
    public async Task DeleteAllAsync()
    {
        await _incomeRepository.DeleteAllIncomesAsync();

    }
}
