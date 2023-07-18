using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class userNegocio
    {
        private AccesoADatos datos = new AccesoADatos();

        public int insertarNuevo(User user)
        {
            try
            {
                datos.setearConsulta("insert into USERS (email,pass,admin) output inserted.id values (@email, @pass, 0)");
                datos.setearParametro("@email", user.Email);
                datos.setearParametro("@pass", user.Pass);

                return datos.ejecutarAccionScalar();
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
        public bool login(User user)
        {
            try
            {
                datos.setearConsulta("select Id, email, pass, nombre, apellido, urlImagenPerfil, admin from USERS where email = @email and pass = @pass");
                datos.setearParametro("@email", user.Email);
                datos.setearParametro("@pass", user.Pass);
                datos.ejecutarLectura();
                if (datos.Lector.Read())
                {
                    user.Id = (int)datos.Lector["Id"];
                    user.IsAdmin = (bool)datos.Lector["admin"];
                    if (!(datos.Lector["nombre"] is DBNull))
                        user.Nombre = (string)datos.Lector["nombre"];
                    if (!(datos.Lector["apellido"] is DBNull))
                        user.Apellido = (string)datos.Lector["apellido"];
                    if (!(datos.Lector["urlImagenPerfil"] is DBNull))
                        user.ImagenPerfil = (string)datos.Lector["urlImagenPerfil"];

                    return true;
                }
                return false;
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

        public void actualizar(User user)
        {
            try
            {
                datos.setearConsulta("Update USERS set apellido = @apellido, nombre = @nombre, urlImagenPerfil = @imagenPerfil where Id = @id");
                datos.setearParametro("@id", user.Id);
                datos.setearParametro("@apellido", user.Apellido);
                datos.setearParametro("@nombre", user.Nombre);
                datos.setearParametro("@imagenPerfil", user.ImagenPerfil ?? (object)DBNull.Value);
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
        public bool existeMail(string email)
        {
            try
            {
                datos.setearConsulta("select email from USERS where email = @comprobarEmail");
                datos.setearParametro("@comprobarEmail", email);
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    if (!(datos.Lector["email"] is DBNull))            
                        return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { datos.cerrarConexion();}
        }
    }
}
