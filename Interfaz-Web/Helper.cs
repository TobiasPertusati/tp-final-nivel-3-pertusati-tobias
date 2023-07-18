using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Interfaz_Web
{
    public class Helper
    {
        public bool estaVacio(string campo)
        {
            if (string.IsNullOrWhiteSpace(campo))             
                return true;
            
            return false;
        }
        public bool nadaSeleccionado(DropDownList ddl)
        {
            if (ddl.SelectedIndex == -1)
            {
                return true;
            }
            return false;
        }
    }
}