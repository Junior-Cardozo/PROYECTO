using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonitorServidores.Data;
using MonitorServidores.Models;

namespace MonitorServidores.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IncidentesController : ControllerBase
{
    private readonly MonitorDbContext _context;

    public IncidentesController(MonitorDbContext context)
    {
        _context = context;
    }

    // GET: api/incidentes
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Incidente>>> ObtenerTodos()
    {
        var incidentes = await _context.Incidentes
            .OrderByDescending(i => i.FechaRegistro)
            .ToListAsync();

        return Ok(incidentes);
    }

    // GET: api/incidentes/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Incidente>> ObtenerPorId(int id)
    {
        var incidente = await _context.Incidentes.FindAsync(id);

        if (incidente == null)
        {
            return NotFound(new { mensaje = "No se encontró el incidente." });
        }

        return Ok(incidente);
    }

    // POST: api/incidentes
    [HttpPost]
    public async Task<ActionResult<Incidente>> Crear(Incidente incidente)
    {
        incidente.Id = 0;
        incidente.FechaRegistro = DateTime.UtcNow;

        _context.Incidentes.Add(incidente);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(ObtenerPorId),
            new { id = incidente.Id },
            incidente);
    }

    // PUT: api/incidentes/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Actualizar(int id, Incidente datos)
    {
        if (id != datos.Id)
        {
            return BadRequest(new { mensaje = "El ID de la URL no coincide con el ID del objeto." });
        }

        var incidente = await _context.Incidentes.FindAsync(id);

        if (incidente == null)
        {
            return NotFound(new { mensaje = "No se encontró el incidente." });
        }

        incidente.ServidorId = datos.ServidorId;
        incidente.Titulo = datos.Titulo;
        incidente.Descripcion = datos.Descripcion;
        incidente.Gravedad = datos.Gravedad;
        incidente.Estado = datos.Estado;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/incidentes/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        var incidente = await _context.Incidentes.FindAsync(id);

        if (incidente == null)
        {
            return NotFound(new { mensaje = "No se encontró el incidente." });
        }

        _context.Incidentes.Remove(incidente);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
