using personalExpensesTracker.Domain.Models;

namespace personalExpensesTracker.Application.DTOs.ExpenseDTOs.Request;

public class ExpenseCreateDTO
{
    public decimal Amount { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateOnly Date { get; set; }
    public string Category { get; set; } = string.Empty;
}
