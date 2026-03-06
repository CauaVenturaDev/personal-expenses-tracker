namespace personalExpensesTracker.Application.DTOs.IncomeDTOs.Requests;

public class IncomeCreateRequest
{
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Category { get; set; } = string.Empty;
    public DateOnly Date { get; set; }
}