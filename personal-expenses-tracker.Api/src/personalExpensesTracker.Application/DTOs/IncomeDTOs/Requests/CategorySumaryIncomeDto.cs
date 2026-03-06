namespace personalExpensesTracker.Application.DTOs.IncomeDTOs.Requests;

public class CategorySumaryIncomeDto
{
    public required string Category { get; set; }
    public decimal Total { get; set; }
    public double Percentage { get; set; }
}