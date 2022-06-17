var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Enable caching
builder.Services.AddMemoryCache();

var app = builder.Build();

app.MapControllers();

app.Run();