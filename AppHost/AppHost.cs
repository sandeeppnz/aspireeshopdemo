using Projects;

var builder = DistributedApplication.CreateBuilder(args);

// Backing services
var postgres = builder.AddPostgres("postgres")
    .WithPgAdmin()
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent);

var catalogDb = postgres.AddDatabase("catalogdb");

var redisCache = builder.AddRedis("cache")
    .WithRedisInsight()
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent);

// Projects
var catalog = builder.AddProject<Projects.Catalog>("catalog")
    .WithReference(catalogDb)
    .WaitFor(catalogDb);

var basket = builder.AddProject<Projects.Basket>("basket")
    .WithReference(redisCache)
    .WaitFor(redisCache);

builder.Build().Run();