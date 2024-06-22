using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoBiblioteca.Models;
using ProyectoBiblioteca.Repositorio.DAO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProyectoBiblioteca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoPrestamoController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> obtenerEstadoPrestamo()
        {
            var lista = await Task.Run(() => new Estado_PrestamoDAO().obtenerEstado_Prestamo());
            return Ok(lista);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> obtenerEstadoPrestamoPorId(int id)
        {
            var estadoPrestamo = await Task.Run(() => new Estado_PrestamoDAO().obtenerEstado_PrestamoPorId(id));
            return Ok(estadoPrestamo);
        }

        [HttpPost]
        public async Task<IActionResult> registrarEstadoPrestamo(Estado_Prestamo reg)
        {
            var mensaje = await Task.Run(() => new Estado_PrestamoDAO().registrarEstado_Prestamo(reg));
            return Ok(mensaje);
        }

        [HttpPut]
        public async Task<IActionResult> actualizarEstadoPrestamo(Estado_Prestamo reg)
        {
            var mensaje = await Task.Run(() => new Estado_PrestamoDAO().actualizarEstado_Prestamo(reg));
            return Ok(mensaje);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> eliminarEstadoPrestamo(int id)
        {
            var mensaje = await Task.Run(() => new Estado_PrestamoDAO().eliminarEstado_Prestamo(id));
            return Ok(mensaje);
        }
    }
}
