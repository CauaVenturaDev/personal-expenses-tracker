using personalExpensesTracker.Domain.Entity.Models;

namespace personalExpensesTracker.Application.DTOs.IncomeDTOs.Requests;

public class MonthlyIncomesDto
{
    public int Year { get; set; }
    public int Month { get; set; }
    public List<Income> Incomes { get; set; } = new();
}
