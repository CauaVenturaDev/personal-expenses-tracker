namespace personalExpensesTracker.Application.DTOs.IncomeDTOs.Response;

public class IncomeResponse
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Category { get; set; } = string.Empty;
    public DateOnly Date { get; set; }
}
