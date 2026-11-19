using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonitorServidores.Data;
using MonitorServidores.Models;

namespace MonitorServidores.Controllers;

[ApiController]
[Route("api/reglas-alerta")]
public class ReglasAlertaController : ControllerBase
{
    private readonly MonitorDbContext _context;

    public ReglasAlertaController(MonitorDbContext context)
    {
        _context = context;
    }

    // GET: api/reglas-alerta
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReglaAlerta>>> ObtenerTodas()
    {
        var reglas = await _context.ReglasAlerta
            .AsNoTracking()
            .OrderBy(r => r.Id)
            .ToListAsync();

        return Ok(reglas);
    }

    // GET: api/reglas-alerta/1
    [HttpGet("{id}")]
    public async Task<ActionResult<ReglaAlerta>> ObtenerPorId(int id)
    {
        var regla = await _context.ReglasAlerta
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == id);

        if (regla == null)
        {
            return NotFound("Regla de alerta no encontrada");
        }

        return Ok(regla);
    }

    // POST: api/reglas-alerta
    [HttpPost]
    public async Task<ActionResult<ReglaAlerta>> Crear(ReglaAlerta datos)
    {
        var servidorExiste = await _context.Servidores
            .AnyAsync(s => s.Id == datos.ServidorId);

        if (!servidorExiste)
        {
            return BadRequest("El servidor indicado no existe");
        }

        if (datos.Umbral < 0 || datos.Umbral > 100)
        {
            return BadRequest("El umbral debe estar entre 0 y 100");
        }

        var regla = new ReglaAlerta
        {
            ServidorId = datos.ServidorId,
            Nombre = datos.Nombre,
            Metrica = datos.Metrica,
            Umbral = datos.Umbral,
            Activa = datos.Activa,
            FechaCreacion = DateTime.UtcNow
        };

        _context.ReglasAlerta.Add(regla);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(ObtenerPorId),
            new { id = regla.Id },
            regla
        );
    }

    // PUT: api/reglas-alerta/1
    [HttpPut("{id}")]
    public async Task<IActionResult> Actualizar(
        int id,
        ReglaAlerta datos)
    {
        var regla = await _context.ReglasAlerta.FindAsync(id);

        if (regla == null)
        {
            return NotFound("Regla de alerta no encontrada");
        }

        var servidorExiste = await _context.Servidores
            .AnyAsync(s => s.Id == datos.ServidorId);

        if (!servidorExiste)
        {
            return BadRequest("El servidor indicado no existe");
        }

        if (datos.Umbral < 0 || datos.Umbral > 100)
        {
            return BadRequest("El umbral debe estar entre 0 y 100");
        }

        regla.ServidorId = datos.ServidorId;
        regla.Nombre = datos.Nombre;
        regla.Metrica = datos.Metrica;
        regla.Umbral = datos.Umbral;
        regla.Activa = datos.Activa;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/reglas-alerta/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        var regla = await _context.ReglasAlerta.FindAsync(id);

        if (regla == null)
        {
            return NotFound("Regla de alerta no encontrada");
        }

        _context.ReglasAlerta.Remove(regla);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}
