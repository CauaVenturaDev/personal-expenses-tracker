using Microsoft.EntityFrameworkCore;
using personalExpensesTracker.Domain.Models;
using personalExpensesTracker.Infrastructure.Data;
using personalExpensesTracker.Infrastructure.Interfaces;

namespace personalExpensesTracker.Infrastructure.Repositories;

public class IncomeRepository(PersonalExpensesTrackerContext context) : IRepository<Income>
{
    private readonly PersonalExpensesTrackerContext _context = context;

    // Adiciona uma nova receita ao banco de dados
    public async Task<Income> AddAsync(Income income)
    {
        await _context.Incomes.AddAsync(income);
        await _context.SaveChangesAsync();
        return income;
    }

    // Recupera uma receita pelo seu ID
    public async Task<Income?> GetByIdAsync(int id)
    {
       return await _context.Incomes.FindAsync(id);
    }

    // Recupera todas as receitas do banco de dados
    public async Task<List<Income>> GetByMonthAsync(int month, int year)
    {
        var incomesByMonth = await _context.Incomes
            .Where(i => i.Date.Month == month && i.Date.Year == year)
            .GroupBy(e => e.Date).ToListAsync();
        return incomesByMonth.SelectMany(i => i).ToList();

    }

    // Calcula o total de receitas para um mês e ano específicos
    public async Task<decimal> GetTotalByMonthAsync(int month, int year)
    {
             return await _context.Incomes
            .Where(i => i.Date.Month == month && i.Date.Year == year)
            .SumAsync(i => i.Amount);
    }

    // Atualiza uma receita existente no banco de dados
    public async Task UpdateAsync(Income income)
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

    // Exclui uma receita do banco de dados
    public async Task DeleteAsync(Income income)
    { 
        _context.Incomes.Remove(income);
        await _context.SaveChangesAsync();
    }

    // Exclui todas as receitas do banco de dados e retorna a lista de receitas excluídas
    public async Task<List<Income>> DeleteAllAsync()
    {
        var allIncomes = await _context.Incomes.ToListAsync();
        _context.Incomes.RemoveRange(allIncomes);
        await _context.SaveChangesAsync();
        return allIncomes;
    }
}
