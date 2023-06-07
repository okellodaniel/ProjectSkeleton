using Microsoft.EntityFrameworkCore;
using ProjectSkeleton.Core.Entities;
using ProjectSkeleton.Data;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));
builder.Services.AddDbContext<TodoDb>(opt => opt.UseInMemoryDatabase("TodoList"));
builder.Services.AddDbContext<TodoDb>(x => x.UseNpgsql(builder.Configuration.GetConnectionString("TodoDb")));
//builder.Services.AddDatabaseDeveloperPageExceptionFilter();


var app = builder.Build();

app.MapGet("/todoitems",  async(TodoDb db) =>  
    await db.Todos.ToListAsync());

app.MapGet("/todoitems/complete", async (TodoDb db) =>
    await db.Todos.Where(t => t.IsComplete).ToListAsync());

app.MapGet("/todoitems/{id}", async (int id,TodoDb db) => await db.Todos.FirstOrDefaultAsync(x => x.Id == id) is Todo todo ? Results.Ok(todo) : Results.NotFound());

app.MapPost("/todoitems/", async (Todo todo, TodoDb db) =>
{
    if (todo == null) throw new Exception(nameof(todo));
    await db.AddAsync(todo);
    await db.SaveChangesAsync();

    return Results.Created($"/todoitems/{todo.Id}", todo);
});

app.MapPut("/todoitems/{id}", async (int id, TodoDb db, Todo inputTodo) =>
{
    var todo = await db.Todos.FindAsync(id);

    if (todo is null) return Results.NotFound();

    todo.Name = inputTodo.Name;
    todo.IsComplete = inputTodo.IsComplete;

    await db.SaveChangesAsync();

    return Results.NoContent();

});

app.MapDelete("/todoitems/{id}", async (int id, TodoDb db) =>
{
    if (await db.Todos.FindAsync(id) is Todo todo)
    {
        db.Todos.Remove(todo);
        await db.SaveChangesAsync();
        return Results.Ok(todo);
    }

    return Results.NotFound();
});



app.Run();
