var builder = DistributedApplication.CreateBuilder(args);

// PostgreSQL Database
var postgres = builder.AddPostgres("postgres");

var authDb = postgres.AddDatabase("auth-db");
var vaultDb = postgres.AddDatabase("vault-db");

// Identity service
var authService = builder
    .AddProject<Projects.DotLock_Services_Auth>("auth-service")
    .WithReference(authDb);

// Vault service
var vaultService = builder
    .AddProject<Projects.DotLock_Services_Vault>("vault-service")
    .WithReference(vaultDb);

// Audit service
var auditService = builder
    .AddProject<Projects.DotLock_Services_Audit>("audit-service");

// API Gateway
var apiGateway = builder
    .AddProject<Projects.DotLock_ApiGateway>("api-gateway")
    .WithReference(authService)
    .WithReference(vaultService)
    .WithReference(auditService);

// Client Web (Blazor)
var webClient = builder
    .AddProject<Projects.DotLock_Clients_Web>("web-client")
    .WithReference(apiGateway);

builder.Build().Run();