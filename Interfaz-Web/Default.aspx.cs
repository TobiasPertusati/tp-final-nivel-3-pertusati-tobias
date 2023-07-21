using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace Interfaz_Web
{
    public partial class Default : System.Web.UI.Page
    {
        public List<Articulo> listaArticulos { get; set; }
        public List<Marca> listaMarcas { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // LOGICA DE LOS ARTICULOS Y EL INDICE
                articuloNegocio negocio = new articuloNegocio();
                listaArticulos = negocio.listarArticulos();
                Session.Add("listaArticulos", listaArticulos);

                marcaNegocio marcaNegocio = new marcaNegocio();
                listaMarcas = marcaNegocio.listar_marcas();
                listaMarcas[0].UrlImagen = "https://turbologo.com/articles/wp-content/uploads/2019/07/samsung-logo-1993.jpg.webp";
                listaMarcas[1].UrlImagen = "https://1000logos.net/wp-content/uploads/2016/10/Apple-Logo.png";
                listaMarcas[2].UrlImagen = "https://1000marcas.net/wp-content/uploads/2020/01/logo-Sony.png";
                listaMarcas[3].UrlImagen = "https://1000logos.net/wp-content/uploads/2018/10/Huawei-logo.jpg";
                listaMarcas[4].UrlImagen = "https://1000logos.net/wp-content/uploads/2017/04/Motorola-Logo.png";
                Session.Add("listaMarcas", listaMarcas);

            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }
    }
}