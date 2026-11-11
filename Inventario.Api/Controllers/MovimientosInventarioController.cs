
using Inventario.Api.Data;
using Inventario.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventario.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientosInventarioController : ControllerBase
    {
        private readonly InventarioDBContext _context;

        public MovimientosInventarioController(InventarioDBContext context)
        {
            _context = context;
        }

        // GET: api/MovimientosInventario
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovimientoInventario>>> GetMovimientosInventario()
        {
            return await _context.MovimientosInventario.ToListAsync();
        }

        // GET: api/MovimientosInventario/1
        [HttpGet("{id}")]
        public async Task<ActionResult<MovimientoInventario>> GetMovimientoInventario(int id)
        {
            var movimiento = await _context.MovimientosInventario.FindAsync(id);

            if (movimiento == null)
            {
                return NotFound();
            }

            return movimiento;
        }

        // POST: api/MovimientosInventario
        [HttpPost]
        public async Task<ActionResult<MovimientoInventario>> PostMovimientoInventario(MovimientoInventario movimiento)
        {
            if (movimiento.IdProducto <= 0)
            {
                return BadRequest("El producto no es válido.");
            }

            var bodega = await _context.Bodegas.FindAsync(movimiento.IdBodega);

            if (bodega == null)
            {
                return BadRequest("La bodega no existe.");
            }

            if (movimiento.Cantidad <= 0)
            {
                return BadRequest("La cantidad debe ser mayor que cero.");
            }

            if (movimiento.Tipo != "Entrada" && movimiento.Tipo != "Salida")
            {
                return BadRequest("El tipo debe ser Entrada o Salida.");
            }

            _context.MovimientosInventario.Add(movimiento);
            await _context.SaveChangesAsync();

            return Ok(movimiento);
        }

        // PUT: api/MovimientosInventario/1
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovimientoInventario(int id, MovimientoInventario movimiento)
        {
            if (id != movimiento.IdMovimiento)
            {
                return BadRequest();
            }

            var movimientoActual = await _context.MovimientosInventario.FindAsync(id);

            if (movimientoActual == null)
            {
                return NotFound();
            }

            if (movimiento.IdProducto <= 0)
            {
                return BadRequest("El producto no es válido.");
            }

            var bodega = await _context.Bodegas.FindAsync(movimiento.IdBodega);

            if (bodega == null)
            {
                return BadRequest("La bodega no existe.");
            }

            if (movimiento.Cantidad <= 0)
            {
                return BadRequest("La cantidad debe ser mayor que cero.");
            }

            if (movimiento.Tipo != "Entrada" && movimiento.Tipo != "Salida")
            {
                return BadRequest("El tipo debe ser Entrada o Salida.");
            }

            movimientoActual.IdProducto = movimiento.IdProducto;
            movimientoActual.IdBodega = movimiento.IdBodega;
            movimientoActual.Tipo = movimiento.Tipo;
            movimientoActual.Cantidad = movimiento.Cantidad;
            movimientoActual.Fecha = movimiento.Fecha;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/MovimientosInventario/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovimientoInventario(int id)
        {
            var movimiento = await _context.MovimientosInventario.FindAsync(id);

            if (movimiento == null)
            {
                return NotFound();
            }

            _context.MovimientosInventario.Remove(movimiento);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
