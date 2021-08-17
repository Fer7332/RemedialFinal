using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Objetos
{
    public class ClassPerfilProfesor
    {
        public int ID_Perfil { set; get; }
        public int ID_Profe { set; get; }
        public int ID_Grado { set; get; }
        public string Estado { set; get; }
        public DateTime FechaObtencion { set; get; }
        public string Evidencia { set; get; }
    }
}
