using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using ProyectoBiblioteca.Models;
using ProyectoBiblioteca.Repositorio.Interfaces;
using System.Data;
using Microsoft.Data.SqlClient;

namespace ProyectoBiblioteca.Repositorio.DAO
{
    public class Estado_PrestamoDAO : IEstado_Prestamo
    {
        private readonly string connectionString;

        public Estado_PrestamoDAO()
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            connectionString = config.GetConnectionString("sql");
        }

        public string actualizarEstado_Prestamo(Estado_Prestamo reg)
        {
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                SqlTransaction tr = cn.BeginTransaction(IsolationLevel.Serializable);
                SqlCommand cmd = new SqlCommand("usp_editarEstado_Prestamo", cn,tr);

                cmd.Parameters.AddWithValue("@IdEstadoPrestamo", reg.IdEstadoPrestamo);
                cmd.Parameters.AddWithValue("@Descripcion", reg.Descripcion);
                cmd.Parameters.AddWithValue("@Estado", reg.Estado);

                cmd.ExecuteNonQuery();
                tr.Commit();

            }

            return "Estado de préstamo actualizado exitosamente.";
        }

        public string eliminarEstado_Prestamo(int id)
        {
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                SqlTransaction tr = cn.BeginTransaction(IsolationLevel.Serializable);
                SqlCommand cmd = new SqlCommand("usp_eliminarEstado_Prestamo", cn, tr);
                cmd.Parameters.AddWithValue("@IdEstadoPrestamo", id);

                

                cmd.ExecuteNonQuery();
                tr.Commit();
            }

            return "Estado de préstamo eliminado exitosamente.";
        }

        public IEnumerable<Estado_Prestamo> obtenerEstado_Prestamo()
        {
            List<Estado_Prestamo> lstEstadoPrestamo = new List<Estado_Prestamo>();

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("usp_listarEstado_Prestamo", cn);

                cn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Estado_Prestamo reg = new Estado_Prestamo();
                    reg.IdEstadoPrestamo = dr.GetInt32("IdEstadoPrestamo");
                    reg.Descripcion = dr.GetString("Descripcion");
                    reg.Estado = dr.GetBoolean("Estado");
                    reg.FechaCreacion = dr.GetDateTime("FechaCreacion");
                    lstEstadoPrestamo.Add(reg);
                }

                dr.Close();
            }

            return lstEstadoPrestamo;
        }

        public Estado_Prestamo obtenerEstado_PrestamoPorId(int id)
        {
            Estado_Prestamo estadoPrestamo = null;

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("usp_listarEstado_Prestamo", cn);
                cmd.Parameters.AddWithValue("@IdEstadoPrestamo", id);

                cn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    estadoPrestamo = new Estado_Prestamo();
                    estadoPrestamo.IdEstadoPrestamo = dr.GetInt32("IdEstadoPrestamo");
                    estadoPrestamo.Descripcion = dr.GetString("Descripcion");
                    estadoPrestamo.Estado = dr.GetBoolean("Estado");
                    estadoPrestamo.FechaCreacion = dr.GetDateTime("FechaCreacion");
                }

                dr.Close();
            }

            return estadoPrestamo;
        }

        public string registrarEstado_Prestamo(Estado_Prestamo reg)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    cn.Open();
                    SqlTransaction tr = cn.BeginTransaction(IsolationLevel.Serializable);
                    SqlCommand cmd = new SqlCommand("usp_crearEstado_Prestamo", cn,tr);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Agregar parámetros
                    cmd.Parameters.AddWithValue("@Descripcion", reg.Descripcion);
                    cmd.Parameters.AddWithValue("@Estado", reg.Estado);

                    cmd.ExecuteNonQuery();
                    tr.Commit();
                }

                return "Estado de préstamo registrado exitosamente.";
            }
            catch (SqlException ex)
            {
                return "Error al registrar el estado de préstamo: " + ex.Message;
            }
            catch (Exception ex)
            {
                return "Error inesperado: " + ex.Message;
            }
        }
    }
}
