using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoBiblioteca.Repositorio.DAO;
using ProyectoBiblioteca.Models;
using System.Threading.Tasks;

namespace ProyectoBiblioteca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EditorialController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> obtenerEditoriales()
        {
            var lista = await Task.Run(() => new EditorialDAO().obtenerEditorial());
            return Ok(lista);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> obtenerEditorial(int id)
        {
            var editorial = await Task.Run(() => new EditorialDAO().obtenerEditorialPorId(id));
            return Ok(editorial);
        }

        [HttpPost]
        public async Task<IActionResult> registrarEditorial(Editorial reg)
        {
            var mensaje = await Task.Run(() => new EditorialDAO().registrarEditorial(reg));
            return Ok(mensaje);
        }

        [HttpPut]
        public async Task<IActionResult> actualizarEditorial(Editorial reg)
        {
            var mensaje = await Task.Run(() => new EditorialDAO().actualizarEditorial(reg));
            return Ok(mensaje);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> eliminarEditorial(int id)
        {
            var mensaje = await Task.Run(() => new EditorialDAO().eliminarEditorial(id));
            return Ok(mensaje);
        }
    }
}
