using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProyectoIntegrador_mejorado.Models;

namespace ProyectoIntegrador_mejorado.Controllers
{
    [Authorize]
    public class requerimientosController : Controller
    {
        private Gr02Proy1Entities db = new Gr02Proy1Entities();

        // GET: requerimientos
        public ActionResult Index()
        {
            //var requerimientos = db.requerimientos.Include(r => r.empleados).Include(r => r.modulos);
            //return View(requerimientos.ToList());

            List<proyectos> proyectos = new proyectosController().Pass();
            TempData["proyectos"] = proyectos;
            TempData.Keep();

            List<modulos> modulos = new modulosController().Pass();
            TempData["modulos"] = modulos;
            TempData.Keep();
            return View();
        }
        [HttpPost]
        public ActionResult Index(proyectos proyectito, modulos modulito)
        {
            //acá se cambió para que solo agarre los requerimientos relacionados con el proyecto que el usuario escogio
            if (proyectito.codigoPK != 0 && modulito.idPK != 0)
            {
                TempData["proyecto"] = proyectito.codigoPK;
                TempData["nombreProyecto"] = new proyectosController().ProjectByCode(int.Parse(TempData["proyecto"].ToString())).nombre;

                TempData["modulos"] = modulito.idPK;
                
                try {
                    TempData["nombreModulo"] = new modulosController().ModByCode(int.Parse(TempData["proyecto"].ToString()), int.Parse(TempData["modulos"].ToString())).nombre;
                }catch (NullReferenceException )
                {
                    return RedirectToAction("Index", "requerimientos");
                }

                TempData.Keep();
                return RedirectToAction("Lista", "requerimientos");
            }
            else
            {
                return View();
            }
        }


        public ActionResult GetModulList(int codigoProyecto)
        {
            List<modulos> modulos = new modulosController().PassByProyect(codigoProyecto);
            ViewBag.Moduls = new SelectList(modulos, "idPK", "nombre");
            
            return PartialView("ModulsPartial");
        }


        public ActionResult Lista()
        {
            //Se agrega este método para deplegar los datos de los modulos del proyecto que el usuario seleccionó
            //el método agarra el id del proyecto, para desplegar entonces solo los modulos correspondientes
            TempData.Keep();
            if (TempData["proyecto"] != null && TempData["modulos"] != null)
            {
                int codigo = int.Parse(TempData["proyecto"].ToString());
                int idMod = int.Parse(TempData["modulos"].ToString());
                List<requerimientos> requerimientos = db.requerimientos.Where(x => x.codigoProyectoFK == codigo &&  x.idModuloFK == idMod).ToList();
                TempData["requerimientos"] = requerimientos;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "requerimientos");
            }
        }


        // GET: requerimientos/Details/5
        [Authorize(Roles = "Soporte, JefeDesarrollo, Lider, Desarrollador")]
        public ActionResult Details(int? idProyecto, int? idModulo, int? id)
        {
            if (idProyecto == null || idModulo == null || id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            requerimientos requerimientos = db.requerimientos.Find(idProyecto, idModulo, id);
            if (requerimientos == null)
            {
                return HttpNotFound();
            }
            return View(requerimientos);
        }

        // GET: requerimientos/Create
        [Authorize(Roles = "Soporte, JefeDesarrollo, Lider")]
        public ActionResult Create()
        {
            ViewBag.cedulaEmpleadoFK = new SelectList(db.empleados, "cedulaPK", "nombre");
            ViewBag.codigoProyectoFK = new SelectList(db.proyectos, "codigoPK", "nombre");
            ViewBag.idModuloFK = new SelectList(db.modulos, "idPK", "nombre");
            return View();
        }

        // POST: requerimientos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "codigoProyectoFK,idModuloFK,idPK,descripcion,complejidad,estado,cedulaEmpleadoFK,fechaInicio,fechaFin,duracionEstimada,duracionDias,nombre")] requerimientos requerimientos)
        {
            
         
            if (ModelState.IsValid)
            {
                db.requerimientos.Add(requerimientos);
                try
                {
                    db.SaveChanges();
                }
                catch(System.Data.SqlClient.SqlException ) {
                    return RedirectToAction("Index", "requerimientos");
                }
                catch (Exception )
                {
                    return RedirectToAction("Index", "requerimientos");
                }

                return RedirectToAction("Index");
            }



            ViewBag.cedulaEmpleadoFK = new SelectList(db.empleados, "cedulaPK", "nombre", requerimientos.cedulaEmpleadoFK);
            ViewBag.codigoProyectoFK = new SelectList(db.proyectos, "codigoPK", "nombre", requerimientos.codigoProyectoFK);
            ViewBag.idModuloFK = new SelectList(db.modulos, "idPK", "nombre", requerimientos.idModuloFK);
            return View(requerimientos);
        }

        // GET: requerimientos/Edit/5
        [Authorize(Roles = "Soporte, JefeDesarrollo, Lider, Desarrollador")]
        public ActionResult Edit(int? idProyecto, int? idModulo, int? id)
        {
            if (idProyecto == null || idModulo == null || id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            requerimientos requerimientos = db.requerimientos.Find(idProyecto, idModulo, id);
            if (requerimientos == null)
            {
                return HttpNotFound();
            }
            ViewBag.cedulaEmpleadoFK = new SelectList(db.empleados, "cedulaPK", "nombre", requerimientos.cedulaEmpleadoFK);
            ViewBag.codigoProyectoFK = new SelectList(db.proyectos, "codigoPK", "nombre", requerimientos.codigoProyectoFK);
            ViewBag.idModuloFK = new SelectList(db.modulos, "idPK", "nombre", requerimientos.idModuloFK);
            return View(requerimientos);
        }

        // POST: requerimientos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "codigoProyectoFK,idModuloFK,idPK,descripcion,complejidad,estado,cedulaEmpleadoFK,fechaInicio,fechaFin,duracionEstimada,duracionDias,nombre")] requerimientos requerimientos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(requerimientos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cedulaEmpleadoFK = new SelectList(db.empleados, "cedulaPK", "nombre", requerimientos.cedulaEmpleadoFK);
            ViewBag.codigoProyectoFK = new SelectList(db.proyectos, "codigoPK", "nombre", requerimientos.codigoProyectoFK);
            ViewBag.idModuloFK = new SelectList(db.modulos, "idPK", "nombre", requerimientos.idModuloFK);
            return View(requerimientos);
        }

        // GET: requerimientos/Delete/5
        [Authorize(Roles = "Soporte, JefeDesarrollo, Lider")]
        public ActionResult Delete(int? idProyecto, int? idModulo, int? id)
        {
            if (idProyecto == null || idModulo == null || id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            requerimientos requerimientos = db.requerimientos.Find(idProyecto, idModulo, id);
            if (requerimientos == null)
            {
                return HttpNotFound();
            }
            return View(requerimientos);
        }

        // POST: requerimientos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? idProyecto, int? idModulo, int? id)
        {
            requerimientos requerimientos = db.requerimientos.Find(idProyecto, idModulo, id);
            db.requerimientos.Remove(requerimientos);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
