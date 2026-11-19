using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonitorServidores.Data;
using MonitorServidores.Models;

namespace MonitorServidores.Controllers;

[ApiController]
[Route("api/servidores")]
public class ServidoresController : ControllerBase
{
    private readonly MonitorDbContext _context;

    public ServidoresController(MonitorDbContext context)
    {
        _context = context;
    }

    // GET: Obtener todos los servidores
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Servidor>>> ObtenerTodos()
    {
        var servidores = await _context.Servidores
            .AsNoTracking()
            .OrderBy(s => s.Id)
            .ToListAsync();

        return Ok(servidores);
    }

    // GET: Obtener servidor por ID
    [HttpGet("{id}")]
    public async Task<ActionResult<Servidor>> ObtenerPorId(int id)
    {
        var servidor = await _context.Servidores
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == id);

        if (servidor == null)
        {
            return NotFound("Servidor no encontrado");
        }

        return Ok(servidor);
    }

    // POST: Registrar un servidor
    [HttpPost]
    public async Task<ActionResult<Servidor>> Crear(
        ServidorRequest datos)
    {
        var servidor = new Servidor
        {
            Nombre = datos.Nombre,
            DireccionIp = datos.DireccionIp,
            Descripcion = datos.Descripcion,
            Activo = datos.Activo,
            FechaRegistro = DateTime.UtcNow
        };

        _context.Servidores.Add(servidor);

        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(ObtenerPorId),
            new { id = servidor.Id },
            servidor
        );
    }

    // PUT: Actualizar servidor
    [HttpPut("{id}")]
    public async Task<IActionResult> Actualizar(
        int id,
        ServidorRequest datos)
    {
        var servidor = await _context.Servidores.FindAsync(id);

        if (servidor == null)
        {
            return NotFound("Servidor no encontrado");
        }

        servidor.Nombre = datos.Nombre;
        servidor.DireccionIp = datos.DireccionIp;
        servidor.Descripcion = datos.Descripcion;
        servidor.Activo = datos.Activo;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: Eliminar servidor
    [HttpDelete("{id}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        var servidor = await _context.Servidores.FindAsync(id);

        if (servidor == null)
        {
            return NotFound("Servidor no encontrado");
        }

        _context.Servidores.Remove(servidor);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}