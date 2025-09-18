using Microsoft.EntityFrameworkCore;
using ToDoAPI;

var builder = WebApplication.CreateBuilder(args);

// Add Dependency Injection - AddService

builder.Services.AddDbContext<ToDoDb>(opt => opt.UseInMemoryDatabase("ToDoList"));

var app = builder.Build();

app.MapGet("/todoitems", async (ToDoDb db) =>
    await db.ToDos.ToListAsync());

app.MapGet("/todoitems/{id}", async (int id, ToDoDb db) =>
    await db.ToDos.FindAsync(id));

app.MapPost("/todoitems", async (ToDoItem todo, ToDoDb db) =>
{
    db.ToDos.Add(todo);
    await db.SaveChangesAsync();
    return Results.Created($"/todoitems/{todo.Id}", todo);
});

app.MapPut("/todoitems/{id}", async (int id, ToDoItem inputToDo, ToDoDb db) =>
{
    var todo = await db.ToDos.FindAsync(id);
    if (todo == null) return Results.NotFound();
    todo.Name = inputToDo.Name;
    todo.IsComplete = inputToDo.IsComplete;
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/todoitems/{id}", async (int id, ToDoDb db) =>
{
    if (await db.ToDos.FindAsync(id) is ToDoItem todo)
    {
        db.ToDos.Remove(todo);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }
    return Results.NotFound();
});

// Configure Pipeline - UseMethod

app.Run();
