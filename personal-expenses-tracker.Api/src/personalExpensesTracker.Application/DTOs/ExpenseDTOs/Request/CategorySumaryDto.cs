namespace personalExpensesTracker.Application.DTOs.ExpenseDTOs.Request;

public class CategorySumaryDto
{
    public string Category { get; set; } = string.Empty;
    public decimal Total { get; set; }
    public double Percentage { get; set; }
}
