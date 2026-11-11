
using Inventario.Api.Data;
using Inventario.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventario.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BodegasController : ControllerBase
    {
        private readonly InventarioDBContext _context;

        public BodegasController(InventarioDBContext context)
        {
            _context = context;
        }

        // GET: api/Bodegas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bodega>>> GetBodegas()
        {
            return await _context.Bodegas.ToListAsync();
        }

        // GET: api/Bodegas/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Bodega>> GetBodega(int id)
        {
            var bodega = await _context.Bodegas.FindAsync(id);

            if (bodega == null)
            {
                return NotFound();
            }

            return bodega;
        }

        // POST: api/Bodegas
        [HttpPost]
        public async Task<ActionResult<Bodega>> PostBodega(Bodega bodega)
        {
            _context.Bodegas.Add(bodega);
            await _context.SaveChangesAsync();

            return Ok(bodega);
        }

        // PUT: api/Bodegas/1
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBodega(int id, Bodega bodega)
        {
            if (id != bodega.IdBodega)
            {
                return BadRequest();
            }

            _context.Entry(bodega).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Bodegas.AnyAsync(e => e.IdBodega == id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // DELETE: api/Bodegas/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBodega(int id)
        {
            var bodega = await _context.Bodegas.FindAsync(id);

            if (bodega == null)
            {
                return NotFound();
            }

            // Comprobar si la bodega tiene registros relacionados
            bool tieneExistencias = await _context.Existencias
                .AnyAsync(e => e.IdBodega == id);

            bool tieneMovimientos = await _context.MovimientosInventario
                .AnyAsync(m => m.IdBodega == id);

            bool tieneReservas = await _context.ReservasStock
                .AnyAsync(r => r.IdBodega == id);

            if (tieneExistencias || tieneMovimientos || tieneReservas)
            {
                return BadRequest("No se puede eliminar la bodega porque tiene registros relacionados.");
            }

            _context.Bodegas.Remove(bodega);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
