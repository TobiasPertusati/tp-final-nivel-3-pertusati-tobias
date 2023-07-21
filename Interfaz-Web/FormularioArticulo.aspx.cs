using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Interfaz_Web
{
    public partial class FormularioArticulo : System.Web.UI.Page
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
                txtId.Enabled = false;
                if (!IsPostBack) 
                {
                    categoriaNegocio categoriaNegocio = new categoriaNegocio();
                    List<Categoria> categorias = categoriaNegocio.listar_categorias();
                    ddlCategoria.DataSource = categorias;
                    ddlCategoria.DataTextField = "Descripcion";
                    ddlCategoria.DataValueField = "Id";
                    ddlCategoria.DataBind();

                    marcaNegocio marcaNegocio = new marcaNegocio();
                    List<Marca> marcas = marcaNegocio.listar_marcas();
                    ddlMarca.DataSource = marcas;
                    ddlMarca.DataTextField = "Descripcion";
                    ddlMarca.DataValueField = "Id";
                    ddlMarca.DataBind();

                    string idSeleccionado = Request.QueryString["id"] != null ? Request.QueryString["id"].ToString() : "";
                    if (idSeleccionado != "")
                    {
                        btnAgregar.Text = "Modificar";
                        chkConfirma.Visible = false;

                        int idSel = int.Parse(idSeleccionado);
                        articuloNegocio negocio = new articuloNegocio();
                        Articulo articuloSel = negocio.listarArticulos(idSeleccionado)[0];

                        // Precargar los campos
                        txtId.Text = idSeleccionado;
                        txtCodigo.Text = articuloSel.Codigo;
                        txtDescripcion.Text = articuloSel.Descripcion;
                        txtNombre.Text = articuloSel.Nombre;
                        txtPrecio.Text = articuloSel.Precio.ToString();
                        txtUrlImagen.Text = articuloSel.UrlImagen;
                        ddlCategoria.SelectedValue = articuloSel.Categoria.Id.ToString();
                        ddlMarca.SelectedValue = articuloSel.Marca.Id.ToString();
                        txtUrlImagen_TextChanged(sender, e);
                    }
                    else
                    {
                        btnAgregar.Text = "Agregar";
                        btnEliminar.Visible = false;
                        chkConfirma.Visible = false;
                        txtId.Text = Session["ultimoId"] != null ? ((int)Session["ultimoId"] + 1).ToString() : "";
                    }
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                //Validaciones 
                Page.Validate();
                if (!Page.IsValid)
                    return;

                Helper helper = new Helper();
                if (helper.estaVacio(txtCodigo.Text) || helper.estaVacio(txtDescripcion.Text) || helper.estaVacio(txtNombre.Text)
                    || helper.estaVacio(txtPrecio.Text) || helper.estaVacio(txtUrlImagen.Text) || helper.nadaSeleccionado(ddlMarca) || helper.nadaSeleccionado(ddlCategoria))
                    return;

                // Guardo los datos
                articuloNegocio negocio = new articuloNegocio();
                Articulo articulo = new Articulo();
                articulo.Codigo = txtCodigo.Text;
                articulo.Nombre = txtNombre.Text;
                articulo.Descripcion = txtDescripcion.Text;
                articulo.Precio = double.Parse(txtPrecio.Text);
                articulo.UrlImagen = txtUrlImagen.Text;
                articulo.Categoria = new Categoria();
                articulo.Categoria.Id = int.Parse(ddlCategoria.SelectedValue);
                articulo.Marca = new Marca();
                articulo.Marca.Id = int.Parse(ddlMarca.SelectedValue);

                if (Request.QueryString["Id"] != null)
                {
                    articulo.Id = int.Parse(txtId.Text);
                    negocio.modificar(articulo);
                }
                else
                    negocio.agregar(articulo);

                ((List<Articulo>)Session["listaArticulos"]).Add(articulo);
                Response.Redirect("ListadoArticulos.aspx", false);

            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void txtUrlImagen_TextChanged(object sender, EventArgs e)
        {
            imgArticulo.ImageUrl = txtUrlImagen.Text;
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            articuloNegocio negocio = new articuloNegocio();
            try
            {
                if (chkConfirma.Visible == false)
                {
                    chkConfirma.Visible = true;
                    return;
                }
                if (!chkConfirma.Checked)               
                    return;
                
                negocio.eliminarFisico(int.Parse(txtId.Text));
                Response.Redirect("ListadoArticulos.aspx", false);
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }
    }
}