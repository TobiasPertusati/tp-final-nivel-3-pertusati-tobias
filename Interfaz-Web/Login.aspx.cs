using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Interfaz_Web
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            userNegocio userNegocio = new userNegocio();
            User user = new User();
            try
            {
                user.Email = txtEmail.Text;
                user.Pass = txtPass.Text;

                if (userNegocio.login(user))
                {
                    Session.Add("usuario", user);
                    Response.Redirect("Default.aspx", false);
                }
                else
                {
                    Session.Add("error", "Usuario o contraseña incorrecta");
                    Response.Redirect("Error.aspx", false);
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }
    }
}