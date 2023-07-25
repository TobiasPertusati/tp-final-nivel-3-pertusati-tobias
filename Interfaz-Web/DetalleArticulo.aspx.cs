using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
                        List<Articulo> lalista = (List<Articulo>)Session["listaArticulos"];
                        string idart = Request.QueryString["id"].ToString();
                        int idArticulo = int.Parse(idart);
                        // HAGO ESTA PREGUNTA PARA DESCARTAR IDS QUE EL USUARIO INGRESE POR LA URL QUE ESTEN FUERA DE LA LISTA DE LOS ARTICULOS.
                        if (idArticulo > lalista.Last().Id)
                        {
                            Session.Add("error", "No existe ningun articulo con esa ID");
                            Response.Redirect("Error.aspx", false);
                            return;
                        }
                        // LISTO EL ARTICULO CON LA ID
                        articuloNegocio negocio = new articuloNegocio();
                        Articulo articulodetalle = negocio.listarArticulos(idart)[0];
                        Session.Add("articulodetalle", articulodetalle);

                        // PARA MOSTRAR PRODUCTOS RELACIONADOS (DE LA MISMA CATEGORIA)
                        List<Articulo> listaxcategoria = new List<Articulo>();
                        int cont = 0;
                        foreach (Articulo articulo in lalista)
                        {
                            if ((articulo.Categoria.Id == articulodetalle.Categoria.Id) && (articulo.Id != articulodetalle.Id) && cont < 4)
                            {
                                listaxcategoria.Add(articulo);
                                cont++;
                            }
                        }
                        Session.Add("listaxcategoria", listaxcategoria);
                        // Lo reseteo a 0 porque se esta cargando un nuevo articulo
                        int contador = 0;
                        Session.Add("contadorFav", contador);
                        Session.Add("paginaAnterior", "DetalleArticulo.aspx?id=" + idart);
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
                    Response.Redirect("Login.aspx", false);
                    return;
                }
                else
                {
                    // Genero un contador por si el usuario hace muchas veces click en favorito no vaya a la base de datos siempre
                    int contador = (int)Session["contadorFav"];
                    contador += 1;
                    Session["contadorFav"] = contador;
                    if (contador > 1)
                        return;

                    // TOMO LOS DATOS DEL USUARIO Y DEL ARTICULO
                    int idUser = ((User)Session["usuario"]).Id;
                    int idArticulo = ((Articulo)Session["articulodetalle"]).Id;
                    favoritosNegocio negocio = new favoritosNegocio();
                    List<Favorito> favs = negocio.listarFavoritos(idUser);
                    // RECORRO LA LISTA DE FAVORITOS PARA VERIFICAR QUE EL USUARIO NO INGRESE 2 VECES EL MISMO ARTICULO A FAVORITO
                    foreach (Favorito favorito in favs)
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