using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace Interfaz_Web
{
    public partial class Registro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegistrarse_Click(object sender, EventArgs e)
        {
            User usuario = new User();
            userNegocio userNegocio = new userNegocio();
            try
            {
                if (!(txtPass.Text == txtConfirmaPass.Text))
                {
                    lbError.Visible = true;
                    lbError.Text = "¡Las contraseñas no coinciden!";
                    return;
                }
                usuario.Email = txtEmail.Text;
                usuario.Pass = txtPass.Text;
                usuario.Id = userNegocio.insertarNuevo(usuario);
                Session.Add("usuario", usuario);
                Response.Redirect("Default.aspx", false);
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }
    }
}