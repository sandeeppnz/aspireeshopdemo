using System.Reflection;
using Catalog.Data;
using Catalog.Endpoints;
using Catalog.Services;
using ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddServiceDefaults();

builder.AddNpgsqlDbContext<ProductDbContext>(connectionName: "catalogdb");

builder.Services.AddScoped<ProductService>();

builder.Services.AddMassTransitWithAssemblies(Assembly.GetExecutingAssembly());

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.AddOllamaSharpChatClient("ollama-llama3-2");

var app = builder.Build();

app.MapDefaultEndpoints();

app.MapProductEndpoints(); //add product endpoints

app.UseMigration();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();