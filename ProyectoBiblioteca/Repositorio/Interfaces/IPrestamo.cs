using ProyectoBiblioteca.Models;

namespace ProyectoBiblioteca.Repositorio.Interfaces
{
    public interface IPrestamo
    {

        IEnumerable<Prestamo> obtenerPrestamo();
        Prestamo obtenerPrestamoPorId(int id);
        string registrarPrestamo(Prestamo reg);
        string actualizarPrestamo(Prestamo reg);

        String eliminarPrestamo(int id);
    }
}
