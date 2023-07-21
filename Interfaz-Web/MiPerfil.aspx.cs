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
                        if (!string.IsNullOrEmpty(user.ImagenPerfil))
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
            Helper helper = new Helper();
            try
            {
                Page.Validate();
                if (!Page.IsValid)
                    return;

                if (helper.estaVacio(txtApellido.Text) || helper.estaVacio(txtNombre.Text))
                {
                    lbFaltanCampos.Visible = true;
                    return;
                }
                // VALIDO QUE LOS DATOS NO SEAN LOS MISMO PARA NO IR A LA BASE DE DATOS AL PP
                User user = (User)Session["usuario"];
                if (user.Nombre == txtNombre.Text && user.Apellido == txtApellido.Text && txtImagen.PostedFile.FileName == "")
                    return;

                // Guardo la imagen
                if (txtImagen.PostedFile.FileName != "")
                {
                    string ruta = Server.MapPath("./Images/");
                    string nombreImg = "perfil-" + user.Id + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute + ".jpg";
                    txtImagen.PostedFile.SaveAs(ruta + nombreImg);
                    user.ImagenPerfil = nombreImg;
                }
                user.Nombre = txtNombre.Text;
                user.Apellido = txtApellido.Text;

                userNegocio usernegocio = new userNegocio();
                usernegocio.actualizar(user);

                //Cargar imagen y nombre en perfil
                Image img = (Image)Master.FindControl("imgPerfil");
                img.ImageUrl = "~/Images/" + user.ImagenPerfil;
                imgPerfil.ImageUrl = "~/Images/" + user.ImagenPerfil;
                Label lb = (Label)Master.FindControl("lbNombre");
                lb.Text = user.Nombre;

            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }
    }
}