using System.Data;

namespace ProyectoBiblioteca.Models
{
    public class Cliente
    {

        private int idCliente;
        public string nombre;
        private string apellido;
        private string telefono;
        private string direccion;
        private DateTime fechaCreacion;

        public Cliente()
        {
        }

        public Cliente(int idCliente, string nombre, string apellido, string telefono, string direccion, DateTime fechaCreacion)
        {
            this.IdCliente = idCliente;
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.Telefono = telefono;
            this.Direccion = direccion;
            this.FechaCreacion = fechaCreacion;
        }

        public int IdCliente { get => idCliente; set => idCliente = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Apellido { get => apellido; set => apellido = value; }
        public string Telefono { get => telefono; set => telefono = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public DateTime FechaCreacion { get => fechaCreacion; set => fechaCreacion = value; }
    }
}
