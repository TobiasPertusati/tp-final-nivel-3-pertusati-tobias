using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    
    public class favoritosNegocio
    {
        private AccesoADatos datos = new AccesoADatos();

        public List<Favorito> listarFavoritos(int idUser) 
        {
            List<Favorito> listaFavoritos = new List<Favorito> ();
            try
            {
                datos.setearConsulta("select F.Id, F.IdUser, F.IdArticulo  from FAVORITOS F where F.IdUser =" + idUser);
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Favorito favorito = new Favorito ();
                    favorito.Id = (int)datos.Lector["Id"];               
                    favorito.IdUser = (int)datos.Lector["IdUser"];
                    favorito.IdArticulo = (int)datos.Lector["IdArticulo"];

                    listaFavoritos.Add(favorito);
                }
                return listaFavoritos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void insertarFavorito(int idUser, int idArticulo)
        {
            try
            {
                datos.setearConsulta("insert into FAVORITOS (idUser, idArticulo) values (@idUser, @idArticulo)");
                datos.setearParametro("@idUser", idUser);
                datos.setearParametro("@idArticulo", idArticulo);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void eliminarFavorito(int idUser, int idArticulo)
        {
            try
            {
                datos.setearConsulta("delete from FAVORITOS where IdArticulo = @idArticulo and IdUser = @idUser");
                datos.setearParametro("@idUser", idUser);
                datos.setearParametro("@idArticulo", idArticulo);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
