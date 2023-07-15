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
                //Cargo el titulo con el nombre de la marca

                int idMarca = Request.QueryString["idMarca"] != null ? int.Parse(Request.QueryString["idMarca"].ToString()) : 1;
                marcaNegocio marcaNegocio = new marcaNegocio();
                Session.Add("listaMarcas", marcaNegocio.listar_marcas());
                titulo.InnerText = "Todos nuestros productos de " + ((List<Marca>)Session["listaMarcas"])[idMarca - 1].Descripcion;

                //Me genero una nueva lista de articulos para que la id siempre esten en orden
                List<Articulo> listaxmarca = new List<Articulo>();
                foreach (Articulo articulo in (List<Articulo>)Session["listaArticulos"])
                {
                    if (articulo.Marca.Id == idMarca)
                        listaxmarca.Add(articulo);    
                }    
                Session.Add("listaxmarca", listaxmarca);
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }
    }
}