using Microsoft.EntityFrameworkCore;
using personalExpensesTracker.Application.DTOs.ExpenseDTOs.Request;
using personalExpensesTracker.Application.DTOs.IncomeDTOs.Requests;
using personalExpensesTracker.Application.Interfaces;
using personalExpensesTracker.Domain.Models;
using personalExpensesTracker.Infrastructure.Interfaces;

namespace personalExpensesTracker.Application.Services;

public class ExpensesServices(IRepository<Expense> repository) : IServices<Expense, CategorySumaryExpenseDto, ExpenseCreateDTO, MonthlyExpensesDto>
{
    private readonly IRepository<Expense> _repository = repository;


    public async Task<Expense> AddAsync(Expense expense)
    {
        if (expense.Amount == 0)
        {
            throw new Exception("Amount must be greater than zero.");
        }
        return await _repository.AddAsync(expense);
    }

    public async Task<List<MonthlyExpensesDto>> GetAllDetailedAsync()
    {
        var allExpenses = await _repository.GetAllAsync();

        var grouped = allExpenses
            .GroupBy(e => new { e.Date.Year, e.Date.Month })
            .Select(g => new MonthlyExpensesDto
            {
                Year = g.Key.Year,
                Month = g.Key.Month,
                Expenses = g.ToList()
            })
            .OrderBy(x => x.Year)
            .ThenBy(x => x.Month)
            .ToList();

        return grouped;
    }

    public Task<List<Expense>> GetByMonthAsync(int month, int year)
    {
        return _repository.GetByMonthAsync(month, year);
    }

    public async Task<List<CategorySumaryExpenseDto>> GetTotalByCategoryAsync(int month, int year)
    {
        var expenses = await _repository.GetByMonthAsync(month, year);
        if (!expenses.Any())
                return new List<CategorySumaryExpenseDto>();

        var totalMonth = expenses.Sum(e => e.Amount);

        var result = expenses
            .GroupBy(e => e.Category)
            .Select(g => new CategorySumaryExpenseDto
            {
                Category = g.Key,
                Total = g.Sum(e => e.Amount),
                Percentage = (double)(g.Sum(e => e.Amount) / totalMonth) * 100
            }).ToList();
        return result;
    }

    public async Task<decimal> GetTotalByMonthAsync(int month, int year)
    {
        return await _repository.GetTotalByMonthAsync(month, year);
    }

    public async Task<Expense> UpdateAsync(int id, ExpenseCreateDTO dto)
    {
        var existingExpense = await _repository.GetByIdAsync(id);

        if (existingExpense == null)
        {
            throw new KeyNotFoundException($"Expense with id {id} not found.");
        }
        existingExpense.Description = dto.Description;
        existingExpense.Amount = dto.Amount;
        existingExpense.Category = dto.Category;
        existingExpense.Date = dto.Date;
        
        await _repository.UpdateAsync(existingExpense);
        return existingExpense;

    }

    public async Task DeleteAsync(int id)
    {
       var expense = await _repository.GetByIdAsync(id);

        if (expense == null)
        {
            throw new KeyNotFoundException($"Expense with id {id} not found.");
        }
        await _repository.DeleteAsync(expense);
    }

    public async Task DeleteAllAsync()
    {
        await _repository.DeleteAllAsync();
    }

}