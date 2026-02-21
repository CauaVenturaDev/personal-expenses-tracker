namespace personalExpensesTracker.Application.DTOs.ExpenseDTOs.Response;

public class ExpenseResponse
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateOnly Date { get; set; }
    public string Category { get; set; } = string.Empty;
}
