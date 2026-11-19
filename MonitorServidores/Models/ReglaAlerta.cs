
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MonitorServidores.Models;

[Table("reglas_alerta")]
public class ReglaAlerta
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int ServidorId { get; set; }

    [Required]
    [MaxLength(100)]
    public string Nombre { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string Metrica { get; set; } = string.Empty;

    [Precision(5, 2)]
    public decimal Umbral { get; set; }

    public bool Activa { get; set; } = true;

    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

    [ForeignKey(nameof(ServidorId))]
    public Servidor? Servidor { get; set; }
}
