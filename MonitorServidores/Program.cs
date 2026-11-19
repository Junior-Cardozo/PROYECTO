using Microsoft.EntityFrameworkCore;
using MonitorServidores.Data;
var builder = WebApplication.CreateBuilder(args);
var conexion = builder.Configuration.GetConnectionString("Postgres")
    ?? throw new InvalidOperationException(
        "No se configuró la conexión a PostgreSQL."
    );

builder.Services.AddDbContext<MonitorDbContext>(options =>
    options.UseNpgsql(conexion)
);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
