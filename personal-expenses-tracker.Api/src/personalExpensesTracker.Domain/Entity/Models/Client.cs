namespace personalExpensesTracker.Domain.Entity.Models;

public class Client
{
    public required Guid Id { get; set; }

    public required string Name { get; set; }

    public required string Email { get; set; }

    public required string Password { get; set; }

    public required string Username { get; set; }

    public DateTime CreatedAt { get; set; }


    public ICollection<Expense> Expenses { get; set; } = new List<Expense>(); 

    public ICollection<Income> Incomes { get; set; } =  new List<Income>();
}