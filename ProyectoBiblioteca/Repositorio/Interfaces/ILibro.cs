using ProyectoBiblioteca.Models;
namespace ProyectoBiblioteca.Repositorio.Interfaces
{
    public interface ILibro
    {
        IEnumerable<Libro> obtenerLibros();
        Libro obtenerLibroPorId(int id);
        string registrarLibro(Libro reg);
        string actualizarLibro(Libro reg);

        String eliminarLibro(int id);
    }
}
