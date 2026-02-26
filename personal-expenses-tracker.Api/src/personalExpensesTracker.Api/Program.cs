using personalExpensesTracker.Application.DTOs.ExpenseDTOs.Request;
using personalExpensesTracker.Application.DTOs.IncomeDTOs.Requests;
using personalExpensesTracker.Application.Interfaces;
using personalExpensesTracker.Application.Services;
using personalExpensesTracker.Domain.Models;
using personalExpensesTracker.Infrastructure.Data;
using personalExpensesTracker.Infrastructure.Interfaces;
using personalExpensesTracker.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterServices();

builder.Services.AddScoped<IRepository<Expense>, ExpensesRepository>();
builder.Services.AddScoped<IServices<Expense, CategorySumaryExpenseDto, ExpenseCreateDTO, MonthlyExpensesDto>, ExpensesServices>();

builder.Services.AddScoped<IRepository<Income>, IncomeRepository>();
builder.Services.AddScoped<IServices<Income, CategorySumaryIncomeDto, IncomeCreateDTO, MonthlyIncomesDto>, IncomeServices>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

