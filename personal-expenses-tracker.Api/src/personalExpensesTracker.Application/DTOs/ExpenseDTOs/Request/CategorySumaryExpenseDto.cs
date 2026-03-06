namespace personalExpensesTracker.Application.DTOs.ExpenseDTOs.Request;

public class CategorySumaryExpenseDto
{
    public required string Category { get; set; }
    public decimal Total { get; set; }
    public double Percentage { get; set; }
}