var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var app = builder.Build();

// Conigure the HTTP request pipeline.

app.Run();
