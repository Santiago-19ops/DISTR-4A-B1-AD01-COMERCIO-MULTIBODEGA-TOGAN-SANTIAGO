
using Pedidos.Api.Data;
using Pedidos.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Pedidos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly PedidosDBContext _context;

        public PedidosController(PedidosDBContext context)
        {
            _context = context;
        }

        // GET: api/Pedidos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetPedidos()
        {
            return await _context.Pedidos.ToListAsync();
        }

        // GET: api/Pedidos/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Pedido>> GetPedido(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);

            if (pedido == null)
            {
                return NotFound();
            }

            return pedido;
        }

        // POST: api/Pedidos
        [HttpPost]
        public async Task<ActionResult<Pedido>> PostPedido(Pedido pedido)
        {
            var cliente = await _context.Clientes.FindAsync(pedido.IdCliente);

            if (cliente == null)
            {
                return BadRequest("El cliente no existe.");
            }

            if (pedido.Fecha == default(DateTime))
            {
                return BadRequest("La fecha no es válida.");
            }

            if (pedido.Total < 0)
            {
                return BadRequest("El total no puede ser negativo.");
            }

            if (string.IsNullOrWhiteSpace(pedido.Estado))
            {
                return BadRequest("El estado es obligatorio.");
            }

            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();

            return Ok(pedido);
        }

        // PUT: api/Pedidos/1
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPedido(int id, Pedido pedido)
        {
            if (id != pedido.IdPedido)
            {
                return BadRequest();
            }

            var pedidoActual = await _context.Pedidos.FindAsync(id);

            if (pedidoActual == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes.FindAsync(pedido.IdCliente);

            if (cliente == null)
            {
                return BadRequest("El cliente no existe.");
            }

            if (pedido.Fecha == default(DateTime))
            {
                return BadRequest("La fecha no es válida.");
            }

            if (pedido.Total < 0)
            {
                return BadRequest("El total no puede ser negativo.");
            }

            if (string.IsNullOrWhiteSpace(pedido.Estado))
            {
                return BadRequest("El estado es obligatorio.");
            }

            pedidoActual.IdCliente = pedido.IdCliente;
            pedidoActual.Fecha = pedido.Fecha;
            pedidoActual.Total = pedido.Total;
            pedidoActual.Estado = pedido.Estado;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Pedidos/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePedido(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);

            if (pedido == null)
            {
                return NotFound();
            }

            bool tieneDetalles = await _context.DetallesPedido.AnyAsync(d =>
                d.IdPedido == id);

            if (tieneDetalles)
            {
                return BadRequest("No se puede eliminar el pedido porque tiene detalles registrados.");
            }

            _context.Pedidos.Remove(pedido);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
