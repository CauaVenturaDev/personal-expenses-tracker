namespace personalExpensesTracker.Domain.Entity.Models;

public partial class Income
{
    public int Id { get; set; }

    public Guid ClientId { get; set; }

    public string Description { get; set; } = null!;

    public decimal Amount { get; set; }

    public string Category { get; set; } = null!;

    public DateOnly Date { get; set; }
}