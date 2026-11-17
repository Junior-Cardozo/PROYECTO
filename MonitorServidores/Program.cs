
using Microsoft.EntityFrameworkCore;
using MonitorServidores.Data;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

var conexion = builder.Configuration.GetConnectionString("Postgres")
    ?? throw new InvalidOperationException(
        "No se configuró la conexión a PostgreSQL."
    );

builder.Services.AddDbContext<MonitorDbContext>(options =>
    options.UseNpgsql(conexion)
);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

// Publicar OpenAPI también en producción (Docker).
app.MapOpenApi();

// Interfaz gráfica de Scalar.
app.MapScalarApiReference();

app.UseAuthorization();

app.MapControllers();

app.Run();
