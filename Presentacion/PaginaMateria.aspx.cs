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
    public partial class PaginaMateria : System.Web.UI.Page
    {
        Logica_Materia logmat = null;
        protected void Page_Load(object sender, EventArgs e)
        {

            if(IsPostBack == false)
            {
                logmat = new Logica_Materia();
                Session["logmat"] = logmat;
                
            }
            else
            {
                logmat = (Logica_Materia)Session["logmat"];
               
            }
            
        }

        protected void btninsertar_Click(object sender, EventArgs e)
        {
            ClassMateria temp = new ClassMateria
            {
                ID_Materia = 0,
                Nombre_Materia = txtnommateria.Text,
                HorasSemana = Convert.ToInt32(txthorasemana.Text),
                Extra = txtextra.Text
            };
            string mensaje = " ";
            logmat.Inserta_Materia(temp, ref mensaje);
            labaux.Text = mensaje;
            MostrarD();
            txtnommateria.Text = "";
            txthorasemana.Text = "";
            txtextra.Text = "";
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            MostrarD();
        }

        public void MostrarD()
        {
            List<ClassMateria> lista = null;
            string mensaje = "";
            lista = logmat.DevuelveporIDMateria(ref mensaje);
            labaux.Text = mensaje;
            if (lista != null)
            {
                DropDownList1.Items.Clear();
                DropDownList2.Items.Clear();
                foreach (ClassMateria m in lista)
                {
                    DropDownList1.Items.Add(new ListItem(m.Nombre_Materia, m.ID_Materia.ToString()));
                    DropDownList2.Items.Add(new ListItem(m.Nombre_Materia, m.ID_Materia.ToString()));
                }          
            }
        }

        protected void btneliminar_Click(object sender, EventArgs e)
        {
            ClassMateria temp = new ClassMateria
            {
                ID_Materia = Convert.ToInt32(DropDownList1.SelectedValue),
                Nombre_Materia = txtnommateria.Text
                
            };

            string mensaje = " ";
            logmat.EliminarMateria(temp, ref mensaje);
            MostrarD();
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void btnmostrar_Click(object sender, EventArgs e)
        {
            string mensaje = "";
            GridView1.DataSource = logmat.ConsultaGeneralMateria(ref mensaje);
            GridView1.DataBind();
            labaux.Text = mensaje;
        }

        protected void btnactualizar_Click(object sender, EventArgs e)
        {
            
            ClassMateria temp = new ClassMateria
            {
                ID_Materia = Convert.ToInt32(DropDownList2.SelectedValue),
                Nombre_Materia = txtnommateria.Text,
                HorasSemana = Convert.ToInt32(txthorasemana.Text),
                Extra = txtextra.Text
            };
            string mensaje = " ";
            logmat.ActualizarMateria(temp, ref mensaje);
            labaux.Text = mensaje;
            MostrarD();
            txtnommateria.Text = "";
            txthorasemana.Text = "";
            txtextra.Text = "";
        }

        protected void btnprofesor_Click(object sender, EventArgs e)
        {
            Response.Redirect("PaginaProfesor.aspx");
        }
    }
}