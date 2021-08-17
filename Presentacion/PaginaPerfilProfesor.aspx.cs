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
    public partial class PaginaPerfilProfesor : System.Web.UI.Page
    {
        Logica_Profesor logpro = null;
        Logica_PerfilProfesor logpprof = null;
        Logica_GradoEspecialidad loggraesp = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                logpro = new Logica_Profesor();
                Session["logpro"] = logpro;
                logpprof = new Logica_PerfilProfesor();
                Session["logpprof"] = logpprof;
                loggraesp = new Logica_GradoEspecialidad();
                Session["loggraesp"] = loggraesp;
                MostrarGradoEsp();
                MostrarProfesor();
            }
            else
            {
                logpro = (Logica_Profesor)Session["logpro"];
                logpprof = (Logica_PerfilProfesor)Session["logpprof"];
                loggraesp = (Logica_GradoEspecialidad)Session["loggraesp"];

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
                DropDownList3.Items.Clear();
                foreach (ClassProfesor m in lista)
                {
                    DropDownList1.Items.Add(new ListItem(m.Nombre, m.ID_Profesor.ToString()));
                    DropDownList3.Items.Add(new ListItem(m.Nombre, m.ID_Profesor.ToString()));
                    
                }
            }
        }

        public void MostrarGradoEsp()
        {
            List<ClassGradoEspecialidad> lista = null;
            string mensaje = "";
            lista = loggraesp.DevuelveporIDGradoEsp(ref mensaje);
            labaux.Text = mensaje;
            if (lista != null)
            {
                
                DropDownList2.Items.Clear();
                foreach (ClassGradoEspecialidad m in lista)
                {                    
                    DropDownList2.Items.Add(new ListItem(m.Titulo, m.ID_Grado.ToString()));
                }
            }
        }

        


        protected void btninsertar_Click(object sender, EventArgs e)
        {
            ClassPerfilProfesor temp = new ClassPerfilProfesor
            {
                ID_Perfil = 0,
                ID_Profe = Convert.ToInt32(DropDownList1.SelectedValue),
                ID_Grado = Convert.ToInt32(DropDownList2.SelectedValue),
                Estado = txtestado.Text,
                FechaObtencion = Convert.ToDateTime( txtfecha.Text),
                Evidencia = txtevidencia.Text

            };
            string mensaje = " ";
            logpprof.Inserta_PerfilProfe(temp, ref mensaje);
            labaux.Text = mensaje;
            MostrarProfesor();
            DropDownList1.Items.Clear();
            DropDownList2.Items.Clear();
            txtestado.Text = "";
            txtfecha.Text = "";
            txtevidencia.Text = "";            

        }

        protected void btnmodificar_Click(object sender, EventArgs e)
        {
            ClassPerfilProfesor temp = new ClassPerfilProfesor
            {
                //ID_Perfil = Convert.ToInt32(DropDownList4.SelectedValue),
                ID_Profe = Convert.ToInt32(DropDownList1.SelectedValue),
                ID_Grado = Convert.ToInt32(DropDownList2.SelectedValue),
                Estado = txtestado.Text,
                FechaObtencion = Convert.ToDateTime(txtfecha.Text),
                Evidencia = txtevidencia.Text

            };
            string mensaje = " ";
            logpprof.ActualizarPerfilProfe(temp, ref mensaje);
            labaux.Text = mensaje;
            MostrarProfesor();
            DropDownList1.Items.Clear();
            DropDownList2.Items.Clear();
            txtestado.Text = "";
            txtfecha.Text = "";
            txtevidencia.Text = "";
        }

        protected void btneliminar_Click(object sender, EventArgs e)
        {
            ClassPerfilProfesor temp = new ClassPerfilProfesor
            {
                
                ID_Profe = Convert.ToInt32(DropDownList3.SelectedValue)

            };

            string mensaje = " ";
            logpprof.EliminarPerfilProfesor(temp, ref mensaje);
            MostrarProfesor();
        }

        protected void btnmostrar_Click(object sender, EventArgs e)
        {
           
            string mensaje = "";
            GridView1.DataSource = logpprof.ConsultaGeneralPerfilProfesor(ref mensaje);
            GridView1.DataBind();
            labaux.Text = mensaje;
        }

        protected void btnactdatos_Click(object sender, EventArgs e)
        {
            MostrarProfesor();
        }
    }
}