using System.Reflection;
using Basket.ApiClients;
using Basket.Endpoints;
using Basket.Services;
using ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddRedisDistributedCache(connectionName: "cache"); // ConnectionString__cache
builder.Services.AddScoped<BasketService>();

builder.Services.AddHttpClient<CatalogApiClient>(client =>
{
    client.BaseAddress = new("https+http://catalog");
});

// Add services to the container.
builder.Services.AddMassTransitWithAssemblies(Assembly.GetExecutingAssembly());


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddAuthentication()
    .AddKeycloakJwtBearer(
        serviceName: "keycloak",
        realm: "eshop",
        configureOptions: options =>
        {
            options.RequireHttpsMetadata = false;
            // options.Authority = "http://keycloak:8089/auth/realms/eshop";
            options.Audience = "account";
        });
builder.Services.AddAuthorization();

var app = builder.Build();

app.MapDefaultEndpoints();
app.MapBasketEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.Run();
