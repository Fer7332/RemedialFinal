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
    public class Logica_Profesor
    {
        public Acceso_Datos acceso = new Acceso_Datos(@"Data Source = DESKTOP-3SLJB0F; Initial Catalog = Bitacora2021LabsUTP; Integrated Security = true;");
       
        SqlCommand sqlc = null;
        public Boolean Inserta_Profe(ClassProfesor profe, ref string mensaje_salida)
        {
            SqlParameter[] parametros = new SqlParameter[10];
            parametros[0] = new SqlParameter
            {
                ParameterName = "ID_PROFESOR",
                SqlDbType = SqlDbType.SmallInt,
                Direction = ParameterDirection.Input,
                Value = profe.ID_Profesor
            };
            parametros[1] = new SqlParameter
            {
                ParameterName = "REGISTROEMPLEADO",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                Value = profe.RegistroEmpleado
            };
            parametros[2] = new SqlParameter
            {
                ParameterName = "NOMBRE",
                SqlDbType = SqlDbType.VarChar,
                Size = 150,
                Direction = ParameterDirection.Input,
                Value = profe.Nombre
            };
            parametros[3] = new SqlParameter
            {
                ParameterName = "APELLIDO_PARTERNO",
                SqlDbType = SqlDbType.VarChar,
                Size = 100,
                Direction = ParameterDirection.Input,
                Value = profe.ApellidoPaterno
            };
            parametros[4] = new SqlParameter
            {
                ParameterName = "APELLIDO_MATERNO",
                SqlDbType = SqlDbType.VarChar,
                Size = 100,
                Direction = ParameterDirection.Input,
                Value = profe.ApellidoMaterno
            };
            parametros[5] = new SqlParameter
            {
                ParameterName = "GENERO",
                SqlDbType = SqlDbType.VarChar,
                Size = 10,
                Direction = ParameterDirection.Input,
                Value = profe.Genero
            };
            parametros[6] = new SqlParameter
            {
                ParameterName = "CATEGORIA",
                SqlDbType = SqlDbType.VarChar,
                Size = 5,
                Direction = ParameterDirection.Input,
                Value = profe.Categoria
            };
            parametros[7] = new SqlParameter
            {
                ParameterName = "CORREO",
                SqlDbType = SqlDbType.VarChar,
                Size = 200,
                Direction = ParameterDirection.Input,
                Value = profe.Correo
            };
            parametros[8] = new SqlParameter
            {
                ParameterName = "CELULAR",
                SqlDbType = SqlDbType.VarChar,
                Size = 20,
                Direction = ParameterDirection.Input,
                Value = profe.Celular
            };
            parametros[9] = new SqlParameter
            {
                ParameterName = "FID_ESTADOCIVIL",
                SqlDbType = SqlDbType.TinyInt,
                Direction = ParameterDirection.Input,
                Value = profe.Estado_Civil
            };
            string sentencia = "Insert into Profesor values (@REGISTROEMPLEADO, @NOMBRE, @APELLIDO_PARTERNO, @APELLIDO_MATERNO, @GENERO, @CATEGORIA, @CORREO, @CELULAR, @FID_ESTADOCIVIL);";
            Boolean salida = false;
            salida = acceso.Modificar_Segura_Parametros(sentencia, acceso.AbrirConexion(ref mensaje_salida), ref mensaje_salida, parametros);
            return salida;
        } //INSERTAR

        public List<ClassProfesor> DevuelveporIDProfesor(ref string mensaje_salida)
        {
            SqlConnection conabiertatemporal = null;
            string consulta = "select * from Profesor";
            conabiertatemporal = acceso.AbrirConexion(ref mensaje_salida);
            SqlDataReader atrapardatos = null;
            atrapardatos = acceso.ConsultasDR(consulta, conabiertatemporal, ref mensaje_salida);
            List<ClassProfesor> ListSalida = new List<ClassProfesor>();
            if (atrapardatos != null)
            {
                while (atrapardatos.Read())
                {
                    ListSalida.Add(new ClassProfesor
                    {
                        ID_Profesor = Convert.ToInt32(atrapardatos[0]),
                        RegistroEmpleado = Convert.ToInt32(atrapardatos[1]),
                        Nombre = atrapardatos[2].ToString(),
                        ApellidoPaterno = atrapardatos[3].ToString(),
                        ApellidoMaterno = atrapardatos[4].ToString(),
                        Genero = atrapardatos[5].ToString(),
                        Categoria = atrapardatos[6].ToString(),
                        Correo = atrapardatos[7].ToString(),
                        Celular = atrapardatos[8].ToString(),
                        Estado_Civil = Convert.ToInt32(atrapardatos[9])
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

        public DataTable ConsultaGeneralProfesor( ref string mensaje_salida)
        {
            //@"select RegistroEmpleado, Nombre, Ap_pat, Ap_Mat, Genero, Categoria, Correo, Celular , (select Estado from EstadoCivil where Id_Edo = "+ estado +") as Estado from Profesor";
            string consulta = @"select RegistroEmpleado, Nombre, Ap_pat as Primer_Apellido, Ap_Mat as Segundo_Apellido, Genero, Categoria, Correo, Celular ,(select (EstadoCivil.Estado )) as Estado from Profesor, EstadoCivil  where Profesor.F_EdoCivil = EstadoCivil.Id_Edo";
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

        public Boolean EliminarProfesor(ClassProfesor profe, ref string mensaje)
        {
            SqlParameter[] parametros = new SqlParameter[1];
            parametros[0] = new SqlParameter
            {
                ParameterName = "ID_PROFESOR",
                SqlDbType = SqlDbType.SmallInt,
                Direction = ParameterDirection.Input,
                Value = profe.ID_Profesor
            };
            
            string sentencia = @"DELETE FROM Profesor WHERE ID_Profe = @ID_PROFESOR;";
            Boolean salida = false;
            
            salida = acceso.Modificar_Segura_Parametros(sentencia, acceso.AbrirConexion(ref mensaje), ref mensaje, parametros);
            return salida;
        }

        public Boolean ActualizarProfesor(ClassProfesor profe, ref string mensaje)
        {
            SqlParameter[] parametros = new SqlParameter[10];
            parametros[0] = new SqlParameter
            {
                ParameterName = "ID_PROFESOR",
                SqlDbType = SqlDbType.SmallInt,
                Direction = ParameterDirection.Input,
                Value = profe.ID_Profesor
            };
            parametros[1] = new SqlParameter
            {
                ParameterName = "REGISTROEMPLEADO",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                Value = profe.RegistroEmpleado
            };
            parametros[2] = new SqlParameter
            {
                ParameterName = "NOMBRE",
                SqlDbType = SqlDbType.VarChar,
                Size = 150,
                Direction = ParameterDirection.Input,
                Value = profe.Nombre
            };
            parametros[3] = new SqlParameter
            {
                ParameterName = "APELLIDO_PARTERNO",
                SqlDbType = SqlDbType.VarChar,
                Size = 100,
                Direction = ParameterDirection.Input,
                Value = profe.ApellidoPaterno
            };
            parametros[4] = new SqlParameter
            {
                ParameterName = "APELLIDO_MATERNO",
                SqlDbType = SqlDbType.VarChar,
                Size = 100,
                Direction = ParameterDirection.Input,
                Value = profe.ApellidoMaterno
            };
            parametros[5] = new SqlParameter
            {
                ParameterName = "GENERO",
                SqlDbType = SqlDbType.VarChar,
                Size = 10,
                Direction = ParameterDirection.Input,
                Value = profe.Genero
            };
            parametros[6] = new SqlParameter
            {
                ParameterName = "CATEGORIA",
                SqlDbType = SqlDbType.VarChar,
                Size = 5,
                Direction = ParameterDirection.Input,
                Value = profe.Categoria
            };
            parametros[7] = new SqlParameter
            {
                ParameterName = "CORREO",
                SqlDbType = SqlDbType.VarChar,
                Size = 200,
                Direction = ParameterDirection.Input,
                Value = profe.Correo
            };
            parametros[8] = new SqlParameter
            {
                ParameterName = "CELULAR",
                SqlDbType = SqlDbType.VarChar,
                Size = 20,
                Direction = ParameterDirection.Input,
                Value = profe.Celular
            };
            parametros[9] = new SqlParameter
            {
                ParameterName = "FID_ESTADOCIVIL",
                SqlDbType = SqlDbType.TinyInt,
                Direction = ParameterDirection.Input,
                Value = profe.Estado_Civil
            };
            string sentencia = @"update Profesor set RegistroEmpleado = @REGISTROEMPLEADO, Nombre = @NOMBRE, Ap_pat = @APELLIDO_PARTERNO, Ap_Mat = @APELLIDO_MATERNO, Genero= @GENERO, Categoria = @CATEGORIA, Correo = @CORREO, Celular = @CELULAR, F_EdoCivil = @FID_ESTADOCIVIL where ID_Profe = @ID_PROFESOR;";

            Boolean salida = false;
            salida = acceso.Modificar_Segura_Parametros(sentencia, acceso.AbrirConexion(ref mensaje), ref mensaje, parametros);

            return salida;
        }
    }
}
