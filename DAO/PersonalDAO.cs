using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using MVC_CRUD_Personal_Imagen.Models;
using System.Data.SqlClient;
namespace MVC_CRUD_Personal_Imagen.DAO
{
    public class PersonalDAO
    {

        // Listar Personal 
        public List<pa_listar_PersonalModel> ListarPersonal()
        {
            List<pa_listar_PersonalModel> listar=
                new List<pa_listar_PersonalModel> ();
            SqlDataReader dr = HelperDB.RetornaDataReader("pa_listar_Personal");
            while (dr.Read()) 
            {
                pa_listar_PersonalModel obj = new pa_listar_PersonalModel()
                {
                    codper = dr.GetInt32(0),
                    nomper = dr.GetString(1),
                    fecing = dr.GetDateTime(2),
                    sueper = dr.GetDecimal(3),
                    fotoper = dr.GetString(4),
                    antiguedad = dr.GetInt32(5),
                    acumulado = dr.GetDecimal(6)

                };

                listar.Add(obj);
            }
            dr.Close();
            return listar;
        }

        //CRUD del Personal
        public string GrabarPersonal(PersonalModel objper)
        {
            string mensaje = "";
            try
            {
                HelperDB.EjecutarSPMantenimiento("pa_Agregar_Personal",
                    objper.nomper, objper.fecing,
                    objper.sueper, objper.fotoper
                    );
                //
                mensaje = "Nuevo Personal Registrado Correctamente";

            }
            catch(Exception ex)
            {
                mensaje = ex.Message;
            }

            return mensaje;
        }

        public string EliminarPersonal(int codper)
        {
            string mensaje = "";
            try
            {
                HelperDB.EjecutarSPMantenimiento("pa_Eliminar_Personal",codper);
                mensaje = "Personal Eliminado de forma Correcta";

            }
            catch(Exception ex)
            {
                mensaje = ex.Message;
            }
            return mensaje;
        }

        // ayuda 
        public string ActualizarPersonal(PersonalModel objper)
        {
            string mensaje = "";
            try
            {
                HelperDB.EjecutarSPMantenimiento("pa_Actualizar_Personal",
                    objper.codper,objper.nomper,objper.fecing,
                    objper.sueper,objper.fotoper
                    );
                mensaje = "Datos del Personal Actualizado correctamente";
            }
            catch (Exception ex)
            {

                mensaje = ex.Message;
            }
            //
            return mensaje;
        }
    }

}