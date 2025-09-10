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
var catalogService = builder.AddProject<Projects.Catalog>("catalog")
    .WithReference(catalogDb)
    .WaitFor(catalogDb);

var basketService = builder.AddProject<Projects.Basket>("basket")
    .WithReference(redisCache)
    .WithReference(catalogService)
    .WaitFor(redisCache);

builder.Build().Run();