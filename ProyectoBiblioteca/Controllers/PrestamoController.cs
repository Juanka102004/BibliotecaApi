using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoBiblioteca.Models;
using ProyectoBiblioteca.Repositorio.DAO;
using System.Threading.Tasks;

namespace ProyectoBiblioteca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrestamoController : ControllerBase
    {
        [HttpGet("Listar")]
        public async Task<IActionResult> obtenerPrestamos()
        {
            var lista = await Task.Run(() => new PrestamoDAO().obtenerPrestamo());
            return Ok(lista);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> obtenerPrestamoPorId(int id)
        {
            var prestamo = await Task.Run(() => new PrestamoDAO().obtenerPrestamoPorId(id));
            return Ok(prestamo);
        }

        [HttpPost("Registrar")]
        public async Task<IActionResult> registrarPrestamo(Prestamo reg)
        {
            var mensaje = await Task.Run(() => new PrestamoDAO().registrarPrestamo(reg));
            return Ok(mensaje);
        }

        [HttpPut("Actualizar")]
        public async Task<IActionResult> actualizarPrestamo(Prestamo reg)
        {
            var mensaje = await Task.Run(() => new PrestamoDAO().actualizarPrestamo(reg));
            return Ok(mensaje);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> eliminarPrestamo(int id)
        {
            var mensaje = await Task.Run(() => new PrestamoDAO().eliminarPrestamo(id));
            return Ok(mensaje);
        }
    }
}
