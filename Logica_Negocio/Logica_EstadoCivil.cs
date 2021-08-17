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
    public class Logica_EstadoCivil
    {
        public Acceso_Datos acceso = new Acceso_Datos(@"Data Source = DESKTOP-3SLJB0F; Initial Catalog = Bitacora2021LabsUTP; Integrated Security = true;");
        public List<ClassEstadoCivil> DevuelveporIDEstadoCivil(ref string mensaje_salida)
        {
            SqlConnection conabiertatemporal = null;
            string consulta = "select * from EstadoCivil";
            conabiertatemporal = acceso.AbrirConexion(ref mensaje_salida);
            SqlDataReader atrapardatos = null;
            atrapardatos = acceso.ConsultasDR(consulta, conabiertatemporal, ref mensaje_salida);
            List<ClassEstadoCivil> ListSalida = new List<ClassEstadoCivil>();
            if (atrapardatos != null)
            {
                while (atrapardatos.Read())
                {
                    ListSalida.Add(new ClassEstadoCivil
                    {
                        ID_EstadoCivil = Convert.ToInt32(atrapardatos[0]),
                        Estado = atrapardatos[1].ToString(),
                        Extra = atrapardatos[2].ToString()
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
        }
    }
}
