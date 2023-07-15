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
    public partial class DetalleArticulo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["id"] != null)
                    {
                        Articulo articulodetalle;
                        List<Articulo> lalista = (List<Articulo>)Session["listaArticulos"];
                        int idArticulo = int.Parse(Request.QueryString["id"].ToString());

                        // HAGO ESTA PREGUNTA PARA DESCARTAR IDS QUE EL USUARIO INGRESE POR LA URL QUE ESTEN FUERA DE LA LISTA DE LOS ARTICULOS.
                        if (idArticulo > lalista.Last().Id)
                        {
                            Session.Add("error", "No existe ningun articulo con esa ID");
                            Response.Redirect("Error.aspx", false);
                            return;
                        }

                        // UTILIZO LA BUSQUEDA BINARIA PARA ENCONTRAR EL ARTICULO EN LA LISTA
                        // YA QUE AL PERMITIRLE AL ADMIN ELIMINAR ARTICULOS DE LA BASE DE DATOS (ELIMINACION FISICA) SUCEDE
                        // QUE EL ID DEL ARTICULO NO ES IGUAL AL INDICE DE ESE MISM0 ARTICULO EN LA LISTA
                        int pos = -1, izq = 0, der = lalista.Count() - 1;
                        while (izq <= der)
                        {
                            int c = (izq + der) / 2;
                            if (lalista[c].Id == idArticulo)
                            {
                                pos = c;
                                break;
                            }
                            if (idArticulo > lalista[c].Id)
                                izq = c + 1;
                            else
                                der = c - 1;
                        }
                        articulodetalle = lalista[pos];

                        // PARA MOSTRAR PRODUCTOS RELACIONADOS (DE LA MISMA CATEGORIA)
                        List<Articulo> listaxcategoria = new List<Articulo>();
                        int cont = 0;
                        foreach (Articulo articulo in lalista)
                        {
                            if ((articulo.Categoria.Id == articulodetalle.Categoria.Id) && (articulo != articulodetalle) && cont < 4)
                            {
                                listaxcategoria.Add(articulo);
                                cont++;
                            }
                        }
                        Session.Add("articulodetalle", articulodetalle);
                        Session.Add("listaxcategoria", listaxcategoria);

                        //ME GUARDO EN SESSION LOS FAVORITOS DEL USUARIO PARA TENER EN CUENTA CUALES YA TIENE COMO FAVORITOS
                        if (Seguridad.sesionActiva(Session["usuario"]))
                        {
                            favoritosNegocio favoritosNegocio = new favoritosNegocio();
                            Session.Add("favs", favoritosNegocio.listarFavoritos(((User)Session["usuario"]).Id));
                        }
                    }
                    else
                    {
                        Session.Add("error", "No se encontro un articulo para mostrar");
                        Response.Redirect("Error.aspx", false);
                    }
                }

            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }
        protected void btnFavorito_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Seguridad.sesionActiva(Session["usuario"]))
                {
                    return;
                }
                else
                {
                    // TOMO LOS DATOS DEL USUARIO Y DEL ARTICULO
                    int idUser = ((User)Session["usuario"]).Id;
                    int idArticulo = ((Articulo)Session["articulodetalle"]).Id;
                    favoritosNegocio negocio = new favoritosNegocio();

                    // RECORRO LA LISTA DE FAVORITOS PARA VERIFICAR QUE EL USUARIO NO INGRESE 2 VECES EL MISMO ARTICULO A FAVORITO
                    foreach (Favorito favorito in (List<Favorito>)Session["favs"])
                    {
                        if (idArticulo == favorito.IdArticulo)
                            return;
                    }
                    negocio.insertarFavorito(idUser, idArticulo);
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