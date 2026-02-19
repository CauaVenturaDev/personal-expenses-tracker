namespace personalExpensesTracker.Application.DTOs;

public class CategorySumaryDto
{
    public string Category { get; set; } = string.Empty;
    public decimal Total { get; set; }
    public double Percentage { get; set; }

}
