using personalExpensesTracker.Application.DTOs.ExpenseDTOs.Request;
using personalExpensesTracker.Application.Interfaces;
using personalExpensesTracker.Domain.Models;
using personalExpensesTracker.Infrastructure.Interfaces;
using personalExpensesTracker.Infrastructure.Repositories;

namespace personalExpensesTracker.Application.Services;

public class ExpensesServices(IExpensesRepository repository) : IExpensesServices
{
    private readonly IExpensesRepository _respository = repository;

    public async Task<Expense> AddAsync(Expense expense)
    {
        if (expense.Amount == 0)
        {
            throw new Exception("Amount must be greater than zero.");
        }
        return await _respository.AddExpenseAsync(expense);
    }


    //Lista as despesas por mês e ano
    public Task<List<Expense>> GetByMonthAsync(int month, int year)
    {
        return _respository.GetExpensesByMonthAsync(month, year);
    }


    //Lista as despesas por categoria, total e porcentagem do total mensal
    public async Task<List<CategorySumaryDto>> GetTotalByCategoryAsync(int month, int year)
    {
        // chama o repositório para obter as despesas do mês e ano especificados
        var expenses = await _respository.GetExpensesByMonthAsync(month, year);
        if (!expenses.Any())
                return new List<CategorySumaryDto>();
        // calcula o total mensal para calcular a porcentagem de cada categoria
        var totalMonth = expenses.Sum(e => e.Amount);

        // agrupa as despesas por categoria, calcula o total e a porcentagem para cada categoria e retorna uma lista de CategorySumaryDto
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


    // Calcula o total gasto no mês e ano especificados
    public async Task<decimal> GetTotalByMonthAsync(int month, int year)
    { 
        return await _respository.GetTotalByMonthAsync(month, year);
    }


    // Atualiza uma despesa existente
    public async Task<Expense> UpdateAsync(int id, ExpenseCreateDTO dto)
    {
        var existingExpense = await _respository.GetExpenseByIdAsync(id);

        if (existingExpense == null)
        {
            throw new KeyNotFoundException($"Expense with id {id} not found.");
        }
        existingExpense.Description = dto.Description;
        existingExpense.Amount = dto.Amount;
        existingExpense.Category = dto.Category;
        existingExpense.Date = dto.Date;
        
        await _respository.UpdateExpenseAsync(existingExpense);
        return existingExpense;

    }


    // Exclui uma despesa existente por id
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