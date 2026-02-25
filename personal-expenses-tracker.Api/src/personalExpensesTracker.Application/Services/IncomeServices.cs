using personalExpensesTracker.Application.DTOs.IncomeDTOs.Requests;
using personalExpensesTracker.Application.Interfaces;
using personalExpensesTracker.Domain.Models;
using personalExpensesTracker.Infrastructure.Interfaces;

namespace personalExpensesTracker.Application.Services;

public class IncomeServices(IRepository<Income> repository) : IServices<Income, CategorySumaryIncomeDto, IncomeCreateDTO>
{

    private readonly IRepository<Income> _incomeRepository = repository;


    // Adiciona uma nova receita, validando se o valor é maior que zero
    public async Task<Income> AddAsync(Income income)
    {
        if (income.Amount == 0)
        {
            throw new Exception("Amount must be greater than zero.");
        }
        return await _incomeRepository.AddAsync(income);
    }


    //Lista as despesas por categoria, total e porcentagem do total mensal
    public async Task<List<Income>> GetByMonthAsync(int month, int year)
    {
        return await _incomeRepository.GetByMonthAsync (month, year);
    }


    //Lista as despesas por categoria, total e porcentagem do total mensal
    public async Task<List<CategorySumaryIncomeDto>> GetTotalByCategoryAsync(int month, int year)
    {
        var incomes = await _incomeRepository.GetByMonthAsync(month, year);

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


    // Calcula o total gasto no mês e ano especificados
    public async Task<decimal> GetTotalByMonthAsync(int month, int year)
    {
        return await _incomeRepository.GetTotalByMonthAsync(month, year);
    }


    // Atualiza uma despesa existente
    public async Task<Income> UpdateAsync(int id, IncomeCreateDTO incomeCreateDTO)
    {
        var existingIncome = await _incomeRepository.GetByIdAsync(id);

        if (existingIncome == null)
        {
            throw new KeyNotFoundException($"Income with id {id} not found.");
        }
        existingIncome.Description = incomeCreateDTO.Description;
        existingIncome.Amount = incomeCreateDTO.Amount;
        existingIncome.Category = incomeCreateDTO.Category;
        existingIncome.Date = incomeCreateDTO.Date;

        await _incomeRepository.UpdateAsync(existingIncome);
        return existingIncome;
    }


    // Exclui uma despesa existente por id
    public async Task DeleteAsync(int id)
    {
        var existingIncome = await _incomeRepository.GetByIdAsync(id);
        if (existingIncome is null)
        {
            throw new KeyNotFoundException($"Income with id {id} not found.");
        }
        await _incomeRepository.DeleteAsync(existingIncome);
    }


    // Exclui todas as despesas do banco de dados
    public async Task DeleteAllAsync()
    {
        await _incomeRepository.DeleteAllAsync();

    }

}
