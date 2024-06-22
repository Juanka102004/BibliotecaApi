using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoBiblioteca.Repositorio.DAO;
using ProyectoBiblioteca.Models;

namespace ProyectoBiblioteca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        [HttpGet]
        public IActionResult obtenerCategorias()
        {
            var lista = new CategoriaDAO().obtenerCategoria();
            return Ok(lista);
        }

        [HttpGet("{id}")]
        public IActionResult obtenerCategoria(int id)
        {
            var categoria = new CategoriaDAO().obtenerCategoriaPorId(id);
            if (categoria == null)
            {
                return NotFound();
            }
            return Ok(categoria);
        }

        [HttpPost]
        public IActionResult registrarCategoria(Categoria reg)
        {
            var mensaje = new CategoriaDAO().registrarCategoria(reg);
            return Ok(mensaje);
        }

        [HttpPut]
        public IActionResult actualizarCategoria(Categoria reg)
        {
            var mensaje = new CategoriaDAO().actualizarCategoria(reg);
            return Ok(mensaje);
        }

        [HttpDelete("{id}")]
        public IActionResult eliminarCategoria(int id)
        {
            var mensaje = new CategoriaDAO().eliminarCategoria(id);
            return Ok(mensaje);
        }
    }
}


