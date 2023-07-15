using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dominio;

namespace Negocio
{
    public static class Seguridad
    {
        public static bool sesionActiva(Object user)
        {
            User usuario = user != null ? (User)user : null ;
            if (usuario != null && usuario.Id != 0) 
                return true;
            
            return false;
        }
        
        public static bool esAdmin(Object user) 
        {
            User usuario = user != null ? (User)user : null;
            return usuario != null ? usuario.IsAdmin : false ;
        }
    }
}