using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoBiblioteca.Repositorio.DAO;
using ProyectoBiblioteca.Models;

namespace ProyectoBiblioteca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroController : ControllerBase
    {
        [HttpGet("Listar")]
        public async Task<IActionResult> obtenerLibros()
        {

            var lista = await Task.Run(() => new LibroDAO().obtenerLibros());
            return Ok(lista);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> obtenerLibro(int id)
        {

            var libro = await Task.Run(() => new LibroDAO().obtenerLibroPorId(id));
            return Ok(libro);
        }

        [HttpPost("Registrar")]
        public async Task<IActionResult> registrarLibro(Libro reg)
        {
            var mensaje = await Task.Run(() => new LibroDAO().registrarLibro(reg));
            return Ok(mensaje);
        }

        [HttpPut("Actualizar")]
        public async Task<IActionResult> actualizarLibro(Libro reg)
        {
            var mensaje = await Task.Run(() => new LibroDAO().actualizarLibro(reg));
            return Ok(mensaje);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> eliminarLibro(int id)
        {
            var mensaje = await Task.Run(() => new LibroDAO().eliminarLibro(id));
            return Ok(mensaje);
        }

    }
}


