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
            if (!IsPostBack)
            {
                Session.Add("paginaAnterior", "Default.aspx");
            }
        }

        protected void btnRegistrarse_Click(object sender, EventArgs e)
        {
            userNegocio userNegocio = new userNegocio();
            Helper helper = new Helper();
            try
            {
                Page.Validate();
                if (!Page.IsValid)
                    return;
                if (helper.estaVacio(txtEmail.Text) || helper.estaVacio(txtPass.Text) || helper.estaVacio(txtConfirmaPass.Text))
                {
                    lbError.Visible = true;
                    lbError.Text = "¡Completar todos los campos!";
                    return;
                }
                else if (!(txtPass.Text == txtConfirmaPass.Text))
                {
                    lbError.Visible = true;
                    lbError.Text = "¡Las contraseñas deben coincidir!";
                    return;
                }
                else if (userNegocio.existeMail(txtEmail.Text))
                {
                    lbError.Visible = true;
                    lbError.Text = "¡Ya existe una cuenta con ese email!";
                    return;
                }
                User usuario = new User
                {
                    Email = txtEmail.Text,
                    Pass = txtPass.Text
                };
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