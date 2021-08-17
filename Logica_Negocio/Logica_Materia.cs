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
    public class Logica_Materia
    {
        public Acceso_Datos acceso = new Acceso_Datos(@"Data Source = DESKTOP-3SLJB0F; Initial Catalog = Bitacora2021LabsUTP; Integrated Security = true;");
        SqlCommand sqlc = null;
        public Boolean Inserta_Materia(ClassMateria mat, ref string mensaje_salida)
        {
            SqlParameter[] parametros = new SqlParameter[4];
            parametros[0] = new SqlParameter
            {
                ParameterName = "ID_MATERIA",
                SqlDbType = SqlDbType.SmallInt,
                Direction = ParameterDirection.Input,
                Value = mat.ID_Materia
            };
            parametros[1] = new SqlParameter
            {
                ParameterName = "NOMBRE_MATERIA",
                SqlDbType = SqlDbType.VarChar,
                Size = 120,
                Direction = ParameterDirection.Input,
                Value = mat.Nombre_Materia
            };
            parametros[2] = new SqlParameter
            {
                ParameterName = "HORAS_MATERIA",
                SqlDbType = SqlDbType.TinyInt,
                Direction = ParameterDirection.Input,
                Value = mat.HorasSemana,
            };
            parametros[3] = new SqlParameter
            {
                ParameterName = "EXTRA",
                SqlDbType = SqlDbType.VarChar,
                Size = 50,
                Direction = ParameterDirection.Input,
                Value = mat.Extra
            };
            string sentencia = "Insert into Materia values(@NOMBRE_MATERIA, @HORAS_MATERIA, @EXTRA);";
            Boolean salida = false;
            salida = acceso.Modificar_Segura_Parametros(sentencia, acceso.AbrirConexion(ref mensaje_salida), ref mensaje_salida, parametros);
            
            return salida;
        } //INSERTAR

        public List<ClassMateria> DevuelveporIDMateria(ref string mensaje_salida)
        {
            SqlConnection conabiertatemporal = null;
            string consulta = "select * from Materia";
            conabiertatemporal = acceso.AbrirConexion(ref mensaje_salida);
            SqlDataReader atrapardatos = null;
            atrapardatos = acceso.ConsultasDR(consulta, conabiertatemporal, ref mensaje_salida);
            List<ClassMateria> ListSalida = new List<ClassMateria>();
            if (atrapardatos != null)
            {
                while (atrapardatos.Read())
                {
                    ListSalida.Add(new ClassMateria
                    {
                        ID_Materia = Convert.ToInt32(atrapardatos[0]),
                        Nombre_Materia = atrapardatos[1].ToString(),
                        HorasSemana = Convert.ToInt32(atrapardatos[2]),
                        Extra = atrapardatos[3].ToString()
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

        public DataTable ConsultaGeneralMateria(ref string mensaje_salida)
        {
            string consulta = "select * from Materia";
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
        }//ConsultaGeneral

        public Boolean EliminarMateria(ClassMateria mat, ref string mensaje)
        {
            SqlParameter[] parametros = new SqlParameter[1];
            parametros[0] = new SqlParameter
            {
                ParameterName = "ID_MATERIA",
                SqlDbType = SqlDbType.SmallInt,
                Direction = ParameterDirection.Input,
                Value = mat.ID_Materia
            };
            string sentencia = @"DELETE FROM Materia WHERE Id_Materia = @ID_MATERIA;";
            Boolean salida = false;
            salida = acceso.Modificar_Segura_Parametros(sentencia, acceso.AbrirConexion(ref mensaje), ref mensaje, parametros);
            return salida;
        }


        public Boolean ActualizarMateria (ClassMateria mat, ref string mensaje)
        {
            SqlParameter[] parametros = new SqlParameter[4];
            parametros[0] = new SqlParameter
            {
                ParameterName = "ID_MATERIA",
                SqlDbType = SqlDbType.SmallInt,
                Direction = ParameterDirection.Input,
                Value = mat.ID_Materia
            };
            parametros[1] = new SqlParameter
            {
                ParameterName = "NOMBRE_MATERIA",
                SqlDbType = SqlDbType.VarChar,
                Size = 120,
                Direction = ParameterDirection.Input,
                Value = mat.Nombre_Materia
            };
            parametros[2] = new SqlParameter
            {
                ParameterName = "HORAS_MATERIA",
                SqlDbType = SqlDbType.TinyInt,
                Direction = ParameterDirection.Input,
                Value = mat.HorasSemana,
            };
            parametros[3] = new SqlParameter
            {
                ParameterName = "EXTRA",
                SqlDbType = SqlDbType.VarChar,
                Size = 50,
                Direction = ParameterDirection.Input,
                Value = mat.Extra
            };
            string sentencia = @"update Materia set NombeMateria = @NOMBRE_MATERIA, HorasSemana = @HORAS_MATERIA, Extra = @EXTRA where Id_Materia = @ID_MATERIA;";

            Boolean salida = false;
            salida = acceso.Modificar_Segura_Parametros(sentencia, acceso.AbrirConexion(ref mensaje), ref mensaje, parametros);

            return salida;
        }
    }
}
