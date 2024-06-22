using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ProyectoBiblioteca.Models;
using ProyectoBiblioteca.Repositorio.Interfaces;

namespace ProyectoBiblioteca.Repositorio.DAO
{
    public class CategoriaDAO : ICategoria
    {
        private readonly string cadena;
        public CategoriaDAO()
        {
            cadena = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("sql");
        }

        public string actualizarCategoria(Categoria reg)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(cadena))
                {
                    cn.Open();
                    SqlTransaction tr = cn.BeginTransaction(IsolationLevel.Serializable);
                    SqlCommand cmd = new SqlCommand("usp_editarCategoria", cn);
                    cmd.CommandType = CommandType.StoredProcedure; // Establecer el tipo de comando como procedimiento almacenado

                    // Agregar parámetros
                    cmd.Parameters.AddWithValue("@IdCategoria", reg.IdCategoria);
                    cmd.Parameters.AddWithValue("@NuevaDescripcion", reg.Descripcion); // Cambiar a @NuevaDescripcion
                                     

                    cmd.ExecuteNonQuery();
                    tr.Commit();
                }

                return "Categoría actualizada exitosamente.";
            }
            catch (SqlException ex)
            {
                return ex.Message; // Manejar errores de SQL
            }
        }


        public string eliminarCategoria(int id)
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
                    SqlCommand cmd = new SqlCommand("usp_eliminarCategoria", cn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Agregamos el parámetro para el IdLibro
                    cmd.Parameters.AddWithValue("@IdCategoria", id);

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

        public IEnumerable<Categoria> obtenerCategoria()
        {
            List<Categoria> lstCategorias = new List<Categoria>();

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("usp_listarCategoria", cn);

                cn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Categoria reg = new Categoria();
                    reg.IdCategoria = dr.GetString("IdCategoria");
                    reg.Descripcion = dr.GetString("descripcion");
                   
                    lstCategorias.Add(reg);
                }

                dr.Close();
            }

            return lstCategorias;
        }

        public Categoria obtenerCategoriaPorId(int id)
        {
            Categoria categoria = null;

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("usp_listarCategoria", cn);
                cmd.Parameters.AddWithValue("@IdCategoria", id);

                cn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    categoria = new Categoria();
                    categoria.IdCategoria = dr.GetString("IdCategoria");
                    categoria.Descripcion = dr.GetString("descripcion");
                    
                }

                dr.Close();
            }

            return categoria;
        }

        public string registrarCategoria(Categoria reg)
        {
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                cn.Open();
                SqlTransaction tr = cn.BeginTransaction(IsolationLevel.Serializable);
                SqlCommand cmd = new SqlCommand("usp_RegistrarCategoria", cn,tr);
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdCategoria", reg.IdCategoria);
                    cmd.Parameters.AddWithValue("@descripcion", reg.Descripcion);                   

                    cmd.ExecuteNonQuery();
                    tr.Commit();
                }
            }

            return "Categoría registrada exitosamente.";
        }

    }
}
