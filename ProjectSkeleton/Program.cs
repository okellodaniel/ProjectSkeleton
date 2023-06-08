using Application;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Presentation;
using ProjectSkeleton.Data;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));
builder.Services.AddDbContext<TodoDb>(opt => opt.UseInMemoryDatabase("TodoList"));
builder.Services.AddDbContext<TodoDb>(x => x.UseNpgsql(builder.Configuration.GetConnectionString("TodoDb")));


builder.Services.AddApplication()
                .AddInfrustructure()
                .AddAPresentation();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
    {
        Title = builder.Environment.ApplicationName,
        Version = "v1"
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json",
        $"{builder.Environment.ApplicationName} v1"));
}

app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.Run();
