using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MVC_CRUD_Personal_Imagen.Models;
using MVC_CRUD_Personal_Imagen.DAO;
using System.IO;
namespace MVC_CRUD_Personal_Imagen.Controllers
{
    public class PersonalController : Controller
    {
        //definir la(s) variable(s) de los DAO
        PersonalDAO dao = new PersonalDAO();


        // GET: Personal
        public ActionResult IndexPersonal()
        {
            return View(dao.ListarPersonal());
        }

        // 01-06-2023
        PersonalModel BuscarPersonal(int codigo)
        {
            var query = dao.ListarPersonal()
                        .Find( per=>per.codper==codigo);
            //
            PersonalModel buscado = new PersonalModel()
            {
                codper=query.codper,
                nomper=query.nomper,
                fecing=query.fecing,
                sueper=query.sueper,
                fotoper=query.fotoper
            };
            return buscado;
        }

        // GET: Personal/Details/5
        public ActionResult DetailsPersonal(int id)
        {
            var listado = BuscarPersonal(id);

            return View(listado);
            //return View(BuscarPersonal(id));
        }

        // GET: Personal/Create
        public ActionResult CreatePersonal()
        {
            PersonalModel obj = new PersonalModel();
            obj.codper = 0;
            obj.fecing =  DateTime.Today;
            //

            return View(obj);
        }

        // POST: Personal/Create
        [HttpPost]
        public ActionResult CreatePersonal(PersonalModel objNuevo,
                                        HttpPostedFileBase fper)
        {
            try
            {
                // validaciones sobre el archivo de imagen recibido
                // si el archivo ees nulo , entonces regresamos
                if (fper == null)
                {
                    ViewBag.MENSAJE = "Debe seleccionar una imagen";
                    return View(objNuevo);
                }
                //si la extension de la imagen recibida No es JPG, entonces regresamos
                if (Path.GetExtension(fper.FileName) != ".jpg")
                {
                    ViewBag.MENSAJE = "La imagen debe ser de tipo .JPG";
                    return View(objNuevo);
                }
                //para asegurarnos que nuestro modelo sera valido
                objNuevo.codper = 0;
                objNuevo.fotoper = "~/Foto/" + Path.GetFileName(fper.FileName);

                if (ModelState.IsValid)
                {
                    ViewBag.MENSAJE = dao.GrabarPersonal(objNuevo);
                }
                // guardando la imagen seleccionada en la carpeta Fotos del sitio MvC
                fper.SaveAs(
                    Path.Combine(Server.MapPath("~/Fotos/"), Path.GetFileName(fper.FileName))
                    );

            }
            catch(Exception ex)
            {
                ViewBag.MENSAJE = ex.Message;
            }
            return View(objNuevo);
        }

        // GET: Personal/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Personal/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Personal/Delete/5
        public ActionResult DeletePersonal(int id)
        {
            return View();
        }

        // POST: Personal/Delete/5
        [HttpPost]
        public ActionResult DeletePersonal(int id, PersonalModel objEli)
        {
            try
            {
                string eliminar = Request.Form["eliminar"].ToString();
                if (eliminar == "si")
                {
                    ViewBag.MENSAJE = dao.EliminarPersonal(id);
                    return RedirectToAction("IndexPersonal");
                }
            }
            catch(Exception ex)
            {
                ViewBag.MENSAJE = ex.Message;
            }
            return View(BuscarPersonal(id));
        }
    }
}
