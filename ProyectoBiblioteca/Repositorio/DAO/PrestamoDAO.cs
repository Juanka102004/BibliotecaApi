using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using ProyectoBiblioteca.Models;
using ProyectoBiblioteca.Repositorio.Interfaces;
using System.Data;
using Microsoft.Data.SqlClient;


namespace ProyectoBiblioteca.Repositorio.DAO
{
    public class PrestamoDAO : IPrestamo
    {

        private readonly string cadena;
        public PrestamoDAO()
        {
            cadena = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("sql");
        }

        public string actualizarPrestamo(Prestamo reg)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(cadena))
                {
                    SqlCommand cmd = new SqlCommand("usp_editarPrestamo", cn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Agregar parámetros
                    cmd.Parameters.AddWithValue("@IdPrestamo", reg.IdPrestamo);
                    cmd.Parameters.AddWithValue("@IdCliente", reg.IdCliente);
                    cmd.Parameters.AddWithValue("@IdLibro", reg.IdLibro);

                    // FechaDevolucion
                    if (reg.FechaDevolucion.HasValue)
                        cmd.Parameters.AddWithValue("@FechaDevolucion", reg.FechaDevolucion.Value);
                    else
                        cmd.Parameters.AddWithValue("@FechaDevolucion", DBNull.Value);

                    // EstadoEntregado
                    if (!string.IsNullOrEmpty(reg.EstadoEntregado))
                        cmd.Parameters.AddWithValue("@EstadoEntregado", reg.EstadoEntregado);
                    else
                        cmd.Parameters.AddWithValue("@EstadoEntregado", DBNull.Value);

                    // EstadoRecibido
                    if (!string.IsNullOrEmpty(reg.EstadoRecibido))
                        cmd.Parameters.AddWithValue("@EstadoRecibido", reg.EstadoRecibido);
                    else
                        cmd.Parameters.AddWithValue("@EstadoRecibido", DBNull.Value);

                    // FechaConfirmacionDevolucion
                    if (reg.FechaConfirmacionDevolucion.HasValue)
                        cmd.Parameters.AddWithValue("@FechaConfirmacionDevolucion", reg.FechaConfirmacionDevolucion.Value);
                    else
                        cmd.Parameters.AddWithValue("@FechaConfirmacionDevolucion", DBNull.Value);

                    cmd.Parameters.AddWithValue("@Estado", reg.Estado);

                    cn.Open();

                    cmd.ExecuteNonQuery();
                }

                return "Prestamo actualizado exitosamente.";
            }
            catch (SqlException ex)
            {
                return "Error al actualizar el préstamo: " + ex.Message;
            }
            catch (Exception ex)
            {
                return "Error inesperado: " + ex.Message;
            }
        }

        public string eliminarPrestamo(int id)
        {
            {
                try
                {
                    using (SqlConnection cn = new SqlConnection(cadena))
                    {
                        cn.Open();

                        SqlCommand cmd = new SqlCommand("usp_eliminarPrestamo", cn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@IdPrestamo", id);

                        // Ejecutar el comando
                        cmd.ExecuteNonQuery();

                        return "Préstamo eliminado correctamente.";
                    }
                }
                catch (SqlException ex)
                {
                    return "Error al eliminar el préstamo: " + ex.Message;
                }
                catch (Exception ex)
                {
                    return "Error inesperado: " + ex.Message;
                }
            }
        }

        public IEnumerable<Prestamo> obtenerPrestamo()
        {
            List<Prestamo> lstPrestamos = new List<Prestamo>();

            try
            {
                using (SqlConnection cn = new SqlConnection(cadena))
                {
                    SqlCommand cmd = new SqlCommand("MostrarPrestamosConDetalle", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Prestamo reg = new Prestamo
                            {
                                IdPrestamo = dr.GetInt32(dr.GetOrdinal("IdPrestamo")),
                                NombreCliente = dr.IsDBNull(dr.GetOrdinal("NombreCliente")) ? null : dr.GetString(dr.GetOrdinal("NombreCliente")),
                                TituloLibro = dr.IsDBNull(dr.GetOrdinal("TituloLibro")) ? null : dr.GetString(dr.GetOrdinal("TituloLibro")),
                                Estado = dr.GetBoolean(dr.GetOrdinal("Estado")),
                                FechaDevolucion = dr.IsDBNull(dr.GetOrdinal("FechaDevolucion")) ? (DateTime?)null : dr.GetDateTime(dr.GetOrdinal("FechaDevolucion")),
                                EstadoEntregado = dr.IsDBNull(dr.GetOrdinal("EstadoEntregado")) ? null : dr.GetString(dr.GetOrdinal("EstadoEntregado")),
                                EstadoRecibido = dr.IsDBNull(dr.GetOrdinal("EstadoRecibido")) ? null : dr.GetString(dr.GetOrdinal("EstadoRecibido")),
                                FechaConfirmacionDevolucion = dr.IsDBNull(dr.GetOrdinal("FechaConfirmacionDevolucion")) ? (DateTime?)null : dr.GetDateTime(dr.GetOrdinal("FechaConfirmacionDevolucion")),
                                FechaCreacion = dr.GetDateTime(dr.GetOrdinal("FechaCreacion"))
                            };

                            lstPrestamos.Add(reg);
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                // Manejar excepción específica de SQL
                Console.WriteLine($"SQL Error: {sqlEx.Message}");
                throw;
            }
            catch (Exception ex)
            {
                // Manejar otras excepciones
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }

            return lstPrestamos;
        }

        public Prestamo obtenerPrestamoPorId(int id)
        {
            Prestamo prestamo = null;

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("usp_obtenerPrestamoPorId", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdPrestamo", id);

                cn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    prestamo = new Prestamo();
                    prestamo.IdPrestamo = dr.GetInt32("IdPrestamo");
                    prestamo.IdCliente = dr.GetInt32("IdCliente");
                    prestamo.IdLibro = dr.GetInt32("IdLibro");
                    prestamo.Estado = dr.GetBoolean("Estado");
                    prestamo.FechaDevolucion = dr.GetDateTime("FechaDevolucion");
                    prestamo.EstadoEntregado = dr.IsDBNull("EstadoEntregado") ? null : (string)dr["EstadoEntregado"];
                    prestamo.EstadoRecibido = dr.IsDBNull("EstadoRecibido") ? null : (string)dr["EstadoRecibido"];
                    prestamo.FechaConfirmacionDevolucion = dr.IsDBNull("FechaConfirmacionDevolucion") ? default(DateTime) : (DateTime)dr["FechaConfirmacionDevolucion"];
                    prestamo.FechaCreacion = dr.GetDateTime("FechaCreacion");
                }

                dr.Close();
            }

            return prestamo;
        }

        public string registrarPrestamo(Prestamo reg)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(cadena))
                {
                    cn.Open();
                    SqlTransaction tr = cn.BeginTransaction(IsolationLevel.Serializable);
                    SqlCommand cmd = new SqlCommand("usp_crearPrestamo", cn, tr);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Agregar parámetros
                    cmd.Parameters.AddWithValue("@IdCliente", reg.IdCliente);
                    cmd.Parameters.AddWithValue("@IdLibro", reg.IdLibro);

                    // FechaDevolucion
                    if (reg.FechaDevolucion.HasValue)
                        cmd.Parameters.AddWithValue("@FechaDevolucion", reg.FechaDevolucion.Value);
                    else
                        cmd.Parameters.AddWithValue("@FechaDevolucion", DBNull.Value);

                    // EstadoEntregado
                    if (!string.IsNullOrEmpty(reg.EstadoEntregado))
                        cmd.Parameters.AddWithValue("@EstadoEntregado", reg.EstadoEntregado);
                    else
                        cmd.Parameters.AddWithValue("@EstadoEntregado", DBNull.Value);

                    // EstadoRecibido
                    if (!string.IsNullOrEmpty(reg.EstadoRecibido))
                        cmd.Parameters.AddWithValue("@EstadoRecibido", reg.EstadoRecibido);
                    else
                        cmd.Parameters.AddWithValue("@EstadoRecibido", DBNull.Value);

                    // FechaConfirmacionDevolucion
                    if (reg.FechaConfirmacionDevolucion.HasValue)
                        cmd.Parameters.AddWithValue("@FechaConfirmacionDevolucion", reg.FechaConfirmacionDevolucion.Value);
                    else
                        cmd.Parameters.AddWithValue("@FechaConfirmacionDevolucion", DBNull.Value);

                    cmd.Parameters.AddWithValue("@Estado", reg.Estado);

                    cmd.ExecuteNonQuery();
                    tr.Commit();
                }

                return "Prestamo registrado exitosamente.";
            }
            catch (SqlException ex)
            {
                return "Error al registrar el préstamo: " + ex.Message;
            }
            catch (Exception ex)
            {
                return "Error inesperado: " + ex.Message;
            }
        }   
    }
}
