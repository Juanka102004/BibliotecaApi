using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoBiblioteca.Repositorio.DAO;
using ProyectoBiblioteca.Models;

namespace ProyectoBiblioteca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        [HttpGet("Listar")]
        public async Task<IActionResult> obtenerClientes()
        {

            var lista = await Task.Run(() => new ClienteDAO().obtenerClientes());
            return Ok(lista);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> obtenerCliente(int id)
        {

            var cliente = await Task.Run(() => new ClienteDAO().obtenerClientePorId(id));
            return Ok(cliente);
        }

        [HttpPost("Registrar")]
        public async Task<IActionResult> registrarCliente(Cliente reg)
        {
            var mensaje = await Task.Run(() => new ClienteDAO().registrarCliente(reg));
            return Ok(mensaje);
        }

        [HttpPut("Actualizar")]
        public async Task<IActionResult> actualizarCliente(Cliente reg)
        {
            var mensaje = await Task.Run(() => new ClienteDAO().actualizarCliente(reg));
            return Ok(mensaje);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> eliminarCliente(int id)
        {
            var mensaje = await Task.Run(() => new ClienteDAO().eliminarCliente(id));
            return Ok(mensaje);
        }

    }
}


