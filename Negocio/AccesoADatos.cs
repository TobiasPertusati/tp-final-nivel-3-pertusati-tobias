using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class AccesoADatos
    {
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector;
        public SqlDataReader Lector
        {
            get { return lector; }
        }

        public AccesoADatos()
        {
            //"workstation id=DB_CATALOGO_WEB_TP.mssql.somee.com;packet size=4096;user id=tobiaspertu_SQLLogin_1;pwd=lgthm4jnxh;data source=DB_CATALOGO_WEB_TP.mssql.somee.com;persist security info=False;initial catalog=DB_CATALOGO_WEB_TP"
            //server=.\\SQLEXPRESS; database=CATALOGO_WEB_DB; integrated security=true
            conexion = new SqlConnection("workstation id=DB_CATALOGO_WEB_TP.mssql.somee.com;packet size=4096;user id=tobiaspertu_SQLLogin_1;pwd=lgthm4jnxh;data source=DB_CATALOGO_WEB_TP.mssql.somee.com;persist security info=False;initial catalog=DB_CATALOGO_WEB_TP");
            comando = new SqlCommand();
        }

        public void setearParametro(string campo, object valor)
        {
            comando.Parameters.AddWithValue(campo, valor);
        }

        public int ejecutarAccionScalar()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                return int.Parse(comando.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void setearConsulta(string consulta)
        {
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = consulta;
        }
        public void ejecutarAccion()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void ejecutarLectura()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                lector = comando.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void cerrarConexion()
        {
            if (lector != null)
            {
                lector.Close();
            }
            conexion.Close();
        }
    }
}
