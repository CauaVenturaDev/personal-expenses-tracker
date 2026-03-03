namespace personalExpensesTracker.Domain.Models;

public class Client
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? Username { get; set; }


    public ICollection<Expense> Expenses { get; set; }

    public ICollection<Income> Incomes { get; set; } 
}