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

// Registra repositórios e serviços no contêiner de injeção de dependência
builder.Services.AddScoped<IRepository<Expense>, ExpensesRepository>();
builder.Services.AddScoped<IServices<Expense, CategorySumaryExpenseDto, ExpenseCreateDTO>, ExpensesServices>();

builder.Services.AddScoped<IRepository<Income>, IncomeRepository>();
builder.Services.AddScoped<IServices<Income, CategorySumaryIncomeDto, IncomeCreateDTO>, IncomeServices>();

// Adiciona serviços ao conteiner.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Enable Swagger in development environment
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

