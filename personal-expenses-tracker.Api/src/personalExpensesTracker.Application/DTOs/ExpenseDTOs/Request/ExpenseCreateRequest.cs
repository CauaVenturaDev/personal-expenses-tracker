namespace personalExpensesTracker.Application.DTOs.ExpenseDTOs.Request;

public class ExpenseCreateRequest
{
    public decimal Amount { get; set; }
    public required string Description { get; set; }
    public DateOnly Date { get; set; }
    public required string Category { get; set; }
}