using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using personalExpensesTracker.Domain.Entity.Models;
using personalExpensesTracker.Infrastructure.Data;
namespace personalExpensesTracker.Api;

public static class ClientEndpoints
{
    public static void MapClientEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Client").WithTags(nameof(Client));

        group.MapGet("/", async (PersonalExpensesTrackerContext db) =>
        {
            return await db.Client.ToListAsync();
        })
        .WithName("GetAllClients");

        group.MapGet("/{id}", async Task<Results<Ok<Client>, NotFound>> (Guid id, PersonalExpensesTrackerContext db) =>
        {
            return await db.Client.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is Client model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetClientById");

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (Guid id, Client client, PersonalExpensesTrackerContext db) =>
        {
            var affected = await db.Client
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.Id, client.Id)
                    .SetProperty(m => m.Name, client.Name)
                    .SetProperty(m => m.Email, client.Email)
                    .SetProperty(m => m.Password, client.Password)
                    .SetProperty(m => m.Username, client.Username)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateClient");

        group.MapPost("/", async (Client client, PersonalExpensesTrackerContext db) =>
        {
            db.Client.Add(client);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Client/{client.Id}",client);
        })
        .WithName("CreateClient");

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (Guid id, PersonalExpensesTrackerContext db) =>
        {
            var affected = await db.Client
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteClient");
    }
}
