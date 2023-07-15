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
    public partial class ListadoArticulos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!(Seguridad.esAdmin(Session["usuario"])))
                {
                    Session.Add("error", "Se requieren permisos de admin para acceder a esta pantalla");
                    Response.Redirect("Error.aspx", false);
                }         
                if (!IsPostBack) 
                {
                    articuloNegocio negocio = new articuloNegocio();
                    Session.Add("listaArticulos", negocio.listarArticulos());
                    List<Articulo> articulos = (List<Articulo>)Session["listaArticulos"];
                    Session.Add("ultimoId", articulos.Last().Id);
                    dgvArticulos.DataSource = articulos;
                    dgvArticulos.DataBind();
                }

            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void dgvArticulos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvArticulos.PageIndex = e.NewPageIndex;
            dgvArticulos.DataBind();
        }

        protected void dgvArticulos_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = dgvArticulos.SelectedDataKey.Value.ToString();
            Response.Redirect("FormularioArticulo.aspx?id=" + id);
        }
    }
}