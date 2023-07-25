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
    public partial class ArticulosPorMarca : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if(Request.QueryString["Marca"] != null)
                    {
                        string nombreMarca = Request.QueryString["Marca"].ToString();


                        // Me fijo que la marca exista, si no redirijo a la pantalla de error
                        marcaNegocio marcaNegocio = new marcaNegocio();
                        List<Marca> marcas = marcaNegocio.listar_marcas();
                        int cont = 0;
                        foreach (Marca marca in marcas)
                        {
                            if (marca.Descripcion == nombreMarca)
                            {
                                cont = 1;
                                break;
                            }
                        }
                        if (cont == 0) 
                        {
                            Session.Add("error", "No se econtro la marca con el nombre " + nombreMarca);
                            Response.Redirect("Error.aspx", false);
                        }

                        //Cargo el titulo con el nombre de la marca
                        titulo.InnerText = "Todos nuestros productos de " + nombreMarca;

                        // Me genero una lista con todos los articulos 
                        List<Articulo> listaxmarca = new List<Articulo>();
                        foreach (Articulo articulo in (List<Articulo>)Session["listaArticulos"])
                        {
                            if (articulo.Marca.Descripcion == nombreMarca)
                                listaxmarca.Add(articulo);
                        }
                        Session.Add("listaxmarca", listaxmarca);
                        Session.Add("paginaAnterior", "ArituculosPorMarca.aspx?Marca=" + nombreMarca);

                    }
                    else
                    {
                        //Redirijo a default porque no se estaba buscando por ninguna marca
                        Response.Redirect("Default.aspx", false);
                    }
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