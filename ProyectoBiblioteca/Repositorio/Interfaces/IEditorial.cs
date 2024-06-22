using ProyectoBiblioteca.Models;

namespace ProyectoBiblioteca.Repositorio.Interfaces
{
    public interface IEditorial
    {

        IEnumerable<Editorial> obtenerEditorial();
        Editorial obtenerEditorialPorId(int id);
        string registrarEditorial(Editorial reg);
        string actualizarEditorial(Editorial reg);

        String eliminarEditorial(int id);
    }
}
