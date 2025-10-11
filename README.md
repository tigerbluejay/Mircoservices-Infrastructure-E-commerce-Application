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
│   ├── ApiGateways/
│   └── WebApps/
├── docker-compose.yml            # Container orchestration
├── docker-compose.override.yml
├── README.md
└── .env
```

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
