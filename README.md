# .NET Aspire + Azure Container Apps + Gen AI Application Demo 🚀

[![.NET 9](https://img.shields.io/badge/.NET-9.0-512BD4?logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![C# 13](https://img.shields.io/badge/C%23-13.0-239120?logo=csharp&logoColor=white)](https://learn.microsoft.com/dotnet/csharp/)
[![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-9.0-5C2D91?logo=dotnet&logoColor=white)](https://learn.microsoft.com/aspnet/core/)
[![Blazor](https://img.shields.io/badge/Blazor-Server-5C2D91?logo=dotnet&logoColor=white)](https://learn.microsoft.com/aspnet/core/blazor/)
[![.NET Aspire](https://img.shields.io/badge/.NET%20Aspire-Cloud%20Native-512BD4?logo=dotnet&logoColor=white)](https://learn.microsoft.com/dotnet/aspire/overview)
[![Azure Container Apps](https://img.shields.io/badge/Azure-Container%20Apps-0078D4?logo=microsoftazure&logoColor=white)](https://learn.microsoft.com/azure/container-apps/)

Explore .NET Aspire, Azure Container Apps, and Gen AI with this demo application.

## Microservice Use Cases 🔎

- Product discovery and search across a catalog
- Filtering and paging of search results
- Viewing detailed information for catalog entities
- Integrating with backend APIs to fetch and display data in interactive UI

These use cases showcase:
- Fast time-to-first-byte with server rendering
- Reliable service communication via typed API clients

## Semantic Search & AI Assistants 🤖

Bring intelligent, context-aware experiences to the app with semantic search and a product support chat:

- Semantic Product Search 🔎🧠  
  Uses vector embeddings to represent queries and catalog items in a shared semantic space. Similarity search returns the most relevant products even when keywords don’t match exactly.

- Vector Embeddings & Memory 💾  
  Employs Microsoft Semantic Kernel’s in-memory store for local development and rapid prototyping of vector search. This can be swapped for persistent/vector-capable backends in production scenarios if needed.

- Support Chat using Ollama 🗣️  
  Integrates with Ollama to run local LLMs for private, on-device inference. A Retrieval-Augmented Generation (RAG) flow can retrieve product context via embeddings and ground responses with relevant catalog data.

- Model & Runtime Notes ⚙️  
  Ollama typically runs locally (e.g., http://localhost:11434) and supports popular models (e.g., Llama, Mistral). Configure the model name and base URL via environment variables for easy portability.

Learn more:  
- Microsoft Semantic Kernel: https://github.com/microsoft/semantic-kernel  
- Ollama: https://ollama.com/

## Cloud-Native with .NET Aspire ☁️

Leverage .NET Aspire to simplify cloud-first development and local orchestration:
- App model via an AppHost to compose services for local dev and integration testing
- Service defaults for health checks, resiliency policies, and timeouts
- First-class telemetry with OpenTelemetry (traces, metrics, logs) and a local dashboard
- Opinionated component packages for common dependencies (e.g., caches, databases) when applicable
- Configuration and secrets management aligned with environment-specific settings
- Seamless path to containerized and cloud deployments

Learn more: https://learn.microsoft.com/dotnet/aspire/overview

## Architecture at a Glance 🏗️

- UI: Razor/Blazor components for pages and interactive elements
- Service Layer: Typed API clients encapsulating external/internal HTTP calls
- Hosting: ASP.NET Core minimal hosting, DI container composition, configuration, and logging
- Rendering: Server-side interactivity (Blazor Server) with streaming rendering to progressively deliver UI

## Getting Started 🏁

Prerequisites:
- .NET SDK 9.0 or later

Restore and run:
- dotnet restore
- dotnet run

The application will start on the configured URL (by default, a localhost port). Open your browser to the displayed address.

## Configuration ⚙️

- appsettings.json: Base configuration
- appsettings.{Environment}.json: Environment overrides
- Environment variables: Override settings for containerized/hosted environments

Common settings:
- API endpoints and base addresses for service integrations
- Logging levels and providers

Tip: Prefer environment variables for secrets and deployment-specific URLs.

## Deployment: Azure Container Apps 🚢

A streamlined path to deploy as managed containers on Azure:
- Containerize: Build an OCI image and tag it with your registry name
- Registry: Push to Azure Container Registry (ACR) and grant the runtime identity pull permissions
- Environment: Create an Azure Container Apps Environment (includes Log Analytics workspace)
- App Deployment: Create a Container App with external ingress and set environment variables for configuration (e.g., API base URLs)
- Scale: Configure min/max replicas and autoscaling rules appropriate for your workload characteristics
- Observability: Forward logs/metrics to Azure Monitor; export OTel telemetry as needed

Docs:
- Azure Container Apps overview: https://learn.microsoft.com/azure/container-apps/
- Quickstart (deploy container): https://learn.microsoft.com/azure/container-apps/get-started

## Development Workflow 🛠️

- Run/Debug in JetBrains Rider:
  - Open the solution
  - Use Run/Debug configurations or the play button to launch
- CLI:
  - dotnet build
  - dotnet test (if test projects are present)
  - dotnet run

Hot reload and iterative development are supported by the .NET tooling and Rider/VS integration.

## Production Considerations 🔒

- Configure API base URLs via environment variables
- Enable appropriate logging and tracing
- Utilize reverse proxy (e.g., Nginx/IIS) and TLS termination
- Scale the app for Blazor Server with connection limits and sticky sessions if needed
- Use health probes and resource limits to improve resiliency and cost efficiency in container platforms

---

If you need additional sections (e.g., API documentation, deployment, or environment setup), let me know and I’ll extend this README.
