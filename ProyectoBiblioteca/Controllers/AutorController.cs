using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoBiblioteca.Repositorio.DAO;
using ProyectoBiblioteca.Models;

namespace ProyectoBiblioteca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> obtenerAutores()
        {
            var lista = await Task.Run(() => new AutorDAO().obtenerAutor());
            return Ok(lista);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> obtenerAutor(int id)
        {
            var autor = await Task.Run(() => new AutorDAO().obtenerAutorPorId(id));
            return Ok(autor);
        }

        [HttpPost]
        public async Task<IActionResult> registrarAutor(Autor reg)
        {
            var mensaje = await Task.Run(() => new AutorDAO().registrarAutor(reg));
            return Ok(mensaje);
        }

        [HttpPut]
        public async Task<IActionResult> actualizarAutor(Autor reg)
        {
            var mensaje = await Task.Run(() => new AutorDAO().actualizarAutor(reg));
            return Ok(mensaje);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> eliminarAutor(int id)
        {
            var mensaje = await Task.Run(() => new AutorDAO().eliminarAutor(id));
            return Ok(mensaje);
        }
    }
}
