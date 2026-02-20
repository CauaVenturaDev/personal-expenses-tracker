using Microsoft.EntityFrameworkCore;
using personalExpensesTracker.Domain.Models;


namespace personalExpensesTracker.Infrastructure.Repositories;

public class ExpensesRepository(PersonalExpensesTrackerContext context) : Interfaces.IExpensesRepository
{
    private readonly PersonalExpensesTrackerContext _context = context;


    // Adiciona uma nova despesa ao banco de dados
    public async Task<Expense> AddExpenseAsync(Expense expense)
    {
        await _context.AddAsync(expense);
        await _context.SaveChangesAsync();
        return expense;
    }


    // Recupera todas as despesas do banco de dados em uma lista
    public async Task<List<Expense>> GetExpensesByMonthAsync(int month, int year)
    {
        return await _context.Expenses
            .Where(e => e.Date.Month == month && e.Date.Year == year)
            .ToListAsync();

    }


    // Calcula o total de despesas para um mês e ano específicos
    public async Task<decimal> GetTotalByMonthAsync(int month, int year)
    {
        return await _context.Expenses
            .Where(e => e.Date.Month == month && e.Date.Year == year)
            .SumAsync(e => e.Amount);
    }


    // Recupera uma despesa específica pelo seu id
    public async Task<Expense> GetExpenseByIdAsync(int id)
    {
        return await _context.Expenses.FindAsync(id);
    }


    // Exclui uma despesa do banco de dados
    public async Task DeleteExpenseAsync(Expense expense)
    {
        _context.Expenses.Remove(expense);
        await _context.SaveChangesAsync();
    }


    // Atualiza uma despesa existente no banco de dados
    public async Task UpdateExpenseAsync(Expense expense)
    {
        var existingExpense = await _context.Expenses.FindAsync(expense.Id);
        if (existingExpense == null) {
            throw new Exception("Expense not found");
        }

        existingExpense.Description = expense.Description;
            existingExpense.Amount = expense.Amount;
            existingExpense.Category = expense.Category;
            existingExpense.Date = expense.Date;

        await _context.SaveChangesAsync();

    }
}
