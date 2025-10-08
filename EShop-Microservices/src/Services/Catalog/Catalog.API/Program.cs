


var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddCarter();

var assembly = typeof(Program).Assembly;

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>)); // add validation behavior
    config.AddOpenBehavior(typeof(LoggingBehavior<,>)); // add logging behavior
});
builder.Services.AddValidatorsFromAssembly(assembly); // scans the assembly for validators and registers them

// Marten ORM
builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

if (builder.Environment.IsDevelopment())
    builder.Services.InitializeMartenWith<CatalogInitialData>(); // seed data

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Database")!);

var app = builder.Build();

// 
//  Wipe & reseed database on startup (development only)
// 
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var store = scope.ServiceProvider.GetRequiredService<IDocumentStore>();

    // 1️ Wipe the Marten database completely
    await store.Advanced.Clean.CompletelyRemoveAllAsync();

    // 2️ Reseed data
    var seeder = new CatalogInitialData();
    await seeder.Populate(store, default);

    Console.WriteLine("✅ Catalog database wiped and reseeded successfully.");
}

// Configure the HTTP request pipeline
app.MapCarter();

app.UseExceptionHandler(options => { });

app.UseHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

app.Run();
