using System;

namespace ProyectoBiblioteca.Models
{
    public class Estado_Prestamo
    {
        private int idEstadoPrestamo;
        private string descripcion;
        private bool estado;
        private DateTime fechaCreacion;

        public Estado_Prestamo()
        {
        }

        public Estado_Prestamo(int idEstadoPrestamo, string descripcion, bool estado, DateTime fechaCreacion)
        {
            this.IdEstadoPrestamo = idEstadoPrestamo;
            this.Descripcion = descripcion;
            this.Estado = estado;
            this.FechaCreacion = fechaCreacion;
        }

        public int IdEstadoPrestamo { get => idEstadoPrestamo; set => idEstadoPrestamo = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public bool Estado { get => estado; set => estado = value; }
        public DateTime FechaCreacion { get => fechaCreacion; set => fechaCreacion = value; }
    }
}

