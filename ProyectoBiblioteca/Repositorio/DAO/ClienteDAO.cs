using System.Data;
using ProyectoBiblioteca.Models;
using ProyectoBiblioteca.Repositorio.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;


namespace ProyectoBiblioteca.Repositorio.DAO
{
    public class ClienteDAO : ICliente
    {
        private readonly string cadena;
        public ClienteDAO()
        {
            cadena = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("sql");
        }
        public string actualizarCliente(Cliente reg)
        {
            SqlConnection cn = new SqlConnection(cadena);

            //Iniciamos proceso
            int resultado = 0;
            string mensaje = "";
            //Abrimos conexión
            cn.Open();

            try
            {
                //Definie el SqlCommand y el tipo de ejecución
                SqlTransaction tr = cn.BeginTransaction(IsolationLevel.Serializable);
                SqlCommand cmd = new SqlCommand("usp_editarCliente", cn, tr);
                cmd.CommandType = CommandType.StoredProcedure;

                //Agregamos parámetros

                cmd.Parameters.AddWithValue("@IdCliente", reg.IdCliente);
                cmd.Parameters.AddWithValue("@Nombre", reg.Nombre);
                cmd.Parameters.AddWithValue("@Apellido", reg.Apellido);
                cmd.Parameters.AddWithValue("@Telefono", reg.Telefono);
                cmd.Parameters.AddWithValue("@Direccion", reg.Direccion);
                

                //Ejecutamos el SqlCommand
                resultado = cmd.ExecuteNonQuery();
                mensaje = "Actualización exitosa - cantidad de filas actualizadas " + resultado;
                tr.Commit();
            }
            catch (SqlException ex)
            {
                mensaje = ex.Message;
            }
            finally
            {
                cn.Close();
            }
            return mensaje;
        }

        public String eliminarCliente(int id)
        {
            SqlConnection cn = new SqlConnection(cadena);

            //Iniciamos proceso
            int resultado = 0;
            string mensaje = "";
            //Abrimos conexión
            cn.Open();
            try
            {
                //Definie el SqlCommand y el tipo de ejecución
                SqlCommand cmd = new SqlCommand("usp_eliminarCliente", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                //Agregamos parámetros
                cmd.Parameters.AddWithValue("@IdCliente", id);

                //Ejecutamos el SqlCommand
                resultado = cmd.ExecuteNonQuery();
                mensaje = "Eliminación exitosa - cantidad de filas eliminadas " + resultado;
            }
            catch (SqlException ex)
            {
                mensaje = ex.Message;
            }
            finally
            {
                cn.Close();
            }
            return mensaje;
        }

        public Cliente obtenerClientePorId(int id)
        {
            Cliente cliente = null;

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("usp_listarClienteConDescripcion", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdCliente", id);

                cn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    cliente = new Cliente();
                    cliente.IdCliente = dr.GetInt32("IdCliente");
                    cliente.Nombre = dr.GetString("Nombre");
                    cliente.Apellido = dr.GetString("Apellido");
                    cliente.Telefono = dr.GetString("Telefono");
                    cliente.Direccion = dr.GetString("Direccion");
                    cliente.FechaCreacion = dr.GetDateTime("FechaCreacion");
                }

                dr.Close();
            }

            return cliente;
        }

        public IEnumerable<Cliente> obtenerClientes()
        {
            //Creamos un ListOF del tipo Cliente
            List<Cliente> lstClientes = new List<Cliente>();
            SqlConnection cn = new SqlConnection(cadena);

            //Definimos un SqlCommand y su CommandType 
            SqlCommand cmd = new SqlCommand("usp_listarCliente", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            //Abrimos conexión
            cn.Open();

            //Ejecutamos el SqlCommand
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Cliente reg = new Cliente();

                reg.IdCliente = dr.GetInt32(0);
                reg.Nombre = dr.GetString("Nombre");
                reg.Apellido = dr.GetString("Apellido");
                reg.Telefono = dr.GetString("Telefono");
                reg.Direccion = dr.GetString("Direccion");
                reg.FechaCreacion = dr.GetDateTime("FechaCreacion");

                lstClientes.Add(reg);
            }

            //Cerramos el SqlDataReader y la conexión a la BD
            dr.Close();
            cn.Close();

            return lstClientes;
        }

        public string registrarCliente(Cliente reg)
        {
            SqlConnection cn = new SqlConnection(cadena);

            //Iniciamos proceso
            int resultado = 0;
            string mensaje = "";

            //Abrimos conexión
            cn.Open();
            try
            {
                //Definie el SqlCommand y el tipo de ejecución
                SqlTransaction tr = cn.BeginTransaction(IsolationLevel.Serializable);
                SqlCommand cmd = new SqlCommand("usp_crearCliente", cn, tr);
                cmd.CommandType = CommandType.StoredProcedure;

                //Agregamos parámetros
                
                cmd.Parameters.AddWithValue("@Nombre", reg.Nombre);
                cmd.Parameters.AddWithValue("@Apellido", reg.Apellido);
                cmd.Parameters.AddWithValue("@Telefono", reg.Telefono);
                cmd.Parameters.AddWithValue("@Direccion", reg.Direccion);
               

                //Ejecutamos el SqlCommand
                resultado = cmd.ExecuteNonQuery();
                mensaje = "Registro exitoso - cantidad de filas insertadas " + resultado;
                tr.Commit();

            }
            catch (SqlException ex)
            {
                mensaje = ex.Message;
            }
            finally
            {
                cn.Close();
            }

            return mensaje;


        }
    }
}