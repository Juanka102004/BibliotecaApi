using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using ProyectoBiblioteca.Models;
using ProyectoBiblioteca.Repositorio.Interfaces;
using System.Data;
using Microsoft.Data.SqlClient;

namespace ProyectoBiblioteca.Repositorio.DAO
{
    public class AutorDAO : IAutor
    {
        private readonly string connectionString;

        public AutorDAO()
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            connectionString = config.GetConnectionString("sql");
        }



        public string actualizarAutor(Autor reg)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    cn.Open();
                    SqlTransaction tr = cn.BeginTransaction(IsolationLevel.Serializable);
                    SqlCommand cmd = new SqlCommand("usp_editarAutor", cn,tr);
                    cmd.CommandType = CommandType.StoredProcedure; // Establecer el tipo de comando como procedimiento almacenado

                    // Agregar parámetros
                    cmd.Parameters.AddWithValue("@IdAutor", reg.IdAutor);
                    cmd.Parameters.AddWithValue("@NuevaDescripcion", reg.Descripcion); // Cambiar a @NuevaDescripcion
                                     
                    cmd.ExecuteNonQuery();
                    tr.Commit();
                }

                return "Autor actualizado exitosamente.";
            }
            catch (SqlException ex)
            {
                return ex.Message; // Manejar errores de SQL
            }
        }

        public string eliminarAutor(int id)
        {
            // Inicializamos el resultado y el mensaje
            int resultado = 0;
            string mensaje = "";

            // Creamos una nueva conexión
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                // Abrimos la conexión
                cn.Open();

                try
                {
                    // Definimos el comando para ejecutar el procedimiento almacenado
                    SqlCommand cmd = new SqlCommand("usp_eliminarAutor", cn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Agregamos el parámetro para el IdAutor
                    cmd.Parameters.AddWithValue("@IdAutor", id);

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

        public IEnumerable<Autor> obtenerAutor()
        {
            List<Autor> lstAutores = new List<Autor>();

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("usp_listarAutor", cn);

                cn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Autor reg = new Autor();
                    reg.IdAutor = dr.GetString("IdAutor");
                    reg.Descripcion = dr.GetString("Descripcion");
                    // Si la columna FechaCreacion es de tipo datetime en la base de datos
                    
                    // Si la columna FechaCreacion es de tipo string en la base de datos
                    // reg.FechaCreacion = DateTime.Parse(dr.GetString("FechaCreacion"));
                    lstAutores.Add(reg);
                }

                dr.Close();
            }

            return lstAutores;
        }

        public Autor obtenerAutorPorId(int id)
        {
            Autor autor = null;

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("usp_listarAutor", cn);
                cmd.Parameters.AddWithValue("@IdAutor", id);

                cn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    autor = new Autor();
                    autor.IdAutor = dr.GetString("IdAutor");
                    autor.Descripcion = dr.GetString("Descripcion");
                    // Si la columna FechaCreacion es de tipo datetime en la base de datos
                   
                    // Si la columna FechaCreacion es de tipo string en la base de datos
                    // autor.FechaCreacion = DateTime.Parse(dr.GetString("FechaCreacion"));
                }

                dr.Close();
            }

            return autor;
        }

        public string registrarAutor(Autor reg)
        {
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                SqlTransaction tr = cn.BeginTransaction(IsolationLevel.Serializable);
                SqlCommand cmd = new SqlCommand("usp_RegistrarAutor", cn, tr);
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdAutor", reg.IdAutor);
                    cmd.Parameters.AddWithValue("@Descripcion", reg.Descripcion);

                   

                    cmd.ExecuteNonQuery();
                    tr.Commit();
                }
            }

            return "Autor registrado exitosamente.";
        }

    }
}
