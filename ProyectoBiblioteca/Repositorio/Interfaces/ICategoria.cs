using ProyectoBiblioteca.Models;
namespace ProyectoBiblioteca.Repositorio.Interfaces
{
    public interface ICategoria
    {
        IEnumerable<Categoria> obtenerCategoria();
        Categoria obtenerCategoriaPorId(int id);
        string registrarCategoria(Categoria reg);
        string actualizarCategoria(Categoria reg);

        String eliminarCategoria(int id);
    }
}
