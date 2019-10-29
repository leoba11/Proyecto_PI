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
            var requerimientos = db.requerimientos.Include(r => r.empleados).Include(r => r.modulos);
            return View(requerimientos.ToList());
        }

        // GET: requerimientos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            requerimientos requerimientos = db.requerimientos.Find(id);
            if (requerimientos == null)
            {
                return HttpNotFound();
            }
            return View(requerimientos);
        }

        // GET: requerimientos/Create
        public ActionResult Create()
        {
            ViewBag.cedulaEmpleadoFK = new SelectList(db.empleados, "cedulaPK", "nombre");
            ViewBag.codigoProyectoFK = new SelectList(db.modulos, "codigoProyectoFK", "nombre");
            return View();
        }

        // POST: requerimientos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "codigoProyectoFK,idModuloFK,idPK,descripcion,complejidad,estado,cedulaEmpleadoFK,fechaInicio,fechaFin,duraciónEstimada,duraciónReal,nombre")] requerimientos requerimientos)
        {
            if (ModelState.IsValid)
            {
                db.requerimientos.Add(requerimientos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cedulaEmpleadoFK = new SelectList(db.empleados, "cedulaPK", "nombre", requerimientos.cedulaEmpleadoFK);
            ViewBag.codigoProyectoFK = new SelectList(db.modulos, "codigoProyectoFK", "nombre", requerimientos.codigoProyectoFK);
            return View(requerimientos);
        }

        // GET: requerimientos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            requerimientos requerimientos = db.requerimientos.Find(id);
            if (requerimientos == null)
            {
                return HttpNotFound();
            }
            ViewBag.cedulaEmpleadoFK = new SelectList(db.empleados, "cedulaPK", "nombre", requerimientos.cedulaEmpleadoFK);
            ViewBag.codigoProyectoFK = new SelectList(db.modulos, "codigoProyectoFK", "nombre", requerimientos.codigoProyectoFK);
            return View(requerimientos);
        }

        // POST: requerimientos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "codigoProyectoFK,idModuloFK,idPK,descripcion,complejidad,estado,cedulaEmpleadoFK,fechaInicio,fechaFin,duraciónEstimada,duraciónReal,nombre")] requerimientos requerimientos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(requerimientos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cedulaEmpleadoFK = new SelectList(db.empleados, "cedulaPK", "nombre", requerimientos.cedulaEmpleadoFK);
            ViewBag.codigoProyectoFK = new SelectList(db.modulos, "codigoProyectoFK", "nombre", requerimientos.codigoProyectoFK);
            return View(requerimientos);
        }

        // GET: requerimientos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            requerimientos requerimientos = db.requerimientos.Find(id);
            if (requerimientos == null)
            {
                return HttpNotFound();
            }
            return View(requerimientos);
        }

        // POST: requerimientos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            requerimientos requerimientos = db.requerimientos.Find(id);
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
