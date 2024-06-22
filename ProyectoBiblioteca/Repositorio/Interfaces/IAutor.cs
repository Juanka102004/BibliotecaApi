using ProyectoBiblioteca.Models;

namespace ProyectoBiblioteca.Repositorio.Interfaces
{
    public interface IAutor
    {

        IEnumerable<Autor> obtenerAutor();
        Autor obtenerAutorPorId(int id);
        string registrarAutor(Autor reg);
        string actualizarAutor(Autor reg);

        String eliminarAutor(int id);
    }
}
