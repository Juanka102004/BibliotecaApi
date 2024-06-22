using System;

namespace ProyectoBiblioteca.Models
{
    public class Autor
    {
        private string idAutor;
        private string descripcion;

        // Constructor sin parámetros requerido para el enlace de datos
        public Autor()
        {
        }

        // Constructor con parámetros
        public Autor(string idAutor, string descripcion)
        {
            this.idAutor = idAutor;
            this.descripcion = descripcion;
        }

        // Propiedad IdAutor
        public string IdAutor
        {
            get { return idAutor; }
            set { idAutor = value; }
        }

        // Propiedad Descripcion
        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }
    }
}

