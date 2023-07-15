using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;
using static System.Net.WebRequestMethods;

namespace Interfaz_Web
{
    public partial class MainMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!(Page is Default || Page is DetalleArticulo || Page is ArticulosPorMarca || Page is Error || Page is Registro || Page is Login))
                {
                    if (!Seguridad.sesionActiva(Session["usuario"]))
                        Response.Redirect("Login.aspx", false);
                }
                if (Seguridad.sesionActiva(Session["usuario"]))
                {
                    string nombre = ((User)Session["usuario"]).Nombre ?? "Yo";
                    if (nombre.Length < 15)
                        lbNombre.Text = nombre;
                    else
                        lbNombre.Text = "Yo";

                    if (((User)Session["usuario"]).ImagenPerfil == null)
                        imgPerfil.ImageUrl = "https://cdn.pixabay.com/photo/2017/11/10/05/48/user-2935527_1280.png";
                    else
                        imgPerfil.ImageUrl = "~/Images/" + ((User)Session["usuario"]).ImagenPerfil;
                }

            }
            catch (Exception ex)
            {   
                Session.Add("error", ex.ToString());
                Response.Redirect("Error");
            }
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            try
            {
                Session.Clear();
                Response.Redirect("Default.aspx", false);
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error");
            }
        }
    }
}