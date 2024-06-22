using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using ProyectoBiblioteca.Models;
using ProyectoBiblioteca.Repositorio.Interfaces;
using System.Data;
using Microsoft.Data.SqlClient;

namespace ProyectoBiblioteca.Repositorio.DAO
{
    public class LibroDAO : ILibro
    {
        private readonly string cadena;
        public LibroDAO()
        {
            cadena = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("sql");
        }

       

        public IEnumerable<Libro> obtenerLibros()
        {
            List<Libro> lstLibros = new List<Libro>();

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("SELECT l.IdLibro, l.Titulo, a.Descripcion AS Autor, e.Descripcion AS Editorial, c.Descripcion AS Categoria, l.Ejemplares, l.FechaRegistro FROM Libro l INNER JOIN Autor a ON l.IdAutor = a.IdAutor INNER JOIN Editorial e ON l.IdEditorial = e.IdEditorial INNER JOIN Categoria c ON l.IdCategoria = c.IdCategoria", cn);

                cn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Libro reg = new Libro();
                    reg.IdLibro = dr.GetInt32(dr.GetOrdinal("IdLibro"));
                    reg.Titulo = dr.GetString(dr.GetOrdinal("Titulo"));
                    reg.IdAutor = dr.GetString(dr.GetOrdinal("Autor"));
                    reg.IdEditorial = dr.GetString(dr.GetOrdinal("Editorial"));
                    reg.IdCategoria = dr.GetString(dr.GetOrdinal("Categoria"));
                    reg.Ejemplares = dr.GetInt32(dr.GetOrdinal("Ejemplares"));
                    reg.FechaRegistro = dr.GetDateTime(dr.GetOrdinal("FechaRegistro"));
                    lstLibros.Add(reg);
                }

                dr.Close();
            }

            return lstLibros;
        }

        public Libro obtenerLibroPorId(int id)
        {
            Libro libro = null;

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("usp_listarLibroConDescripcion", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdLibro", id);

                cn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    libro = new Libro();
                    libro.IdLibro = dr.GetInt32("IdLibro");
                    libro.Titulo = dr.GetString("Titulo");
                    libro.IdAutor = dr.GetString("AutorDescripcion");
                    libro.IdEditorial = dr.GetString("EditorialDescripcion");
                    libro.IdCategoria = dr.GetString("CategoriaDescripcion");
                    libro.Ejemplares = dr.GetInt32("Ejemplares");
                    libro.FechaRegistro = dr.GetDateTime("FechaRegistro");
                }

                dr.Close();
            }

            return libro;
        }

        public string registrarLibro(Libro reg)
        {
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("usp_CrearLibro", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Titulo", reg.Titulo);
                cmd.Parameters.AddWithValue("@IdAutor", reg.IdAutor);
                cmd.Parameters.AddWithValue("@IdEditorial", reg.IdEditorial);
                cmd.Parameters.AddWithValue("@IdCategoria", reg.IdCategoria);
                cmd.Parameters.AddWithValue("@Ejemplares", reg.Ejemplares);

                cn.Open();
                cmd.ExecuteNonQuery();
            }

            return "Libro registrado exitosamente.";
        }

        public string actualizarLibro(Libro reg)
        {
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("usp_EditarLibro", cn);
                cmd.CommandType = CommandType.StoredProcedure; // Especifica que se está llamando a un procedimiento almacenado

                // Agregar parámetros al comando
                cmd.Parameters.AddWithValue("@IdLibro", reg.IdLibro);
                cmd.Parameters.AddWithValue("@Titulo", reg.Titulo);
                cmd.Parameters.AddWithValue("@IdAutor", reg.IdAutor);
                cmd.Parameters.AddWithValue("@IdEditorial", reg.IdEditorial);
                cmd.Parameters.AddWithValue("@IdCategoria", reg.IdCategoria);
                cmd.Parameters.AddWithValue("@Ejemplares", reg.Ejemplares);
               

                cn.Open();

                cmd.ExecuteNonQuery();
            }

            return "Libro actualizado exitosamente.";
        }

        public string eliminarLibro(int id)
        {
            // Inicializamos el resultado y el mensaje
            int resultado = 0;
            string mensaje = "";

            // Creamos una nueva conexión
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                // Abrimos la conexión
                cn.Open();

                try
                {
                    // Definimos el comando para ejecutar el procedimiento almacenado
                    SqlCommand cmd = new SqlCommand("usp_eliminarLibro", cn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Agregamos el parámetro para el IdLibro
                    cmd.Parameters.AddWithValue("@IdLibro", id);

                    // Ejecutamos el comando y obtenemos el resultado
                    resultado = cmd.ExecuteNonQuery();
                    mensaje = "Eliminación exitosa - cantidad de filas eliminadas: " + resultado;
                }
                catch (SqlException ex)
                {
                    // Capturamos cualquier excepción de SQL y la asignamos al mensaje
                    mensaje = ex.Message;
                }
            }

            // Retornamos el mensaje resultante
            return mensaje;
        }
    }
}
