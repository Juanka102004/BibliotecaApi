using ProyectoBiblioteca.Models;
namespace ProyectoBiblioteca.Repositorio.Interfaces
{
    public interface ICliente
    {
        IEnumerable<Cliente> obtenerClientes();
        Cliente obtenerClientePorId(int id);
        string registrarCliente(Cliente reg);
        string actualizarCliente(Cliente reg);

        String eliminarCliente(int id);
    }
}
