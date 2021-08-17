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
    public partial class PaginaProfesor : System.Web.UI.Page
    {
        Logica_Profesor logpro = null;
        Logica_EstadoCivil logedci = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                logpro = new Logica_Profesor();                
                Session["logpro"] = logpro;
                logedci = new Logica_EstadoCivil();
                Session["logedci"] = logedci;
                MostrarEstado();
                MostrarProfesor();
            }
            else
            {
                logpro = (Logica_Profesor)Session["logpro"];
                logedci = (Logica_EstadoCivil)Session["logedci"];

            }
            
        }

        public void MostrarEstado()
        {
            List<ClassEstadoCivil> lista = null;
            string mensaje = "";
            lista = logedci.DevuelveporIDEstadoCivil(ref mensaje);
            labaux.Text = mensaje;
            if (lista != null)
            {
                DropDownList1.Items.Clear();
                foreach (ClassEstadoCivil m in lista)
                {
                    DropDownList1.Items.Add(new ListItem(m.Estado, m.ID_EstadoCivil.ToString()));
                   
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
                DropDownList2.Items.Clear();
                DropDownList3.Items.Clear();
                foreach (ClassProfesor m in lista)
                {
                    DropDownList2.Items.Add(new ListItem(m.Nombre, m.ID_Profesor.ToString()));
                    DropDownList3.Items.Add(new ListItem(m.Nombre, m.ID_Profesor.ToString()));

                }
            }
        }
        protected void btninsertar_Click(object sender, EventArgs e)
        {
            ClassProfesor temp = new ClassProfesor
            {
                ID_Profesor = 0,
                RegistroEmpleado = Convert.ToInt32(txtregistroempl.Text),
                Nombre = txtnombre.Text,
                ApellidoPaterno = txtap.Text,
                ApellidoMaterno = txtam.Text,
                Genero = txtgenero.Text,
                Categoria = txtcategoria.Text,
                Correo = txtcorreo.Text,
                Celular = txtcelular.Text,
                Estado_Civil = Convert.ToInt32(DropDownList1.SelectedValue)
            };
            
            string mensaje = " ";
            logpro.Inserta_Profe(temp, ref mensaje);
            labaux.Text = mensaje;
            MostrarProfesor();
            txtregistroempl.Text = "";
            txtnombre.Text = "";
            txtap.Text = "";
            txtam.Text = "";
            txtgenero.Text = "";
            txtcategoria.Text = "";
            txtcorreo.Text = "";
            txtcelular.Text = "";

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            MostrarProfesor();
        }

        protected void btnmodificar_Click(object sender, EventArgs e)
        {
            ClassProfesor temp = new ClassProfesor
            {
                ID_Profesor = Convert.ToInt32(DropDownList3.SelectedValue),
                RegistroEmpleado = Convert.ToInt32(txtregistroempl.Text),
                Nombre = txtnombre.Text,
                ApellidoPaterno = txtap.Text,
                ApellidoMaterno = txtam.Text,
                Genero = txtgenero.Text,
                Categoria = txtcategoria.Text,
                Correo = txtcorreo.Text,
                Celular = txtcelular.Text,
                Estado_Civil = Convert.ToInt32(DropDownList1.SelectedValue)
            };

            string mensaje = " ";
            logpro.ActualizarProfesor(temp, ref mensaje);
            labaux.Text = mensaje;
            MostrarProfesor();
            txtregistroempl.Text = "";
            txtnombre.Text = "";
            txtap.Text = "";
            txtam.Text = "";
            txtgenero.Text = "";
            txtcategoria.Text = "";
            txtcorreo.Text = "";
            txtcelular.Text = "";
        }

        protected void btneliminar_Click(object sender, EventArgs e)
        {
            ClassProfesor temp = new ClassProfesor
            {
                ID_Profesor = Convert.ToInt32(DropDownList2.SelectedValue),
                Nombre = txtnombre.Text
            };

            string mensaje = " ";
            logpro.EliminarProfesor(temp, ref mensaje);
            MostrarProfesor();
            
        }

        protected void btnmostrar_Click(object sender, EventArgs e)
        {

            //int esta = Convert.ToInt32(DropDownList1.SelectedValue);
            string mensaje = "";
            GridView1.DataSource = logpro.ConsultaGeneralProfesor(ref mensaje);
            GridView1.DataBind();
            labaux.Text = mensaje;
        }

        protected void btnasignaprof_Click(object sender, EventArgs e)
        {
            Response.Redirect("PaginaProfeaMateria.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("PaginaGradoEspecialidad.aspx");
        }

        protected void btnasignaprof_Click1(object sender, EventArgs e)
        {
            Response.Redirect("PaginaProfesoraMateria.aspx");
        }
    }
}