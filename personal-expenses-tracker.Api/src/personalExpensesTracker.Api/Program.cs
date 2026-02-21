using Microsoft.EntityFrameworkCore;
using personalExpensesTracker.Application.Interfaces;
using personalExpensesTracker.Application.Services;
using personalExpensesTracker.Infrastructure;
using personalExpensesTracker.Infrastructure.Interfaces;
using personalExpensesTracker.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterServices();

// Registra repositórios e serviços no contêiner de injeção de dependência
builder.Services.AddScoped<IExpensesRepository, ExpensesRepository>();
builder.Services.AddScoped<IExpensesServices, ExpensesServices>();

builder.Services.AddScoped<IIncomeRepository, IncomeRepository>();
builder.Services.AddScoped<IIncomeServices, IncomeServices>();

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

