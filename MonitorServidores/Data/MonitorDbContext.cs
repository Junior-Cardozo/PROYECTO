using Microsoft.EntityFrameworkCore;
using MonitorServidores.Models;

namespace MonitorServidores.Data;

public class MonitorDbContext : DbContext
{
    public MonitorDbContext(DbContextOptions<MonitorDbContext> options)
        : base(options)
    {
    }

    public DbSet<Servidor> Servidores { get; set; }
}