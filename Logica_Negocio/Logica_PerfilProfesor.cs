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
    public class Logica_PerfilProfesor
    {
        public Acceso_Datos acceso = new Acceso_Datos(@"Data Source = DESKTOP-3SLJB0F; Initial Catalog = Bitacora2021LabsUTP; Integrated Security = true;");
        SqlCommand sqlc = null;
        public Boolean Inserta_PerfilProfe(ClassPerfilProfesor pprofe, ref string mensaje_salida)
        {
            SqlParameter[] parametros = new SqlParameter[6];
            parametros[0] = new SqlParameter
            {
                ParameterName = "ID_PERFILPROFE",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                Value = pprofe.ID_Perfil
            };
            parametros[1] = new SqlParameter
            {
                ParameterName = "ID_PROFESOR",
                SqlDbType = SqlDbType.SmallInt,
                Direction = ParameterDirection.Input,
                Value = pprofe.ID_Profe
            };
            parametros[2] = new SqlParameter
            {
                ParameterName = "ID_GRADO",
                SqlDbType = SqlDbType.SmallInt,
                Direction = ParameterDirection.Input,
                Value = pprofe.ID_Grado
            };
            parametros[3] = new SqlParameter
            {
                ParameterName = "ESTADO",
                SqlDbType = SqlDbType.VarChar,
                Size = 150,
                Direction = ParameterDirection.Input,
                Value = pprofe.Estado
            };
            parametros[4] = new SqlParameter
            {
                ParameterName = "FECHA_OBTENCION",
                SqlDbType = SqlDbType.DateTime,
                Direction = ParameterDirection.Input,
                Value = pprofe.FechaObtencion
            };
            parametros[5] = new SqlParameter
            {
                ParameterName = "EVIDENCIA",
                SqlDbType = SqlDbType.VarChar,
                Size = 50,
                Direction = ParameterDirection.Input,
                Value = pprofe.Evidencia
            };
            string sentencia = "Insert into PerfilProfe values (@ID_PROFESOR, @ID_GRADO, @ESTADO, @FECHA_OBTENCION, @EVIDENCIA);";
            Boolean salida = false;
            salida = acceso.Modificar_Segura_Parametros(sentencia, acceso.AbrirConexion(ref mensaje_salida), ref mensaje_salida, parametros);
            return salida;
        } //INSERTAR

        public List<ClassPerfilProfesor> DevuelveporID_PerfilProfesor(ref string mensaje_salida)
        {
            SqlConnection conabiertatemporal = null;
            string consulta = @"select * from PerfilProfe";
            conabiertatemporal = acceso.AbrirConexion(ref mensaje_salida);
            SqlDataReader atrapardatos = null;
            atrapardatos = acceso.ConsultasDR(consulta, conabiertatemporal, ref mensaje_salida);
            List<ClassPerfilProfesor> ListSalida = new List<ClassPerfilProfesor>();
            if (atrapardatos != null)
            {
                while (atrapardatos.Read())
                {
                    ListSalida.Add(new ClassPerfilProfesor
                    {
                        ID_Perfil = (int)atrapardatos[0],
                        ID_Profe = (int)atrapardatos[1],
                        ID_Grado = (int)atrapardatos[2],
                        Estado = atrapardatos[3].ToString(),
                        FechaObtencion = Convert.ToDateTime(atrapardatos[4]),
                        Evidencia = atrapardatos[5].ToString()
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
        }//Devuelve por ID@"select (select Nombre from Profesor where ID_Profe = "+nombre+") as Profesor, (select Titulo from GradoEspecialidad where Id_Grado = "+titulo+") as Especialidad, Estado, FechaObtencion, Evidencia from PerfilProfe";

        public DataTable ConsultaGeneralPerfilProfesor(ref string mensaje_salida)
        {
            string consulta = @"select (select (Profesor.Nombre)) as Profesor, (select (GradoEspecialidad.Titulo)) as Grado, (select (GradoEspecialidad.Institucion)) as Institucion, FechaObtencion, Evidencia from PerfilProfe, Profesor,GradoEspecialidad where Profesor.ID_Profe = PerfilProfe.F_Profe and GradoEspecialidad.Id_Grado = PerfilProfe.F_Grado";
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

        public Boolean EliminarPerfilProfesor(ClassPerfilProfesor pprof, ref string mensaje)
        {
            SqlParameter[] parametros = new SqlParameter[1];
            parametros[0] = new SqlParameter
            {
                ParameterName = "ID_PROFESOR",
                SqlDbType = SqlDbType.SmallInt,
                Direction = ParameterDirection.Input,
                Value = pprof.ID_Profe
            };
            string sentencia = @"DELETE FROM PerfilProfe WHERE F_Profe = @ID_PROFESOR;";
            Boolean salida = false;
            salida = acceso.Modificar_Segura_Parametros(sentencia, acceso.AbrirConexion(ref mensaje), ref mensaje, parametros);
            return salida;
        }

        public Boolean ActualizarPerfilProfe(ClassPerfilProfesor pprof, ref string mensaje)
        {
            SqlParameter[] parametros = new SqlParameter[6];
            parametros[0] = new SqlParameter
            {
                ParameterName = "ID_PERFILPROFE",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                Value = pprof.ID_Perfil
            };
            parametros[1] = new SqlParameter
            {
                ParameterName = "ID_PROFESOR",
                SqlDbType = SqlDbType.SmallInt,
                Direction = ParameterDirection.Input,
                Value = pprof.ID_Profe
            };
            parametros[2] = new SqlParameter
            {
                ParameterName = "ID_GRADO",
                SqlDbType = SqlDbType.SmallInt,
                Direction = ParameterDirection.Input,
                Value = pprof.ID_Grado
            };
            parametros[3] = new SqlParameter
            {
                ParameterName = "ESTADO",
                SqlDbType = SqlDbType.VarChar,
                Size = 150,
                Direction = ParameterDirection.Input,
                Value = pprof.Estado
            };
            parametros[4] = new SqlParameter
            {
                ParameterName = "FECHA_OBTENCION",
                SqlDbType = SqlDbType.Date,
                Direction = ParameterDirection.Input,
                Value = pprof.FechaObtencion
            };
            parametros[5] = new SqlParameter
            {
                ParameterName = "EVIDENCIA",
                SqlDbType = SqlDbType.VarChar,
                Size = 50,
                Direction = ParameterDirection.Input,
                Value = pprof.Evidencia
            };
            string sentencia = @"update PerfilProfe set F_Profe = @ID_PROFESOR, F_Grado = @ID_GRADO, Estado = @ESTADO, FechaObtencion = @FECHA_OBTENCION , Evidencia = @EVIDENCIA  where F_Profe = @ID_PROFESOR;";

            Boolean salida = false;
            salida = acceso.Modificar_Segura_Parametros(sentencia, acceso.AbrirConexion(ref mensaje), ref mensaje, parametros);

            return salida;
        }
    }
}
