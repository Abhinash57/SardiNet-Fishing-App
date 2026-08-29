var builder = DistributedApplication.CreateBuilder(args);

var postgresServer = builder.AddPostgres("postgres-server")
    .WithImage("imresamu/postgis")
    .WithImageTag("17-3.6.1-bookworm")
    .WithHostPort(4321)
    .WithDataVolume("pgdata-v17")
    .WithLifetime(ContainerLifetime.Session);

var testDatabase = postgresServer.AddDatabase("api-db");
var identityDatabase = postgresServer.AddDatabase("identity-db");
var mediaDatabase = postgresServer.AddDatabase("media-db");

var redis = builder.AddRedis("auth-cache");
var appCache = builder.AddRedis("app-cache");

var gatewayUrl = builder.Configuration["GatewayUrl"] 
    ?? throw new InvalidOperationException("GatewayUrl not in config");
var mailtrapKey = builder.Configuration["Mailtrap:ApiKey"]
    ?? throw new InvalidOperationException("Mailtrap ApiKey not in config");

var identityService = builder.AddProject<Projects.PVT15_8_Identity>("identity-service")
    .WithExternalHttpEndpoints()
    .WithEnvironment("GatewayUrl", gatewayUrl)
    .WithEnvironment("Mailtrap__ApiKey", mailtrapKey)
    .WithReference(identityDatabase)
    .WaitFor(identityDatabase)
    .WithReference(redis)
    .WaitFor(redis);

var apiService = builder.AddProject<Projects.PVT15_8_ApiService>("apiservice")
    .WithExternalHttpEndpoints()
    .WithEnvironment("GatewayUrl", gatewayUrl)
    .WithReference(testDatabase)
    .WaitFor(testDatabase);

var external = builder.AddProject<Projects.PVT15_8_External>("external-service")
    .WithReference(appCache);

var media = builder.AddProject<Projects.PVT15_8_Media>("media-service")
    .WithEnvironment("GatewayUrl", gatewayUrl)
    .WaitFor(mediaDatabase)
    .WithReference(mediaDatabase);

builder.AddProject<Projects.PVT15_8_Gateway>("gateway")
    .WithReference(identityService)
    .WaitFor(identityService)
    .WithReference(apiService)
    .WaitFor(apiService)
    .WithReference(redis)
    .WaitFor(redis)
    .WithReference(external)
    .WithReference(media)
    .WithHttpEndpoint(port: 5001, name: "cloudflared");

builder.Build().Run();
