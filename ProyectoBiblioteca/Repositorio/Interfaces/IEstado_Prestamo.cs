using ProyectoBiblioteca.Models;

namespace ProyectoBiblioteca.Repositorio.Interfaces
{
    public interface IEstado_Prestamo
    {

        IEnumerable<Estado_Prestamo> obtenerEstado_Prestamo();
        Estado_Prestamo obtenerEstado_PrestamoPorId(int id);
        string registrarEstado_Prestamo(Estado_Prestamo reg);
        string actualizarEstado_Prestamo(Estado_Prestamo reg);

        String eliminarEstado_Prestamo(int id);
    }
}
