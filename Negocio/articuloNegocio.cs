using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class articuloNegocio
    {
        private AccesoADatos datos = new AccesoADatos();

        public List<Articulo> listarArticulos()
        {
            List<Articulo> articulosList = new List<Articulo>();

            try
            {
                datos.setearConsulta("select A.Id, A.Codigo, A.Nombre, A.Descripcion, A.ImagenUrl, A.Precio, M.Descripcion Marca,C.Descripcion Categoria, A.IdMarca, A.IdCategoria from ARTICULOS A, MARCAS M, CATEGORIAS C where A.IdCategoria = C.Id and A.IdMarca = M.Id\r\n");
                datos.ejecutarLectura();
                leerDatos(articulosList);
                return articulosList;
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
        public void agregar(Articulo nuevo)
        {
            try
            {
                datos.setearConsulta("insert into ARTICULOS (Codigo,Nombre,Descripcion,IdMarca,IdCategoria,ImagenUrl,Precio) values (@Codigo, @Nombre, @Descripcion, @IdMarca, @IdCategoria, @ImagenUrl, @Precio)");
                datos.setearParametro("@Codigo", nuevo.Codigo);
                datos.setearParametro("@Nombre", nuevo.Nombre);
                datos.setearParametro("@Descripcion", nuevo.Descripcion);
                datos.setearParametro("@IdMarca", nuevo.Marca.Id);
                datos.setearParametro("@IdCategoria", nuevo.Categoria.Id);
                datos.setearParametro("@ImagenUrl", nuevo.UrlImagen);
                datos.setearParametro("Precio", nuevo.Precio);

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

        public void modificar(Articulo articulo_modify)
        {
            try
            {
                datos.setearConsulta("update ARTICULOS set Codigo = @Codigo, Nombre = @Nombre, Descripcion = @Descripcion, IdMarca = @IdMarca, IdCategoria = @IdCategoria, ImagenUrl = @ImagenUrl, Precio = @Precio where id = @Id");
                datos.setearParametro("@Id", articulo_modify.Id);
                datos.setearParametro("@Nombre", articulo_modify.Nombre);
                datos.setearParametro("@Codigo", articulo_modify.Codigo);
                datos.setearParametro("@Descripcion", articulo_modify.Descripcion);
                datos.setearParametro("@IdMarca", articulo_modify.Marca.Id);
                datos.setearParametro("@IdCategoria", articulo_modify.Categoria.Id);
                datos.setearParametro("@ImagenUrl", articulo_modify.UrlImagen);
                datos.setearParametro("Precio", articulo_modify.Precio);

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

        public void eliminarFisico(int Id)
        {
            try
            {
                datos.setearConsulta("delete from ARTICULOS where id = @Id");
                datos.setearParametro("@Id", Id);
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

        public List<Articulo> listarFiltrado(string campo, string criterio, string filtro)
        {
            List<Articulo> lista_filtrada = new List<Articulo>();
            try
            {   
                string consulta = "select A.Id, A.Codigo, A.Nombre, A.Descripcion, A.ImagenUrl, A.Precio, M.Descripcion Marca,C.Descripcion Categoria, A.IdMarca, A.IdCategoria from ARTICULOS A, MARCAS M, CATEGORIAS C where A.IdCategoria = C.Id and A.IdMarca = M.Id and ";
                switch (campo)
                {
                    case "Precio":
                        switch (criterio)
                        {
                            case "Mayor a":
                                consulta += "A.Precio > " + filtro;
                                break;

                            case "Menor a":
                                consulta += "A.Precio < " + filtro;
                                break;

                            default:
                                consulta += "A.Precio = " + filtro;
                                break;
                        }
                        break;
                    case "Codigo":
                        consulta = manejarConsulta(consulta, criterio, filtro, "A.Codigo");
                        break;
                    case "Marca":
                        consulta = manejarConsulta(consulta, criterio, filtro, "M.Descripcion");
                        break;
                    default:
                        consulta = manejarConsulta(consulta, criterio, filtro, "C.Descripcion");
                        break;
                }
                datos.setearConsulta(consulta);
                datos.ejecutarLectura();
                leerDatos(lista_filtrada);
                return lista_filtrada;
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

        public void leerDatos(List<Articulo> lista)
        {
            while (datos.Lector.Read())
            {
                Articulo aux = new Articulo();
                aux.Id = (int)datos.Lector["Id"];
                aux.Codigo = (string)datos.Lector["Codigo"];
                aux.Nombre = (string)datos.Lector["Nombre"];
                aux.Descripcion = (string)datos.Lector["Descripcion"];
                if (!(datos.Lector["ImagenUrl"] is DBNull))
                    aux.UrlImagen = (string)datos.Lector["ImagenUrl"];
                aux.Precio = (double)(decimal)datos.Lector["Precio"];
                aux.Marca = new Marca();
                aux.Marca.Id = (int)datos.Lector["IdMarca"];
                aux.Marca.Descripcion = (string)datos.Lector["Marca"];
                aux.Categoria = new Categoria();
                aux.Categoria.Id = (int)datos.Lector["IdCategoria"];
                aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];

                lista.Add(aux);
            }
        }
        public string manejarConsulta(string consulta, string criterio, string filtro, string nombre)
        {
            switch (criterio)
            {
                case "Empieza con":
                    consulta += nombre + " like '" + filtro + "%' ";
                    break;

                case "Termina con":
                    consulta += nombre + " like '%" + filtro + "'";
                    break;

                default:
                    consulta += nombre + " like '%" + filtro + "%' ";
                    break;
            }
            return consulta;
        }
    }
}
