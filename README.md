# 🏪 E-Commerce Platform

This is a demonstration of Neil Cummings’s .NET Microservices E-Commerce Application. I implemented the application step by step, and all the code is available on my GitHub account at github.com/tigerbluejay

Building this app helped me understand microservice architecture, asynchronous communication, and containerized orchestration using Docker. It also gave me practical experience working with multiple .NET project types and integrating them within a distributed environment.

The solution consists of seven projects: four services (Catalog, Basket, Discount, and Ordering), an API Gateway, a Building Blocks project that includes the message broker and shared utilities, and a Web Client built with Razor Pages.

The Catalog and Basket services use Marten with a DocumentStore over PostgreSQL, the Discount service uses SQLite, and the Ordering service uses SQL Server. All services run in separate containers.
The Basket and Ordering Microservices communicate asynchronously through the message broker RabbitMQ using MassTransit included in the Building Blocks project.

This application was built entirely in .NET 8 and demonstrates a modern, modular approach to backend development. 

## 🚀 Features

- Microservices Architecture – Independent, loosely coupled services for Catalog, Basket, Discount, Ordering, and more.
- Service-to-Service Communication – Uses gRPC, HTTP, and RabbitMQ message bus for synchronous and asynchronous workflows.
- Event-Driven Messaging – Integrates RabbitMQ for reliable inter-service event propagation.
- API Gateway – Centralized entry point using Ocelot for routing and aggregation.
- Distributed Caching – Implements Redis for high-performance data caching.
- NoSQL and SQL Datastores – Uses Marten ORM with Document Store for PostgreSQL, SQLite, and SQL Server on distinct microservices.
- Containerization – All services run in Docker containers for easy deployment and orchestration.

## 🛠 Tech Stack

### ✅ Backend

- ASP.NET Core 8 (Web API, gRPC, Razor Pages, Blank Libraries)
- Entity Framework Core
- RabbitMQ and MassTransit
- Minimal APIs

### ✅ Databases

- DocumentStore No-SQL over PostgreSQL (Catalog Service)
- Redis Cache
- DocumentStore No-SQL over PostgreSQL (Basket Service)
- SQLite (Discount Service)
- SQL Server (Ordering Service)

### ✅ Infrastructure & Tools

- Docker & Docker Compose

## 🗂 Project Structure
```plaintext
Microservices-Infrastructure-E-commerce-Application/
│
├── src/
│   ├── Services/
│   │   ├── Catalog.API/          # Product catalog (PostgreSQL)
│   │   ├── Basket.API/           # Shopping basket (PostgreSQL)
│   │   ├── Discount.API/         # Discounts (SQLite)
│   │   ├── Ordering.API/         # Orders and checkout (SQL Server)
│   ├── BuildingBlocks/           # Shared libraries (Common, EventBus, etc.)
│   ├── ApiGateway/
│   └── WebApp/
├── docker-compose.yml            # Container orchestration
├── docker-compose.override.yml
├── README.md
└── .env
```

## 🧩 Patterns, Packages and Technologies

✅ Catalog Microservice

- ASP.NET Core Minimal APIs and latest features of .NET 8 and C# 12
- Vertical Slice Architecture implementation with Feature folders
- CQRS implementation using MediatR library
- CQRS Validation Pipeline Behaviours with MediatR and FluentValidation
- Marten library for .NET Transactional Document DB on PostgreSQL
- Carter library for Minimal API endpoint definition
- Cross-cutting concerns Logging, global Exception Handling and Health Checks
- Dockerfile and docker-compose file for running Multi-container in Docker environment

✅ Basket Microservice

- ASP.NET 8 Web API application, Following REST API principles, CRUD operations
- Redis as a Distributed Cache over basketdb
- Implements Proxy, Decorator and Cache-aside Design Patterns
- Consumes Discount gRPC Service for inter-service sync communication to calculate product final price
- Publishes BasketCheckout Queue with using MassTransit and RabbitMQ

✅ Discount Microservice

- ASP.NET gRPC Server application
- Build a Highly Performant inter-service gRPC Communication with Basket Microservice
- Exposing gRPC Services with creating Protobuf messages
- Entity Framework Core ORM - SQLite Data Provider and Migrations
- SQLite database connection and containerization

✅ Microservices Communication

- Sync inter-service gRPC Communication
- Async Microservices Communication with RabbitMQ Message-Broker Service
- Using RabbitMQ Publish/Subscribe Topic Exchange Model
- Using MassTransit for abstraction over RabbitMQ Message-Broker system
- Publishing BasketCheckout event queue from Basket microservices and Subscribing this event from Ordering microservices
- Create RabbitMQ EventBus.Messages library and add references Microservices

✅ Ordering Microservice

- Implementing DDD, CQRS, and Clean Architecture with using Best Practices
- Developing CQRS with using MediatR, FluentValidation and Mapster packages
- Use Domain Events & Integration Events
- Entity Framework Core Code-First Approach, Migrations, DDD Entity Configurations
- Consuming RabbitMQ BasketCheckout event queue with using MassTransit-RabbitMQ Configuration
- SqlServer database connection and containerization
- Using Entity Framework Core ORM and auto migrate to SqlServer when application startup

✅ Yarp API Gateway

- Implement API Gateways with Yarp Reverse Proxy applying Gateway Routing Pattern
- Yarp Reverse Proxy Configuration; Route, Cluster, Path, Transform, Destinations
- Rate Limiting with FixedWindowLimiter on Yarp Reverse Proxy Configuration
- Sample microservices/containers to reroute through the API Gateways

✅ WebUI ShoppingApp Client

- ASP.NET Core Web Application with Bootstrap 4 and Razor template
- Consume YarpApiGateway APIs using Refit Library with Generated HttpClientFactory
- ASPNET Core Razor Tools — View Components, partial Views, Tag Helpers, Model Bindings and Validations, Razor Sections etc.

✅ Docker Compose integrating all Microservices, Databases and Data Stores, Message Broker, API Gateway and Client

- Containerization of microservices
- Orchestrating of microservices and backing services (databases, distributed caches, message brokers..)
- Overriding Environment variables

## ⚙ Getting Started
### Clone the Repository
```bash
git clone https://github.com/tigerbluejay/Microservices-Infrastructure-E-commerce-Application.git
cd Microservices-Infrastructure-E-commerce-Application
```

### Run with Docker Compose

Make sure Docker is installed and running, then execute:
```bash
docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d
```
This will start all microservices, databases, RabbitMQ, and Elastic Stack containers.

### Access the Services
Service	URL	Description
- https://localhost:6065

Each service is designed to runs orchestrated in a docker container and communicates via HTTP/gRPC and RabbitMQ.

#### 💡 Development Notes

This project focuses on backend architecture and infrastructure, showcasing how a containerized .NET solution can be structured for scalability and reliability.
