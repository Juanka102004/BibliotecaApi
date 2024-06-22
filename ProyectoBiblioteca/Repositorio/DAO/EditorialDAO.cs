using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using ProyectoBiblioteca.Models;
using ProyectoBiblioteca.Repositorio.Interfaces;
using System.Data;
using Microsoft.Data.SqlClient;


namespace ProyectoBiblioteca.Repositorio.DAO
{
    public class EditorialDAO : IEditorial
    {

        private readonly string connectionString;

        public EditorialDAO()
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            connectionString = config.GetConnectionString("sql");
        }

        public string actualizarEditorial(Editorial reg)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    cn.Open();
                    SqlTransaction tr = cn.BeginTransaction(IsolationLevel.Serializable);
                    SqlCommand cmd = new SqlCommand("usp_editarEditorial", cn,tr);
                    cmd.CommandType = CommandType.StoredProcedure; // Establecer el tipo de comando como procedimiento almacenado

                    // Agregar parámetros
                    cmd.Parameters.AddWithValue("@IdEditorial", reg.IdEditorial);
                    cmd.Parameters.AddWithValue("@NuevaDescripcion", reg.Descripcion); // Cambiar a @NuevaDescripcion


                    cmd.ExecuteNonQuery();
                    tr.Commit();
                }

                return "Editorial actualizada exitosamente.";
            }
            catch (SqlException ex)
            {
                return ex.Message; // Manejar errores de SQL
            }
        }

        public string eliminarEditorial(int id)
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
                    SqlCommand cmd = new SqlCommand("usp_eliminarEditorial", cn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Agregamos el parámetro para el IdEditorial
                    cmd.Parameters.AddWithValue("@IdEditorial", id);

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

        public IEnumerable<Editorial> obtenerEditorial()
        {
            List<Editorial> lstEditoriales = new List<Editorial>();

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("usp_listarEditorial", cn);

                cn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Editorial reg = new Editorial();
                    reg.IdEditorial = dr.GetString("IdEditorial");
                    reg.Descripcion = dr.GetString("Descripcion");
                  
                    lstEditoriales.Add(reg);
                }

                dr.Close();
            }

            return lstEditoriales;
        }

        public Editorial obtenerEditorialPorId(int id)
        {
            Editorial editorial = null;

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("usp_listarEditorial", cn);
                cmd.Parameters.AddWithValue("@IdEditorial", id);

                cn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    editorial = new Editorial();
                    editorial.IdEditorial = dr.GetString("IdEditorial");
                    editorial.Descripcion = dr.GetString("Descripcion");
                    
                }

                dr.Close();
            }

            return editorial;
        }

        public string registrarEditorial(Editorial reg)
        {
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                SqlTransaction tr = cn.BeginTransaction(IsolationLevel.Serializable);
                SqlCommand cmd = new SqlCommand("usp_RegistrarEditorial", cn, tr);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idEditorial", reg.IdEditorial);
                cmd.Parameters.AddWithValue("@Descripcion", reg.Descripcion);
            
                cmd.ExecuteNonQuery();
                tr.Commit();
            }

            return "Editorial registrado exitosamente.";
        }
    }
}
