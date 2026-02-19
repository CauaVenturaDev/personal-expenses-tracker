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

    public async Task DeleteAsync(int id)
    {
       var expense = await _respository.GetExpenseByIdAsync(id);

        if (expense == null)
        {
            throw new KeyNotFoundException($"Expense with id {id} not found.");
        }
        await _respository.DeleteExpenseAsync(expense);
    }

    public Task<List<Expense>> GetByMonthAsync(int month, int year)
    {
        return _respository.GetExpensesByMonthAsync(month, year);
    }

    public async Task<Dictionary<string, decimal>> GetTotalByCategoryAsync(int month, int year)
    {
        var expenses = await _respository.GetExpensesByMonthAsync(month, year);
            return expenses
            .GroupBy(e => e.Category)
            .ToDictionary(
                g => g.Key, 
                g => g.Sum(e => e.Amount)
                );
    }

    public async Task<decimal> GetTotalByMonthAsync(int month, int year)
    {
        return await _respository.GetTotalByMonthAsync(month, year);
    }

    public async Task UpdateAsync(Expense expense)
    {
        await _respository.UpdateExpenseAsync(expense);
    }
}