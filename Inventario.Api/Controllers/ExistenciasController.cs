
using Inventario.Api.Data;
using Inventario.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventario.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExistenciasController : ControllerBase
    {
        private readonly InventarioDBContext _context;

        public ExistenciasController(InventarioDBContext context)
        {
            _context = context;
        }

        // GET: api/Existencias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Existencia>>> GetExistencias()
        {
            return await _context.Existencias.ToListAsync();
        }

        // GET: api/Existencias/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Existencia>> GetExistencia(int id)
        {
            var existencia = await _context.Existencias.FindAsync(id);

            if (existencia == null)
            {
                return NotFound();
            }

            return existencia;
        }

        // POST: api/Existencias
        [HttpPost]
        public async Task<ActionResult<Existencia>> PostExistencia(Existencia existencia)
        {
            if (existencia.Stock < 0)
            {
                return BadRequest("El stock no puede ser negativo.");
            }

            if (existencia.IdProducto <= 0)
            {
                return BadRequest("El producto no es válido.");
            }

            var bodega = await _context.Bodegas.FindAsync(existencia.IdBodega);

            if (bodega == null)
            {
                return BadRequest("La bodega no existe.");
            }

            // Validar que no exista el mismo producto en la misma bodega
            bool existeRegistro = await _context.Existencias.AnyAsync(e =>
                e.IdProducto == existencia.IdProducto &&
                e.IdBodega == existencia.IdBodega);

            if (existeRegistro)
            {
                return BadRequest("Este producto ya está registrado en esta bodega.");
            }

            _context.Existencias.Add(existencia);
            await _context.SaveChangesAsync();

            return Ok(existencia);
        }

        // PUT: api/Existencias/1
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExistencia(int id, Existencia existencia)
        {
            if (id != existencia.IdExistencia)
            {
                return BadRequest();
            }

            var existenciaActual = await _context.Existencias.FindAsync(id);

            if (existenciaActual == null)
            {
                return NotFound();
            }

            if (existencia.Stock < 0)
            {
                return BadRequest("El stock no puede ser negativo.");
            }

            if (existencia.IdProducto <= 0)
            {
                return BadRequest("El producto no es válido.");
            }

            var bodega = await _context.Bodegas.FindAsync(existencia.IdBodega);

            if (bodega == null)
            {
                return BadRequest("La bodega no existe.");
            }

            // Validar que no exista otro registro con el mismo producto y bodega
            bool existeRegistro = await _context.Existencias.AnyAsync(e =>
                e.IdProducto == existencia.IdProducto &&
                e.IdBodega == existencia.IdBodega &&
                e.IdExistencia != id);

            if (existeRegistro)
            {
                return BadRequest("Este producto ya está registrado en esta bodega.");
            }

            existenciaActual.IdProducto = existencia.IdProducto;
            existenciaActual.IdBodega = existencia.IdBodega;
            existenciaActual.Stock = existencia.Stock;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Existencias/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExistencia(int id)
        {
            var existencia = await _context.Existencias.FindAsync(id);

            if (existencia == null)
            {
                return NotFound();
            }

            _context.Existencias.Remove(existencia);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
