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
    public partial class PaginaGradoEspecialidad : System.Web.UI.Page
    {
        Logica_GradoEspecialidad logesp = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                logesp = new Logica_GradoEspecialidad();
                Session["logesp"] = logesp;

            }
            else
            {
                logesp = (Logica_GradoEspecialidad)Session["logesp"];

            }
        }

        public void MostrarGradoEsp()
        {
            List<ClassGradoEspecialidad> lista = null;
            string mensaje = "";
            lista = logesp.DevuelveporIDGradoEsp(ref mensaje);
            labaux.Text = mensaje;
            if (lista != null)
            {
                DropDownList1.Items.Clear();
                DropDownList2.Items.Clear();
                foreach (ClassGradoEspecialidad m in lista)
                {
                    DropDownList1.Items.Add(new ListItem(m.Titulo, m.ID_Grado.ToString()));
                    DropDownList2.Items.Add(new ListItem(m.Titulo, m.ID_Grado.ToString()));
                }
            }
        }

        protected void btninsertar_Click(object sender, EventArgs e)
        {
            ClassGradoEspecialidad temp = new ClassGradoEspecialidad
            {
                ID_Grado = 0,
                Titulo = txttitulo.Text,
                Institucion = txtinstitucion.Text,
                Pais = txtpais.Text,
                Extra = txtextra.Text
            };
            string mensaje = " ";
            logesp.Inserta_Grado_Especialidad(temp, ref mensaje);
            labaux.Text = mensaje;
            MostrarGradoEsp();
            txttitulo.Text = "";
            txtinstitucion.Text = "";
            txtpais.Text = "";
            txtextra.Text = "";

        }

        protected void btnmodificar_Click(object sender, EventArgs e)
        {
            ClassGradoEspecialidad temp = new ClassGradoEspecialidad
            {
                ID_Grado = Convert.ToInt32(DropDownList2.SelectedValue),
                Titulo = txttitulo.Text,
                Institucion = txtinstitucion.Text,
                Pais = txtpais.Text,
                Extra = txtextra.Text
            };
            string mensaje = " ";
            logesp.ActualizarGradoEsp(temp, ref mensaje);
            labaux.Text = mensaje;
            MostrarGradoEsp();
            txttitulo.Text = "";
            txtinstitucion.Text = "";
            txtpais.Text = "";
            txtextra.Text = "";
        }

        protected void btneliminar_Click(object sender, EventArgs e)
        {
            ClassGradoEspecialidad temp = new ClassGradoEspecialidad
            {
                ID_Grado = Convert.ToInt32(DropDownList1.SelectedValue),
                Titulo = txttitulo.Text
            };
            string mensaje = " ";
            logesp.EliminarGradoEspecialidad(temp, ref mensaje);
            MostrarGradoEsp();
        }

        protected void btnmostrartabla_Click(object sender, EventArgs e)
        {
            string mensaje = "";
            GridView1.DataSource = logesp.ConsultaGeneralGradoEsp(ref mensaje);
            GridView1.DataBind();
            labaux.Text = mensaje;
        }

        protected void btnactudrop_Click(object sender, EventArgs e)
        {
            MostrarGradoEsp();
        }

        protected void btnperfprof_Click(object sender, EventArgs e)
        {
            Response.Redirect("PaginaPerfilProfesor.aspx");
        }
    }
}