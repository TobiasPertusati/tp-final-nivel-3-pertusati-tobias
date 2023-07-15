using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Dominio;

namespace Interfaz_Web
{
    public partial class MiPerfil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtEmail.ReadOnly = true;
            try
            {
                if (!IsPostBack)
                {
                    User user = (User)Session["usuario"];
                    if (user != null)
                    {
                        txtEmail.Text = user.Email;
                        txtNombre.Text = user.Nombre;
                        txtApellido.Text = user.Apellido;
                        if (!string.IsNullOrEmpty(user.ImagenPerfil) )
                            imgPerfil.ImageUrl = "~/Images/" + user.ImagenPerfil;
                    }
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            userNegocio usernegocio = new userNegocio();
            try
            {
                User user = (User)Session["usuario"];

                if (txtImagen.PostedFile.FileName != "")
                {
                    string ruta = Server.MapPath("./Images/");
                    string nombreImg = "perfil-" + user.Id + ".jpg";
                    txtImagen.PostedFile.SaveAs(ruta + nombreImg);
                    user.ImagenPerfil = nombreImg;
                }
                user.Nombre = txtNombre.Text;
                user.Apellido = txtApellido.Text;

                usernegocio.actualizar(user);

                //Cargar imagen
                Image img = (Image)Master.FindControl("imgPerfil");
                img.ImageUrl = "~/Images/" + user.ImagenPerfil;
                imgPerfil.ImageUrl = "~/Images/" + user.ImagenPerfil;

            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }
    }
}