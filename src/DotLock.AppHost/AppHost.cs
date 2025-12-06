var builder = DistributedApplication.CreateBuilder(args);

// PostgreSQL Database
var postgres = builder.AddPostgres("postgres");

var identityDb = postgres.AddDatabase("identity-db");
var vaultDb = postgres.AddDatabase("vault-db");

// Identity service
var identityService = builder
    .AddProject<Projects.DotLock_Services_Identity>("identity-service")
    .WithReference(identityDb);

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
    .WithReference(identityService)
    .WithReference(vaultService)
    .WithReference(auditService);

// Client Web (Blazor)
var webClient = builder
    .AddProject<Projects.DotLock_Clients_Web>("web-client")
    .WithReference(apiGateway);

builder.Build().Run();