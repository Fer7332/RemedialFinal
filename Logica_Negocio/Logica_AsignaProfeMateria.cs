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
    public class Logica_AsignaProfeMateria
    {
        public Acceso_Datos acceso = new Acceso_Datos(@"Data Source = DESKTOP-3SLJB0F; Initial Catalog = Bitacora2021LabsUTP; Integrated Security = true;");
        SqlCommand sqlc = null;
        public Boolean Inserta_AsignaProfeMateria(ClassAsignarProfesorMateria APMC, ref string mensaje_salida)
        {
            SqlParameter[] parametros = new SqlParameter[5];
            parametros[0] = new SqlParameter
            {
                ParameterName = "ID_ASIGNAPROFEMATERIA",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                Value = APMC.ID_AsignaPro
            };
            parametros[1] = new SqlParameter
            {
                ParameterName = "FID_PROFE",
                SqlDbType = SqlDbType.SmallInt,
                Direction = ParameterDirection.Input,
                Value = APMC.ID_Profe
            };
            parametros[2] = new SqlParameter
            {
                ParameterName = "FID_MATERIA",
                SqlDbType = SqlDbType.SmallInt,
                Direction = ParameterDirection.Input,
                Value = APMC.ID_Materia
            };
            parametros[3] = new SqlParameter
            {
                ParameterName = "FID_GRUPOCUATRI",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                Value = APMC.ID_GrupoCuatri
            };
            parametros[4] = new SqlParameter
            {
                ParameterName = "EXTRA",
                SqlDbType = SqlDbType.VarChar,
                Size = 50,
                Direction = ParameterDirection.Input,
                Value = APMC.Extra
            };
            string sentencia = @"Insert into AsignaProfeMateriaCuatri values(@FID_PROFE, @FID_MATERIA, @FID_GRUPOCUATRI, @EXTRA);";
            Boolean salida = false;
            salida = acceso.Modificar_Segura_Parametros(sentencia, acceso.AbrirConexion(ref mensaje_salida), ref mensaje_salida, parametros);
            return salida;
        } //INSERTAR

        public List<ClassAsignarProfesorMateria> DevuelveporIDAsigProfeMateria(ref string mensaje_salida)
        {
            SqlConnection conabiertatemporal = null;
            string consulta = "select * from AsignaProfeMateriaCuatri";
            conabiertatemporal = acceso.AbrirConexion(ref mensaje_salida);
            SqlDataReader atrapardatos = null;
            atrapardatos = acceso.ConsultasDR(consulta, conabiertatemporal, ref mensaje_salida);
            List<ClassAsignarProfesorMateria> ListSalida = new List<ClassAsignarProfesorMateria>();
            if (atrapardatos != null)
            {
                while (atrapardatos.Read())
                {
                    ListSalida.Add(new ClassAsignarProfesorMateria
                    {
                        ID_AsignaPro = Convert.ToInt32(atrapardatos[0]),
                        ID_Profe = Convert.ToInt32(atrapardatos[1]),
                        ID_Materia = Convert.ToInt32(atrapardatos[2]),
                        ID_GrupoCuatri = Convert.ToInt32(atrapardatos[3]),
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

        public DataTable ConsultaGeneralAsigProfeMateria(ref string mensaje_salida)
        {
            string consulta = @"select (select (Profesor.Nombre)) as Profesor,(select (Profesor.Ap_pat)) as Apellido, (select(Materia.NombeMateria)) as Materia, (select(Grupo.Letra)) as Grupo  from AsignaProfeMateriaCuatri, Profesor, Materia, Grupo where Profesor.ID_Profe = AsignaProfeMateriaCuatri.F_Profe and Materia.Id_Materia = AsignaProfeMateriaCuatri.F_Materia and Grupo.Id_grupo = AsignaProfeMateriaCuatri.F_GrupoCuatri";
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

        public Boolean EliminarAsignaProfeMateria(ClassAsignarProfesorMateria asig, ref string mensaje)
        {
            SqlParameter[] parametros = new SqlParameter[1];
            parametros[0] = new SqlParameter
            {
                ParameterName = "FID_PROFE",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                Value = asig.ID_Profe
            };
            string sentencia = @"DELETE FROM AsignaProfeMateriaCuatri WHERE F_Profe = @FID_PROFE;";

            Boolean salida = false;

            salida = acceso.Modificar_Segura_Parametros(sentencia, acceso.AbrirConexion(ref mensaje), ref mensaje, parametros);

            return salida;
        }

        public Boolean ActualizarProfeMateria (ClassAsignarProfesorMateria promat, ref string mensaje)
        {
            SqlParameter[] parametros = new SqlParameter[5];
            parametros[0] = new SqlParameter
            {
                ParameterName = "ID_ASIGNAPROFEMATERIA",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                Value = promat.ID_AsignaPro
            };
            parametros[1] = new SqlParameter
            {
                ParameterName = "FID_PROFE",
                SqlDbType = SqlDbType.SmallInt,
                Direction = ParameterDirection.Input,
                Value = promat.ID_Profe
            };
            parametros[2] = new SqlParameter
            {
                ParameterName = "FID_MATERIA",
                SqlDbType = SqlDbType.SmallInt,
                Direction = ParameterDirection.Input,
                Value = promat.ID_Materia
            };
            parametros[3] = new SqlParameter
            {
                ParameterName = "FID_GRUPOCUATRI",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                Value = promat.ID_GrupoCuatri
            };
            parametros[4] = new SqlParameter
            {
                ParameterName = "EXTRA",
                SqlDbType = SqlDbType.VarChar,
                Size = 50,
                Direction = ParameterDirection.Input,
                Value = promat.Extra
            };
            string sentencia = @"update AsignaProfeMateriaCuatri set F_Profe = @FID_PROFE, F_Materia = @FID_MATERIA, F_GrupoCuatri = @FID_GRUPOCUATRI, Extra = @EXTRA where Id_AsignaPro = @ID_ASIGNAPROFEMATERIA;";

            Boolean salida = false;
            salida = acceso.Modificar_Segura_Parametros(sentencia, acceso.AbrirConexion(ref mensaje), ref mensaje, parametros);

            return salida;
        }
    }
}
