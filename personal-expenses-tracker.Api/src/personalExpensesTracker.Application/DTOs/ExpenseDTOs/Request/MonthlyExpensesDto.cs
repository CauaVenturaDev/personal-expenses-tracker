using personalExpensesTracker.Domain.Entity.Models;

namespace personalExpensesTracker.Application.DTOs.ExpenseDTOs.Request;

public class MonthlyExpensesDto
{
    public int Year { get; set; }
    public int Month { get; set; }
    public List<Expense> Expenses { get; set; } = new();
}