using Microsoft.EntityFrameworkCore;
using personalExpensesTracker.Application.Interfaces;
using personalExpensesTracker.Application.Services;
using personalExpensesTracker.Infrastructure;
using personalExpensesTracker.Infrastructure.Interfaces;
using personalExpensesTracker.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterServices();

builder.Services.AddScoped<IExpensesRepository, ExpensesRepository>();
builder.Services.AddScoped<IExpensesServices, ExpensesServices>();

// Add services to the container.
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

