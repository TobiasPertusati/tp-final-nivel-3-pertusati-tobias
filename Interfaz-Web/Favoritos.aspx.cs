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
    public partial class Favoritos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if ((User)Session["usuario"] != null)
                {
                    if (!IsPostBack)
                    {
                        // LISTO LOS FAVORITOS DEL USUARIO
                        favoritosNegocio favoritosNegocio = new favoritosNegocio();
                        List<Favorito> listFav = favoritosNegocio.listarFavoritos(((User)Session["usuario"]).Id);

                        // RECORRO LA LISTA DE FAVORITOS DESPUES LA DE ARTICULOS CUANDO MACHEAN LAS IDS LO AÑADO A MI LISTA NUEVA
                        List<Articulo> articulosFavoritos = new List<Articulo>();
                        foreach (Favorito favorito in listFav)
                        {
                            foreach (Articulo articulo in (List<Articulo>)Session["listaArticulos"])
                            {
                                if (articulo.Id == favorito.IdArticulo)
                                    articulosFavoritos.Add(articulo);
                            }
                        }
                        // LE ASIGNO AL REPETIDOR LA LISTA DE ARTICULOS
                        repFavs.DataSource = articulosFavoritos;
                        repFavs.DataBind();

                        // LA AÑADO A LA SESSION
                        Session.Add("listaFavoritos", articulosFavoritos);
                    }
                }
                else
                {
                    Session.Add("paginaAnterior", "Favoritos.aspx");
                    Response.Redirect("Login.aspx", false);
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            favoritosNegocio favoritosNegocio = new favoritosNegocio();
            try
            {
                // USO EL METODO eliminarFavorito PARA BORRRAR EL REGISTRO DE LA BASE DE DATO
                int iduser = ((User)Session["usuario"]).Id;
                int idarticulo = int.Parse(((Button)sender).CommandArgument);
                favoritosNegocio.eliminarFavorito(iduser, idarticulo);

                // LUEGO PARA QUE EL CAMBIO SE EFECTUE EN LA PAGINA ELIMINO EL ARTICULO DE LA LISTA Y BINDEO DEVUELTA EL REAPETER CON LA LISTA ACTUALIZADA.
                List<Articulo> lista = (List<Articulo>)Session["listaFavoritos"];
                lista.Remove(lista.Find(x => x.Id == idarticulo));
                repFavs.DataSource = lista;
                repFavs.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }
    }
}