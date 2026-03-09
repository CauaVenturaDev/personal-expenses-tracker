using personalExpensesTracker.Application;
using personalExpensesTracker.Infrastructure.Data;
using personalExpensesTracker.Api;

//Projeto pausado momentaneamente

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterServices();

builder.Services.AddPersonalExpensesServices();

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

app.MapClientEndpoints();

app.Run();