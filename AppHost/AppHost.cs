using Projects;

var builder = DistributedApplication.CreateBuilder(args);

// Backing services
var postgres = builder.AddPostgres("postgres")
    .WithPgAdmin()
    // .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent);

var catalogDb = postgres.AddDatabase("catalogdb");

var redisCache = builder.AddRedis("cache")
    .WithRedisInsight()
    // .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent);

var rabbitmq = builder.AddRabbitMQ("rabbitmq")
    .WithManagementPlugin()
    // .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent);

var keycloak = builder
    .AddKeycloak("keycloak", 8089)
    // .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent);

if (builder.ExecutionContext.IsRunMode)
{
    postgres.WithDataVolume();
    //redisCache.WithDataVolume();
    rabbitmq.WithDataVolume();
    keycloak.WithDataVolume();   
}

var ollama = builder
    .AddOllama("ollama", 11434)
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent)
    .WithOpenWebUI();

var llama = ollama.AddModel("llama3.2");
var embedding = ollama.AddModel("mxbai-embed-large");

// Projects
var catalogService = builder.AddProject<Projects.Catalog>("catalog")
    .WithReference(catalogDb)
    .WithReference(rabbitmq)
    .WithReference(llama)
    .WithReference(embedding)
    .WaitFor(catalogDb)
    .WaitFor(rabbitmq)
    .WaitFor(llama)
    .WaitFor(embedding);;

var basketService = builder.AddProject<Projects.Basket>("basket")
    .WithReference(redisCache)
    .WithReference(rabbitmq)
    .WithReference(keycloak)
    .WithReference(catalogService)
    .WaitFor(redisCache)
    .WaitFor(rabbitmq)
    .WaitFor(keycloak);

var webapp = builder
    .AddProject<Projects.WebApp>("webapp")
    .WithExternalHttpEndpoints()
    .WithReference(redisCache)
    .WithReference(catalogService)    
    .WithReference(basketService)
    .WaitFor(catalogService)
    .WaitFor(basketService);

// CICD https://www.youtube.com/watch?v=qWtRFNLPkmg
builder.Build().Run();