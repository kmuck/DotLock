# DotLock

This is an **early-stage** password manager built with a **micro-services** and **zero-knowledge** architecture using .NET 10.

It is designed as a full **mono-repo** / **mono-solution** to maximize compile-time consistency and enforce strict contract integrity.

Tech stack includes:
- **.NET Aspire** (orchestration, service discovery, diagnostics)
- **ASP.NET** Minimal APIs (microservices)
- **Blazor WebAssembly** (client-side crypto)
- **EF Core** + **PostgreSQL**
- **YARP** (reverse proxy, future routing/gateway layer)

## Architecture Overview

Services:
- **Auth Service:** authentication & user lifecycle
- **Vault Service:** encrypted vault storage (ciphertext only)
- **Audit Service:** security events & activity logging

Principles:
- **Zero-knowledge:** all encryption runs on the client; the backend stores only encrypted data.
- **Full .NET stack:** everything (services, shared libraries, client, host) is built and validated together.
- **Orchestrated by Aspire:** AppHost + Shared Defaults handle wiring.

## Prerequisites

- **.NET 10 SDK**
- **Docker Engine** (for PostgreSQL)

## Getting Started

```
dotnet restore
dotnet build
dotnet run --project src/DotLock.AppHost
```