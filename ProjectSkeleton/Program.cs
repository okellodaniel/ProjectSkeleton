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

var app = builder.Build();


app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.Run();
