using personalExpensesTracker.Application.DTOs;
using personalExpensesTracker.Application.Interfaces;
using personalExpensesTracker.Domain.Models;
using personalExpensesTracker.Infrastructure.Repositories;

namespace personalExpensesTracker.Application.Services;

public class ExpensesServices(ExpensesRepository repository) : IExpensesServices
{
    private readonly ExpensesRepository _respository = repository;

    public async Task<Expense> AddAsync(Expense expense)
    {
        if (expense.Amount == 0)
        {
            throw new Exception("Amount must be greater than zero.");
        }
        return await _respository.AddExpenseAsync(expense);
    }

    public Task<List<Expense>> GetByMonthAsync(int month, int year)
    {
        return _respository.GetExpensesByMonthAsync(month, year);
    }

    public async Task<List<CategorySumaryDto>> GetTotalByCategoryAsync(int month, int year)
    {
        var expenses = await _respository.GetExpensesByMonthAsync(month, year);
        if (!expenses.Any())
                return new List<CategorySumaryDto>();

        var totalMonth = expenses.Sum(e => e.Amount);

        var result = expenses
            .GroupBy(e => e.Category)
            .Select(g => new CategorySumaryDto
            {
                Category = g.Key,
                Total = g.Sum(e => e.Amount),
                Percentage = (double)(g.Sum(e => e.Amount) / totalMonth) * 100
            }).ToList();
        return result;
    }

    public async Task<decimal> GetTotalByMonthAsync(int month, int year)
    { 
        return await _respository.GetTotalByMonthAsync(month, year);
    }

    public async Task UpdateAsync(Expense expense)
    {
        await _respository.UpdateExpenseAsync(expense);
    }

    public async Task DeleteAsync(int id)
    {
       var expense = await _respository.GetExpenseByIdAsync(id);

        if (expense == null)
        {
            throw new KeyNotFoundException($"Expense with id {id} not found.");
        }
        await _respository.DeleteExpenseAsync(expense);
    }
}