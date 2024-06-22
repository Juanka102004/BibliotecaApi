using System;

namespace ProyectoBiblioteca.Models
{
    public class Categoria
    {
        private string idCategoria;
        private string descripcion;
        

        public Categoria()
        {
            idCategoria = "";
            descripcion = "";
           
        }

        public Categoria(string idCategoria, string descripcion, DateTime fechaCreacion)
        {
            this.idCategoria = idCategoria;
            this.descripcion = descripcion;
            
        }

        public string IdCategoria
        {
            get { return idCategoria; }
            set { idCategoria = value; }
        }

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        
    }
}

