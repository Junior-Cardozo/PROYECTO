using System.ComponentModel.DataAnnotations;

namespace MonitorServidores.Models;

public class ServidorRequest
{
    [Required]
    [MaxLength(100)]
    public string Nombre { get; set; } = string.Empty;

    [Required]
    [MaxLength(255)]
    public string DireccionIp { get; set; } = string.Empty;

    [MaxLength(300)]
    public string? Descripcion { get; set; }

    public bool Activo { get; set; } = true;
}