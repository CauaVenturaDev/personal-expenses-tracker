using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using personalExpensesTracker.Models;
using personalExpensesTracker.Models.Models;

namespace personalExpensesTracker.Data;

public partial class PersonalExpensesTrackerContext : DbContext
{
    // Constructor for dependency injection

    public PersonalExpensesTrackerContext()
    {
    }
    public PersonalExpensesTrackerContext(DbContextOptions<PersonalExpensesTrackerContext> options)
        : base(options)
    {
    }
    // DbSet properties for the entities
    public virtual DbSet<Expense> Expenses { get; set; }

    public virtual DbSet<Income> Incomes { get; set; }

    // OnModelCreating method to apply configurations
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PersonalExpensesTrackerContext).Assembly);
    }
}
