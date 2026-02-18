using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using personalExpensesTracker.Models;

namespace personalExpensesTracker.Data;

public partial class PersonalExpensesTrackerContext : DbContext
{

    public PersonalExpensesTrackerContext(DbContextOptions<PersonalExpensesTrackerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Expense> Expenses { get; set; }

    public virtual DbSet<Income> Incomes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PersonalExpensesTrackerContext).Assembly);
    }
}
