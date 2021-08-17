using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;

namespace ConexionBD
{
    public class Acceso_Datos
    {

        private string CADCON;

        public Acceso_Datos(string ccon)
        {
            CADCON = ccon;
        }

        public SqlConnection AbrirConexion(ref string mensaje)
        {
            SqlConnection con1 = new SqlConnection();
            con1.ConnectionString = CADCON;
            try
            {
                con1.Open();
                mensaje = "Conexión Abierta";
            }
            catch (Exception r)
            {
                con1 = null;
                mensaje = "Error: " + r.Message;
            }
            return con1;
        }

        public DataSet ConsultasDS(string consultasql, SqlConnection ConAbierta, ref string mensaje)
        {
            SqlCommand carrito = null;
            SqlDataAdapter trailer = null;
            DataSet salida = new DataSet();
            if (ConAbierta == null)
            {
                mensaje = "No hay conexión a la Base de Datos";
                salida = null;
            }
            else
            {
                carrito = new SqlCommand();
                carrito.CommandText = consultasql;
                carrito.Connection = ConAbierta;

                trailer = new SqlDataAdapter();
                trailer.SelectCommand = carrito;

                try
                {
                    trailer.Fill(salida, "consulta");
                    mensaje = "Consulta correcta";
                }
                catch (Exception e)
                {
                    salida = null;
                    mensaje = "Error" + e.Message;
                }
                ConAbierta.Close();
                ConAbierta.Dispose();
            }
            return salida;
        }//Consultas Data set

        public SqlDataReader ConsultasDR(string consultas, SqlConnection ConAbierta, ref string mensaje)
        {
            SqlCommand carrito = null;
            SqlDataReader contenedor = null;
            if (ConAbierta == null)
            {
                mensaje = "No hay conexión";
                contenedor = null;
            }
            else
            {
                carrito = new SqlCommand();
                carrito.CommandText = consultas;
                carrito.Connection = ConAbierta;

                try
                {
                    contenedor = carrito.ExecuteReader();
                    mensaje = "Consulta correcta";
                }
                catch (Exception e)
                {
                    contenedor = null;
                    mensaje = "Error" + e.Message;
                }
            }
            return contenedor;
        }//Consulta Data Reader

        public Boolean Multiples_consultasDS(string consulta, SqlConnection ConAbierta, ref string mensaje, ref DataSet dataset1, string nomConsulta)
        {
            SqlCommand carrito = null;
            SqlDataAdapter trailer = null;
            Boolean salida = false;
            if (ConAbierta == null)
            {
                mensaje = "No hay conexion";
                salida = false;
            }
            else
            {
                carrito = new SqlCommand();
                carrito.CommandText = consulta;
                carrito.Connection = ConAbierta;
                trailer = new SqlDataAdapter();
                trailer.SelectCommand = carrito;
                try
                {
                    trailer.Fill(dataset1, nomConsulta);
                    mensaje = "Consulta correcta en el DataSet";
                    salida = true;
                }
                catch (Exception a)
                {
                    mensaje = "Error: " + a.Message;
                }
                ConAbierta.Close();
                ConAbierta.Dispose();
            }
            return salida;

        }

        public Boolean Modificar_insegura(string cadenasql, SqlConnection conabierta, ref string mensaje)
        {
            SqlCommand carrito = null;
            Boolean salida = false;
            if (conabierta == null)
            {
                mensaje = "No hay conexión";
                salida = false;
            }
            else
            {
                carrito = new SqlCommand();
                carrito.CommandText = cadenasql;
                carrito.Connection = conabierta;
                try
                {
                    carrito.ExecuteNonQuery();
                    mensaje = "Modificación realizada";
                    salida = true;
                }
                catch (Exception e)
                {
                    salida = false;
                    mensaje = "Error" + e.Message;
                }
                conabierta.Close();
                conabierta.Dispose();
            }
            return salida;
        }

        public Boolean InsertarParametro(string sentencia, SqlConnection conabierta, ref string mensaje, SqlParameter par1, SqlParameter par2)
        {
            Boolean salida = false;
            SqlCommand carrito = null;
            if (conabierta != null)
            {
                carrito = new SqlCommand();
                carrito.CommandText = sentencia;
                carrito.Connection = conabierta;
                carrito.Parameters.Add(par1);
                carrito.Parameters.Add(par2);
                try
                {
                    carrito.ExecuteNonQuery();
                    mensaje = "Modificación Correcta";
                    salida = true;
                }
                catch (Exception e)
                {
                    mensaje = "Error: " + e.Message;
                    salida = false;
                }
                conabierta.Close();
                conabierta.Dispose();
            }
            else
            {
                mensaje = "No hay conexión a la BD";
            }
            return salida;
        }

        public Boolean Modificar_Segura_Parametros(string sentencia, SqlConnection conabierta, ref string mensaje, SqlParameter[] parametros)
        {
            Boolean salida = false;
            SqlCommand carrito = null;

            if (conabierta != null)
            {
                carrito = new SqlCommand();
                carrito.CommandText = sentencia;
                carrito.Connection = conabierta;
                foreach (SqlParameter x in parametros)
                {
                    carrito.Parameters.Add(x);
                }
                try
                {
                    carrito.ExecuteReader();
                    mensaje = "Modificación correcta en la Base de Datos";
                    salida = true;
                }
                catch (Exception e)
                {
                    mensaje = "Error:" + e.Message;
                    salida = false;
                }
                conabierta.Close();
                conabierta.Dispose();
            }
            else
            {
                mensaje = "No hay conexión";
            }
            return salida;
        }
    }

}
