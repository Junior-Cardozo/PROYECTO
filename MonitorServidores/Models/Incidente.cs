using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonitorServidores.Models;

[Table("incidentes")]
public class Incidente
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [Column("servidor_id")]
    public int ServidorId { get; set; }

    [Required]
    [StringLength(100)]
    [Column("titulo")]
    public string Titulo { get; set; } = string.Empty;

    [StringLength(500)]
    [Column("descripcion")]
    public string? Descripcion { get; set; }

    [Required]
    [StringLength(20)]
    [Column("gravedad")]
    public string Gravedad { get; set; } = "media";

    [Required]
    [StringLength(20)]
    [Column("estado")]
    public string Estado { get; set; } = "abierto";

    [Column("fecha_registro")]
    public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
}
