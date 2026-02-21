namespace personalExpensesTracker.Application.DTOs.IncomeDTOs.Requests;

public class CategorySumaryIncomeDto
{
    public string Category { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public decimal Percentage { get; set; }

}
