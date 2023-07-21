using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text.RegularExpressions;
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
                    //GENERO LA LISTA DE ARTICULOS Y LA BINDEO AL DGV Y AGREGO OTROS PARAMETROS A SESSION
                    articuloNegocio negocio = new articuloNegocio();
                    List<Articulo> articulos = negocio.listarArticulos();

                    Session.Add("listaArticulos", articulos);
                    Session.Add("ultimoId", articulos.Last().Id);
                    Session.Add("BusquedaEjecutada", false);
                    Session.Add("textoFiltro", true);

                    dgvArticulos.DataSource = articulos;
                    dgvArticulos.DataBind();

                    // ME TRAIGO LAS LISTAS DE MARCA Y CATEGORIA PARA ASIGNARLOS AL DDLCRITERIO
                    categoriaNegocio categoriaNegocio = new categoriaNegocio();
                    Session.Add("listaCategorias",categoriaNegocio.listar_categorias());
                    marcaNegocio marcaNegocio = new marcaNegocio();
                    Session.Add("listaMarcas", marcaNegocio.listar_marcas());
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
            if ((bool)Session["BusquedaEjecutada"] == false)
                dgvArticulos.DataSource = (List<Articulo>)Session["listaArticulos"];
            else
                dgvArticulos.DataSource = (List<Articulo>)Session["listaFiltrada"];
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
            if (lista_filtrada.Count > 0)
            {
                Session["BusquedaEjecutada"] = true;
                Session.Add("listaFiltrada", lista_filtrada);
                imgBusquedaNula.Visible = false;
            }
            else
                imgBusquedaNula.Visible = true;
        }

        protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
        {
            txtFiltroRapido.Text = "";
            imgBusquedaNula.Visible = false;
            Session["BusquedaEjecutada"] = false;
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
                Session["textoFiltro"] = true;
            }
            else if (ddlCampo.SelectedItem.ToString() == "Marca")
            {
                ddlCriterio.DataSource = (List<Marca>)Session["listaMarcas"];
                ddlCriterio.DataTextField = "Descripcion";
                ddlCriterio.DataValueField = "Id";
                ddlCriterio.DataBind();
                Session["textoFiltro"] = false;
            }
            else if(ddlCampo.SelectedItem.ToString() == "Categoria")
            {
                ddlCriterio.DataSource = (List<Categoria>)Session["listaCategorias"];
                ddlCriterio.DataTextField = "Descripcion";
                ddlCriterio.DataValueField = "Id";
                ddlCriterio.DataBind();
                Session["textoFiltro"] = false;
            }
            else
            {
                ddlCriterio.Items.Add("Empieza con");
                ddlCriterio.Items.Add("Termina con");
                ddlCriterio.Items.Add("Contiene");
                Session["textoFiltro"] = true;
            }
        }

        protected void btnBusqueda_Click(object sender, EventArgs e)
        {
            articuloNegocio negocio = new articuloNegocio();
            try
            {
                Helper helper = new Helper();

                // si el ddl es precio o codigo
                if (ddlCampo.Items.ToString() == "Precio" || ddlCampo.Items.ToString() == "Codigo")
                {
                    if (helper.nadaSeleccionado(ddlCriterio) || helper.nadaSeleccionado(ddlCampo) || helper.estaVacio(txtFiltroAvanzado.Text))
                    {
                        lbAlerta.Visible = true;
                        lbAlerta.Text = "Completar todos los campos";
                        return;
                    }
                    if (ddlCampo.SelectedItem.ToString() == "Precio" && !Regex.IsMatch(txtFiltroAvanzado.Text, "\\s*([\\d,]+)"))
                    {
                        lbFiltro.Visible = true;
                        lbFiltro.Text = "Monto no valido";
                        return;
                    }
                }
                lbAlerta.Visible = false;
                lbFiltro.Visible = false;

                string campo = ddlCampo.SelectedItem.ToString();
                string criterio = ddlCriterio.SelectedItem.ToString();
                string filtro = txtFiltroAvanzado.Text;
                filtro = filtro.Replace(",", ".");
                List<Articulo> filtrada = negocio.listarFiltrado(campo, criterio, filtro);
                dgvArticulos.DataSource = filtrada;
                dgvArticulos.DataBind();
                if (filtrada.Count > 0)
                {
                    Session["BusquedaEjecutada"] = true;
                    Session.Add("listaFiltrada", filtrada);
                    imgBusquedaNula.Visible = false;
                }
                else
                    imgBusquedaNula.Visible = true;

            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }
    }
}