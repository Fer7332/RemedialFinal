using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ConexionBD;
using Objetos;
using System.Data;
using System.Data.SqlClient;

namespace Logica_Negocio
{
    public class Logica_GradoEspecialidad
    {
        public Acceso_Datos acceso = new Acceso_Datos(@"Data Source = DESKTOP-3SLJB0F; Initial Catalog = Bitacora2021LabsUTP; Integrated Security = true;");
        SqlCommand sqlc = null;
        public Boolean Inserta_Grado_Especialidad(ClassGradoEspecialidad gresp, ref string mensaje_salida)
        {
            SqlParameter[] parametros = new SqlParameter[5];
            parametros[0] = new SqlParameter
            {
                ParameterName = "ID_GRADO",
                SqlDbType = SqlDbType.SmallInt,
                Direction = ParameterDirection.Input,
                Value = gresp.ID_Grado
            };
            parametros[1] = new SqlParameter
            {
                ParameterName = "TITULO",
                SqlDbType = SqlDbType.VarChar,
                Size = 150,
                Direction = ParameterDirection.Input,
                Value = gresp.Titulo
            };
            parametros[2] = new SqlParameter
            {
                ParameterName = "INSTITUCION",
                SqlDbType = SqlDbType.VarChar,
                Size = 150,
                Direction = ParameterDirection.Input,
                Value = gresp.Institucion
            };
            parametros[3] = new SqlParameter
            {
                ParameterName = "PAIS",
                SqlDbType = SqlDbType.VarChar,
                Size = 50,
                Direction = ParameterDirection.Input,
                Value = gresp.Pais
            };
            parametros[4] = new SqlParameter
            {
                ParameterName = "EXTRA",
                SqlDbType = SqlDbType.VarChar,
                Size = 50,
                Direction = ParameterDirection.Input,
                Value = gresp.Extra
            };
            string sentencia = "Insert into GradoEspecialidad values (@TITULO, @INSTITUCION, @PAIS, @EXTRA);";
            Boolean salida = false;
            salida = acceso.Modificar_Segura_Parametros(sentencia, acceso.AbrirConexion(ref mensaje_salida), ref mensaje_salida, parametros);
            return salida;
        }//Cierra Insertar

        public List<ClassGradoEspecialidad> DevuelveporIDGradoEsp(ref string mensaje_salida)
        {
            SqlConnection conabiertatemporal = null;
            string consulta = "select * from GradoEspecialidad";
            conabiertatemporal = acceso.AbrirConexion(ref mensaje_salida);
            SqlDataReader atrapardatos = null;
            atrapardatos = acceso.ConsultasDR(consulta, conabiertatemporal, ref mensaje_salida);
            List<ClassGradoEspecialidad> ListSalida = new List<ClassGradoEspecialidad>();
            if (atrapardatos != null)
            {
                while (atrapardatos.Read())
                {
                    ListSalida.Add(new ClassGradoEspecialidad
                    {
                        ID_Grado = Convert.ToInt32(atrapardatos[0]),
                        Titulo = atrapardatos[1].ToString(),
                        Institucion = atrapardatos[2].ToString(),
                        Pais = atrapardatos[3].ToString(),
                        Extra = atrapardatos[4].ToString()
                    });
                }
            }
            else
            {
                ListSalida = null;
            }
            conabiertatemporal.Close();
            conabiertatemporal.Dispose();
            return ListSalida;
        }//Devuelve por ID

        public DataTable ConsultaGeneralGradoEsp(ref string mensaje_salida)
        {
            string consulta = "select * from GradoEspecialidad";
            DataSet contenedor = null;
            DataTable Tablafinal = null;
            contenedor = acceso.ConsultasDS(consulta, acceso.AbrirConexion(ref mensaje_salida), ref mensaje_salida);
            if (contenedor != null)
            {
                Tablafinal = contenedor.Tables[0];
                if (Tablafinal.Rows.Count == 0)
                {
                    Tablafinal.Rows.Add(Tablafinal.NewRow());
                }
            }
            return Tablafinal;
        }

       public Boolean EliminarGradoEspecialidad(ClassGradoEspecialidad graesp, ref string mensaje)
        {
            SqlParameter[] parametros = new SqlParameter[1];
            parametros[0] = new SqlParameter
            {
                ParameterName = "ID_GRADOESP",
                SqlDbType = SqlDbType.SmallInt,
                Direction = ParameterDirection.Input,
                Value = graesp.ID_Grado
            };
            string sentencia = @"DELETE FROM GradoEspecialidad WHERE Id_Grado = @ID_GRADOESP;";
            Boolean salida = false;
            salida = acceso.Modificar_Segura_Parametros(sentencia, acceso.AbrirConexion(ref mensaje), ref mensaje, parametros);
            return salida;
        }


        public Boolean ActualizarGradoEsp (ClassGradoEspecialidad graesp, ref string mensaje)
        {
            SqlParameter[] parametros = new SqlParameter[5];
            parametros[0] = new SqlParameter
            {
                ParameterName = "ID_GRADO",
                SqlDbType = SqlDbType.SmallInt,
                Direction = ParameterDirection.Input,
                Value = graesp.ID_Grado
            };
            parametros[1] = new SqlParameter
            {
                ParameterName = "TITULO",
                SqlDbType = SqlDbType.VarChar,
                Size = 150,
                Direction = ParameterDirection.Input,
                Value = graesp.Titulo
            };
            parametros[2] = new SqlParameter
            {
                ParameterName = "INSTITUCION",
                SqlDbType = SqlDbType.VarChar,
                Size = 150,
                Direction = ParameterDirection.Input,
                Value = graesp.Institucion
            };
            parametros[3] = new SqlParameter
            {
                ParameterName = "PAIS",
                SqlDbType = SqlDbType.VarChar,
                Size = 50,
                Direction = ParameterDirection.Input,
                Value = graesp.Pais
            };
            parametros[4] = new SqlParameter
            {
                ParameterName = "EXTRA",
                SqlDbType = SqlDbType.VarChar,
                Size = 50,
                Direction = ParameterDirection.Input,
                Value = graesp.Extra
            };
            string sentencia = @"update GradoEspecialidad set Titulo = @TITULO, Institucion = @INSTITUCION, Pais = @PAIS, Extra = @EXTRA where Id_Grado = @ID_GRADO;";

            Boolean salida = false;
            salida = acceso.Modificar_Segura_Parametros(sentencia, acceso.AbrirConexion(ref mensaje), ref mensaje, parametros);

            return salida;
        }
    }
}
