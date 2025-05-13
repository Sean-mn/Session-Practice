var builder = WebApplication.CreateBuilder(args);

var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION");
if (string.IsNullOrEmpty(connectionString))
    throw new InvalidOperationException("Database connection is null.");

builder.Services.

builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();