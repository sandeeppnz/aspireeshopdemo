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

var rabbitmq = builder.AddRabbitMQ("rabbitmq")
    .WithManagementPlugin()
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent);

var keycloak = builder
    .AddKeycloak("keycloak", 8089)
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent);

// Projects
var catalogService = builder.AddProject<Projects.Catalog>("catalog")
    .WithReference(catalogDb)
    .WithReference(rabbitmq)
    .WaitFor(catalogDb)
    .WaitFor(rabbitmq);

var basketService = builder.AddProject<Projects.Basket>("basket")
    .WithReference(redisCache)
    .WithReference(rabbitmq)
    .WithReference(keycloak)
    .WithReference(catalogService)
    .WaitFor(redisCache)
    .WaitFor(rabbitmq)
    .WaitFor(keycloak);


builder.Build().Run();