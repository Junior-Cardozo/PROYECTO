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

    public DbSet<ReglaAlerta> ReglasAlerta { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ReglaAlerta>()
            .HasOne(r => r.Servidor)
            .WithMany()
            .HasForeignKey(r => r.ServidorId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ReglaAlerta>()
            .Property(r => r.Umbral)
            .HasPrecision(5, 2);
    }
}