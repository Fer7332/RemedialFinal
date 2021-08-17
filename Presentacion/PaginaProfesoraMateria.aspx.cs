using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Objetos;
using Logica_Negocio;

namespace Presentacion
{
    public partial class PaginaProfesoraMateria : System.Web.UI.Page
    {
        Logica_AsignaProfeMateria LOGPROMAT = null;
        Logica_Profesor logpro = null;
        Logica_Materia LOGMAT = null;
        LogicaSalon logsal = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                logpro = new Logica_Profesor();
                Session["logpro"] = logpro;
                LOGPROMAT = new Logica_AsignaProfeMateria();
                Session["LOGPROMAT"] = LOGPROMAT;
                LOGMAT = new Logica_Materia();
                Session["LOGMAT"] = LOGMAT;
                logsal = new LogicaSalon();
                Session["logsal"] = logsal;
                MostrarProfesor();
                MostrarMATERIA();
                MostrarGrupo();
                MostrarProfesorMateria();
            }
            else
            {
                logpro = (Logica_Profesor)Session["logpro"];
                LOGPROMAT = (Logica_AsignaProfeMateria)Session["LOGPROMAT"];
                LOGMAT = (Logica_Materia)Session["LOGMAT"];
                logsal = (LogicaSalon)Session["logsal"];
            }   

        }
        public void MostrarMATERIA()
        {
            List<ClassMateria> lista = null;
            string mensaje = "";
            lista = LOGMAT.DevuelveporIDMateria(ref mensaje);
            labaux.Text = mensaje;
            if (lista != null)
            {
                DropDownList2.Items.Clear();
                foreach (ClassMateria m in lista)
                {
                    DropDownList2.Items.Add(new ListItem(m.Nombre_Materia, m.ID_Materia.ToString()));
                }
            }
        }

        public void MostrarGrupo()
        {
            List<Salon> lista = null;
            string mensaje = "";
            lista = logsal.DevuelveporIDSalon(ref mensaje);
            labaux.Text = mensaje;
            if (lista != null)
            {
                DropDownList2.Items.Clear();
                foreach (Salon m in lista)
                {
                    DropDownList3.Items.Add(new ListItem(m.grupo, m.Id_grupo.ToString()));
                }
            }
        }

        public void MostrarProfesor()
        {
            List<ClassProfesor> lista = null;
            string mensaje = "";
            lista = logpro.DevuelveporIDProfesor(ref mensaje);
            labaux.Text = mensaje;
            if (lista != null)
            {

                DropDownList1.Items.Clear();
                DropDownList4.Items.Clear();
                
                foreach (ClassProfesor m in lista)
                {
                    DropDownList1.Items.Add(new ListItem(m.Nombre, m.ID_Profesor.ToString()));
                    DropDownList4.Items.Add(new ListItem(m.Nombre, m.ID_Profesor.ToString()));
                   

                }
            }
        }

        public void MostrarProfesorMateria()
        {
            List<ClassAsignarProfesorMateria> lista = null;
            string mensaje = "";
            lista = LOGPROMAT.DevuelveporIDAsigProfeMateria(ref mensaje);
            labaux.Text = mensaje;
            if (lista != null)
            {
                DropDownList5.Items.Clear();
                foreach (ClassAsignarProfesorMateria m in lista)
                {
                    DropDownList5.Items.Add(new ListItem(m.ID_AsignaPro.ToString(), m.ID_AsignaPro.ToString()));
                }
            }
        }

        protected void btninsertar_Click(object sender, EventArgs e)
        {
            ClassAsignarProfesorMateria temp = new ClassAsignarProfesorMateria
            {
                ID_AsignaPro = 0,
                ID_Profe = Convert.ToInt32(DropDownList1.SelectedValue),
                ID_Materia = Convert.ToInt32(DropDownList2.SelectedValue),
                ID_GrupoCuatri = Convert.ToInt32(DropDownList3.SelectedValue),
                Extra = TextBox1.Text
            };
            string mensaje = " ";
            LOGPROMAT.Inserta_AsignaProfeMateria(temp, ref mensaje);
            labaux.Text = mensaje;           
            MostrarProfesor();

            DropDownList1.Items.Clear();
            DropDownList2.Items.Clear();
            DropDownList3.Items.Clear();
            TextBox1.Text = "";
        }

        protected void btnmodificar_Click(object sender, EventArgs e)
        {
            ClassAsignarProfesorMateria TEM = new ClassAsignarProfesorMateria
            {
                ID_AsignaPro = Convert.ToInt32(DropDownList5.SelectedValue),
                ID_Profe = Convert.ToInt32(DropDownList1.SelectedValue),
                ID_Materia = Convert.ToInt32(DropDownList2.SelectedValue),
                ID_GrupoCuatri = Convert.ToInt32(DropDownList3.SelectedValue),
                Extra = TextBox1.Text
            };
            string mensaje = " ";
            LOGPROMAT.ActualizarProfeMateria(TEM, ref mensaje);
            labaux.Text = mensaje;
            MostrarProfesor();
            MostrarMATERIA();
            DropDownList1.Items.Clear();
            DropDownList2.Items.Clear();
            DropDownList3.Items.Clear();
            DropDownList5.Items.Clear();
            TextBox1.Text = "";
        }

        protected void btneliminar_Click(object sender, EventArgs e)
        {
            ClassAsignarProfesorMateria temp = new ClassAsignarProfesorMateria
            {

                ID_Profe = Convert.ToInt32(DropDownList4.SelectedValue)

            };

            string mensaje = " ";
            LOGPROMAT.EliminarAsignaProfeMateria(temp, ref mensaje);
            MostrarProfesor();
            MostrarMATERIA();
        }

        protected void btnmostrar_Click(object sender, EventArgs e)
        {

            string mensaje = "";
            GridView1.DataSource = LOGPROMAT.ConsultaGeneralAsigProfeMateria(ref mensaje);
            GridView1.DataBind();
            labaux.Text = mensaje;
        }

        protected void btndatos_Click(object sender, EventArgs e)
        {
            MostrarGrupo();
            MostrarMATERIA();
            MostrarProfesor();
            MostrarProfesorMateria();
        }
    }
}