using Microsoft.EntityFrameworkCore;
using personalExpensesTracker.Domain.Models;
using personalExpensesTracker.Infrastructure.Data;
using personalExpensesTracker.Infrastructure.Interfaces;


namespace personalExpensesTracker.Infrastructure.Repositories;

public class ExpensesRepository(PersonalExpensesTrackerContext context) : IRepository<Expense>
{
    private readonly PersonalExpensesTrackerContext _context = context;


    // Adiciona uma nova despesa ao banco de dados
    public async Task<Expense> AddAsync(Expense expense)
    {
        await _context.AddAsync(expense);
        await _context.SaveChangesAsync();
        return expense;
    }


    // Recupera todas as despesas do banco de dados em uma lista
    public async Task<List<Expense>> GetByMonthAsync(int month, int year)
    {
        var ExpensesByMonth = await _context.Expenses
            .Where(e => e.Date.Month == month && e.Date.Year == year)
            .GroupBy(e => e.Date).ToListAsync();
        return ExpensesByMonth.SelectMany(g => g).ToList();
    }


    // Calcula o total de despesas para um mês e ano específicos
    public async Task<decimal> GetTotalByMonthAsync(int month, int year)
    {
        return await _context.Expenses
            .Where(e => e.Date.Month == month && e.Date.Year == year)
            .SumAsync(e => e.Amount);
    }


    // Recupera uma despesa específica pelo seu id
    public async Task<Expense?> GetByIdAsync(int id)
    {
        return await _context.Expenses.FindAsync(id);
    }


    // Atualiza uma despesa existente no banco de dados
    public async Task UpdateAsync(Expense expense)
    {
        var existingExpense = await _context.Expenses.FindAsync(expense.Id);
        if (existingExpense == null)
        {
            throw new Exception("Expense not found");
        }

        existingExpense.Description = expense.Description;
        existingExpense.Amount = expense.Amount;
        existingExpense.Category = expense.Category;
        existingExpense.Date = expense.Date;

        await _context.SaveChangesAsync();

    }


    // Exclui uma despesa do banco de dados
    public async Task DeleteAsync(Expense expense)
    {
        _context.Expenses.Remove(expense);
        await _context.SaveChangesAsync();
    }


    // Exclui todas as despesas do banco de dados e retorna lista das despesas excluídas
    public async Task<List<Expense>> DeleteAllAsync()
    {
        var allExpenses = await _context.Expenses.ToListAsync();
        _context.Expenses.RemoveRange(allExpenses);
        await _context.SaveChangesAsync();
        return allExpenses;
    }
}

