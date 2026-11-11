
using Inventario.Api.Data;
using Inventario.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventario.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservasStockController : ControllerBase
    {
        private readonly InventarioDBContext _context;

        public ReservasStockController(InventarioDBContext context)
        {
            _context = context;
        }

        // GET: api/ReservasStock
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservaStock>>> GetReservasStock()
        {
            return await _context.ReservasStock.ToListAsync();
        }

        // GET: api/ReservasStock/1
        [HttpGet("{id}")]
        public async Task<ActionResult<ReservaStock>> GetReservaStock(int id)
        {
            var reserva = await _context.ReservasStock.FindAsync(id);

            if (reserva == null)
            {
                return NotFound();
            }

            return reserva;
        }

        // POST: api/ReservasStock
        [HttpPost]
        public async Task<ActionResult<ReservaStock>> PostReservaStock(ReservaStock reserva)
        {
            if (reserva.IdProducto <= 0)
            {
                return BadRequest("El producto no es válido.");
            }

            var bodega = await _context.Bodegas.FindAsync(reserva.IdBodega);

            if (bodega == null)
            {
                return BadRequest("La bodega no existe.");
            }

            if (reserva.Cantidad <= 0)
            {
                return BadRequest("La cantidad debe ser mayor que cero.");
            }

            if (string.IsNullOrWhiteSpace(reserva.Estado))
            {
                return BadRequest("El estado es obligatorio.");
            }

            _context.ReservasStock.Add(reserva);
            await _context.SaveChangesAsync();

            return Ok(reserva);
        }

        // PUT: api/ReservasStock/1
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservaStock(int id, ReservaStock reserva)
        {
            if (id != reserva.IdReserva)
            {
                return BadRequest();
            }

            var reservaActual = await _context.ReservasStock.FindAsync(id);

            if (reservaActual == null)
            {
                return NotFound();
            }

            if (reserva.IdProducto <= 0)
            {
                return BadRequest("El producto no es válido.");
            }

            var bodega = await _context.Bodegas.FindAsync(reserva.IdBodega);

            if (bodega == null)
            {
                return BadRequest("La bodega no existe.");
            }

            if (reserva.Cantidad <= 0)
            {
                return BadRequest("La cantidad debe ser mayor que cero.");
            }

            if (string.IsNullOrWhiteSpace(reserva.Estado))
            {
                return BadRequest("El estado es obligatorio.");
            }

            reservaActual.IdProducto = reserva.IdProducto;
            reservaActual.IdBodega = reserva.IdBodega;
            reservaActual.Cantidad = reserva.Cantidad;
            reservaActual.Estado = reserva.Estado;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/ReservasStock/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservaStock(int id)
        {
            var reserva = await _context.ReservasStock.FindAsync(id);

            if (reserva == null)
            {
                return NotFound();
            }

            _context.ReservasStock.Remove(reserva);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
