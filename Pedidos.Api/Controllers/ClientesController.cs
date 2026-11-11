
using Pedidos.Api.Data;
using Pedidos.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Pedidos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly PedidosDBContext _context;

        public ClientesController(PedidosDBContext context)
        {
            _context = context;
        }

        // GET: api/Clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
            return await _context.Clientes.ToListAsync();
        }

        // GET: api/Clientes/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return cliente;
        }

        // POST: api/Clientes
        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente)
        {
            if (string.IsNullOrWhiteSpace(cliente.Nombres))
            {
                return BadRequest("El nombre es obligatorio.");
            }

            if (cliente.Cedula.Length != 10 ||
                !cliente.Cedula.All(char.IsDigit))
            {
                return BadRequest("La cédula debe tener 10 dígitos.");
            }

            if (!cliente.Correo.Contains("@") ||
                !cliente.Correo.Contains("."))
            {
                return BadRequest("El correo no es válido.");
            }

            // Validar que no exista la misma cédula
            bool existeCliente = await _context.Clientes.AnyAsync(c =>
                c.Cedula == cliente.Cedula);

            if (existeCliente)
            {
                return BadRequest("Ya existe un cliente con esta cédula.");
            }

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            return Ok(cliente);
        }

        // PUT: api/Clientes/1
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, Cliente cliente)
        {
            if (id != cliente.IdCliente)
            {
                return BadRequest();
            }

            var clienteActual = await _context.Clientes.FindAsync(id);

            if (clienteActual == null)
            {
                return NotFound();
            }

            if (string.IsNullOrWhiteSpace(cliente.Nombres))
            {
                return BadRequest("El nombre es obligatorio.");
            }

            if (cliente.Cedula.Length != 10 ||
                !cliente.Cedula.All(char.IsDigit))
            {
                return BadRequest("La cédula debe tener 10 dígitos.");
            }

            if (!cliente.Correo.Contains("@") ||
                !cliente.Correo.Contains("."))
            {
                return BadRequest("El correo no es válido.");
            }

            // Validar que otro cliente no tenga la misma cédula
            bool existeCliente = await _context.Clientes.AnyAsync(c =>
                c.Cedula == cliente.Cedula &&
                c.IdCliente != id);

            if (existeCliente)
            {
                return BadRequest("Ya existe un cliente con esta cédula.");
            }

            clienteActual.Nombres = cliente.Nombres;
            clienteActual.Cedula = cliente.Cedula;
            clienteActual.Correo = cliente.Correo;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Clientes/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            // Validar que el cliente no tenga pedidos
            bool tienePedidos = await _context.Pedidos.AnyAsync(p =>
                p.IdCliente == id);

            if (tienePedidos)
            {
                return BadRequest("No se puede eliminar el cliente porque tiene pedidos registrados.");
            }

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
