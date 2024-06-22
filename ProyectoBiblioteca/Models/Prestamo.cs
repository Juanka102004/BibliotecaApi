namespace ProyectoBiblioteca.Models
{
    public class Prestamo
    {
        private int idPrestamo;
        private int idCliente;
        private int idLibro;
        private DateTime? fechaDevolucion; // Cambiado a DateTime?
        private DateTime? fechaConfirmacionDevolucion; // Cambiado a DateTime?
        private string estadoEntregado;
        private string estadoRecibido;
        private bool estado;
        private DateTime fechaCreacion;

        public string NombreCliente { get; set; }
        public string TituloLibro { get; set; }

        public Prestamo()
        {
        }

        public Prestamo(int idPrestamo, int idCliente, int idLibro, DateTime? fechaDevolucion, DateTime? fechaConfirmacionDevolucion, string estadoEntregado, string estadoRecibido, bool estado, DateTime fechaCreacion)
        {
            this.IdPrestamo = idPrestamo;
            this.IdCliente = idCliente;
            this.IdLibro = idLibro;
            this.FechaDevolucion = fechaDevolucion;
            this.FechaConfirmacionDevolucion = fechaConfirmacionDevolucion;
            this.EstadoEntregado = estadoEntregado;
            this.EstadoRecibido = estadoRecibido;
            this.Estado = estado;
            this.FechaCreacion = fechaCreacion;
        }

        public int IdPrestamo { get => idPrestamo; set => idPrestamo = value; }
        public int IdCliente { get => idCliente; set => idCliente = value; }
        public int IdLibro { get => idLibro; set => idLibro = value; }
        public DateTime? FechaDevolucion { get => fechaDevolucion; set => fechaDevolucion = value; } // Cambiado a DateTime?
        public DateTime? FechaConfirmacionDevolucion { get => fechaConfirmacionDevolucion; set => fechaConfirmacionDevolucion = value; } // Cambiado a DateTime?
        public string EstadoEntregado { get => estadoEntregado; set => estadoEntregado = value; }
        public string EstadoRecibido { get => estadoRecibido; set => estadoRecibido = value; }
        public bool Estado { get => estado; set => estado = value; }
        public DateTime FechaCreacion { get => fechaCreacion; set => fechaCreacion = value; }
    }
}