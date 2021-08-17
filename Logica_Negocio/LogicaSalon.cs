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
    public class LogicaSalon
    {
        public Acceso_Datos acceso = new Acceso_Datos(@"Data Source = DESKTOP-3SLJB0F; Initial Catalog = Bitacora2021LabsUTP; Integrated Security = true;");
        public List<Salon> DevuelveporIDSalon(ref string mensaje_salida)
        {
            SqlConnection conabiertatemporal = null;
            string consulta = "select * from Grupo";
            conabiertatemporal = acceso.AbrirConexion(ref mensaje_salida);
            SqlDataReader atrapardatos = null;
            atrapardatos = acceso.ConsultasDR(consulta, conabiertatemporal, ref mensaje_salida);
            List<Salon> ListSalida = new List<Salon>();
            if (atrapardatos != null)
            {
                while (atrapardatos.Read())
                {
                    ListSalida.Add(new Salon
                    {
                        Id_grupo = Convert.ToInt32(atrapardatos[0]),
                        Grado = Convert.ToInt32(atrapardatos[1]),
                        grupo = atrapardatos[2].ToString(),
                        extra = atrapardatos[3].ToString()
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
