using System;

namespace ProyectoBiblioteca.Models
{
    public class Libro
    {
        private int idLibro;
        public string titulo;
        private string idAutor;
        private string idEditorial;
        private string idCategoria;
        private int ejemplares;
        private DateTime fechaRegistro;

        public Libro()
        {
        }

        public Libro(int idLibro, string titulo, string idAutor, string idEditorial, string idCategoria, int ejemplares, DateTime fechaRegistro)
        {
            this.IdLibro = idLibro;
            this.Titulo = titulo;
            this.IdAutor = idAutor;
            this.IdEditorial = idEditorial;
            this.IdCategoria = idCategoria;
            this.Ejemplares = ejemplares;
            this.FechaRegistro = fechaRegistro;
        }

        public int IdLibro { get => idLibro; set => idLibro = value; }
        public string Titulo { get => titulo; set => titulo = value; }
        public string IdAutor { get => idAutor; set => idAutor = value; }
        public string IdEditorial { get => idEditorial; set => idEditorial = value; }
        public string IdCategoria { get => idCategoria; set => idCategoria = value; }
        public int Ejemplares { get => ejemplares; set => ejemplares = value; }
        public DateTime FechaRegistro { get => fechaRegistro; set => fechaRegistro = value; }
    }
}

