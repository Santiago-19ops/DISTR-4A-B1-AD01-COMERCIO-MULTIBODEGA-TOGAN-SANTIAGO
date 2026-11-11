
using Pedidos.Api.Data;
using Pedidos.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Pedidos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetallesPedidoController : ControllerBase
    {
        private readonly PedidosDBContext _context;

        public DetallesPedidoController(PedidosDBContext context)
        {
            _context = context;
        }

        // GET: api/DetallesPedido
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetallePedido>>> GetDetallesPedido()
        {
            return await _context.DetallesPedido.ToListAsync();
        }

        // GET: api/DetallesPedido/1
        [HttpGet("{id}")]
        public async Task<ActionResult<DetallePedido>> GetDetallePedido(int id)
        {
            var detalle = await _context.DetallesPedido.FindAsync(id);

            if (detalle == null)
            {
                return NotFound();
            }

            return detalle;
        }

        // POST: api/DetallesPedido
        [HttpPost]
        public async Task<ActionResult<DetallePedido>> PostDetallePedido(DetallePedido detalle)
        {
            var pedido = await _context.Pedidos.FindAsync(detalle.IdPedido);

            if (pedido == null)
            {
                return BadRequest("El pedido no existe.");
            }

            if (detalle.IdProducto <= 0)
            {
                return BadRequest("El producto no es válido.");
            }

            if (detalle.Cantidad <= 0)
            {
                return BadRequest("La cantidad debe ser mayor que cero.");
            }

            if (detalle.PrecioUnitario < 0)
            {
                return BadRequest("El precio unitario no puede ser negativo.");
            }

            // Validar que no exista el mismo producto en el pedido
            bool existeDetalle = await _context.DetallesPedido.AnyAsync(d =>
                d.IdPedido == detalle.IdPedido &&
                d.IdProducto == detalle.IdProducto);

            if (existeDetalle)
            {
                return BadRequest("Este producto ya está registrado en el pedido.");
            }

            _context.DetallesPedido.Add(detalle);
            await _context.SaveChangesAsync();

            return Ok(detalle);
        }

        // PUT: api/DetallesPedido/1
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetallePedido(int id, DetallePedido detalle)
        {
            if (id != detalle.IdDetalle)
            {
                return BadRequest();
            }

            var detalleActual = await _context.DetallesPedido.FindAsync(id);

            if (detalleActual == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos.FindAsync(detalle.IdPedido);

            if (pedido == null)
            {
                return BadRequest("El pedido no existe.");
            }

            if (detalle.IdProducto <= 0)
            {
                return BadRequest("El producto no es válido.");
            }

            if (detalle.Cantidad <= 0)
            {
                return BadRequest("La cantidad debe ser mayor que cero.");
            }

            if (detalle.PrecioUnitario < 0)
            {
                return BadRequest("El precio unitario no puede ser negativo.");
            }

            // Validar que no exista otro detalle con el mismo producto
            bool existeDetalle = await _context.DetallesPedido.AnyAsync(d =>
                d.IdPedido == detalle.IdPedido &&
                d.IdProducto == detalle.IdProducto &&
                d.IdDetalle != id);

            if (existeDetalle)
            {
                return BadRequest("Este producto ya está registrado en el pedido.");
            }

            detalleActual.IdPedido = detalle.IdPedido;
            detalleActual.IdProducto = detalle.IdProducto;
            detalleActual.Cantidad = detalle.Cantidad;
            detalleActual.PrecioUnitario = detalle.PrecioUnitario;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/DetallesPedido/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetallePedido(int id)
        {
            var detalle = await _context.DetallesPedido.FindAsync(id);

            if (detalle == null)
            {
                return NotFound();
            }

            _context.DetallesPedido.Remove(detalle);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
