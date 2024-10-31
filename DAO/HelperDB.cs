using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace MVC_CRUD_Personal_Imagen.DAO
{
    public class HelperDB
    {
        static string cad_cn = 
            ConfigurationManager.ConnectionStrings["cn1"].ConnectionString;
        private static void PoblarParametrosCommand(
            SqlCommand comando, params object[] parametros)
        {
            int indice = 0;
            //descubrir y  crear la coleccion de parametros del sqlcommand
            SqlCommandBuilder.DeriveParameters(comando);
            //
            foreach(SqlParameter item in comando.Parameters)
            {
                if (item.ParameterName != "@RETURN_VALUE")
                {
                    item.Value = parametros[indice];
                    indice++;
                }
            }

        }
        public static SqlDataReader RetornaDataReader(
            string nombreSP , params object[] Parametros )
        {
            SqlConnection cnx = new SqlConnection(cad_cn);
            cnx.Open();
            //            
            SqlCommand cmd = new SqlCommand(nombreSP, cnx);
            cmd.CommandType = CommandType.StoredProcedure;
            // si hemos enviado valores para los parametros 
            // ,entonces poblamos los parametros
            if (Parametros.Length > 0)
                PoblarParametrosCommand(cmd, Parametros);

            //
            SqlDataReader reader =
                cmd.ExecuteReader(CommandBehavior.CloseConnection);

            return reader;
        }

        public static void EjecutarSPMantenimiento(
              string nombreSP, params object[] Parametros)
        {
            //Conectar a la base de datos
            SqlConnection cnx = new SqlConnection(cad_cn);
            cnx.Open();
            //            
            SqlCommand cmd = new SqlCommand(nombreSP, cnx);
            cmd.CommandType = CommandType.StoredProcedure;
            // si hemos enviado valores para los parametros 
            // ,entonces poblamos los parametros
            if (Parametros.Length > 0)
                PoblarParametrosCommand(cmd, Parametros);
            //ejecutar el sqlcommand
            cmd.ExecuteNonQuery();
            //cerrar la conexion de BD
            cnx.Close();
        }





    }
}