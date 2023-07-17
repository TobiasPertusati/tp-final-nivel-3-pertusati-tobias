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
        public bool FiltroAvanzado { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!(Seguridad.esAdmin(Session["usuario"])))
                {
                    Session.Add("error", "Se requieren permisos de admin para acceder a esta pantalla");
                    Response.Redirect("Error.aspx", false);
                }         
                FiltroAvanzado = chkFiltroAvanzado.Checked;
                if (!IsPostBack) 
                {
                    articuloNegocio negocio = new articuloNegocio();
                    List<Articulo> articulos = negocio.listarArticulos();
                    Session.Add("listaArticulos", articulos);
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

        protected void txtFiltroRapido_TextChanged(object sender, EventArgs e)
        {
            string texto = txtFiltroRapido.Text.ToUpper();
            List<Articulo> lista_filtrada = ((List<Articulo>)Session["listaArticulos"]).FindAll(x => x.Codigo.ToUpper().Contains(texto) 
            || x.Nombre.ToUpper().Contains(texto) || x.Marca.Descripcion.ToUpper().Contains(texto) || x.Categoria.Descripcion.ToUpper().Contains(texto));
            dgvArticulos.DataSource = lista_filtrada;
            dgvArticulos.DataBind();
        }

        protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
        {
            txtFiltroRapido.Text = "";
            dgvArticulos.DataSource = (List<Articulo>)Session["listaArticulos"];
            dgvArticulos.DataBind();
        }

        protected void chkFiltroAvanzado_CheckedChanged(object sender, EventArgs e)
        {
            FiltroAvanzado = chkFiltroAvanzado.Checked;
            txtFiltroRapido.Enabled = !FiltroAvanzado;
        }

        protected void ddlCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCriterio.Items.Clear();
            if (ddlCampo.SelectedItem.ToString() == "Precio")
            {
                ddlCriterio.Items.Add("Mayor a");
                ddlCriterio.Items.Add("Menor a");
                ddlCriterio.Items.Add("Igual a");
            }
            else
            {
                ddlCriterio.Items.Add("Empieza con");
                ddlCriterio.Items.Add("Termina con");
                ddlCriterio.Items.Add("Contiene");
            }
        }

        protected void btnBusqueda_Click(object sender, EventArgs e)
        {
            articuloNegocio negocio = new articuloNegocio();
            try
            {
                //Validar (FILTRO) que cuando campo esta en precio no se escriban letras ni letras raras
                // Validar que no vayan empty el ddlCriterio cuando se apreta el boton
                string campo = ddlCampo.SelectedItem.ToString();
                string criterio = ddlCriterio.SelectedItem.ToString();
                string filtro = txtFiltroAvanzado.Text;
                dgvArticulos.DataSource = negocio.listarFiltrado(campo, criterio, filtro);
                dgvArticulos.DataBind();

            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }
    }
}