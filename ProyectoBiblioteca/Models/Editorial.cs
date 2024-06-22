using System;

namespace ProyectoBiblioteca.Models
{
    public class Editorial
    {
        private string idEditorial;
        private string descripcion;

        public Editorial()
        {
        }

        public Editorial(string idEditorial, string descripcion)
        {
            this.IdEditorial = idEditorial;
            this.Descripcion = descripcion;
        }

        public string IdEditorial { get => idEditorial; set => idEditorial = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
    }
}
