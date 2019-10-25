using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Proyecto_Integrador.Models;

namespace Proyecto_Integrador.Controllers
{
    public class requerimientosController : Controller
    {
        private Gr02Proy1Entities db = new Gr02Proy1Entities();

        // GET: requerimientos
        public ActionResult Index()
        {
            var requerimientos = db.requerimientos.Include(r => r.modulos);
            return View(requerimientos.ToList());
        }

        // GET: requerimientos/Details/5
        public ActionResult Details(int? idProyecto, int? idModulo, int? id)
        {
            if (idProyecto == null|| idModulo == null || id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            requerimientos requerimientos = db.requerimientos.Find(idProyecto,idModulo,id);
            if (requerimientos == null)
            {
                return HttpNotFound();
            }
            return View(requerimientos);
        }

        // GET: requerimientos/Create
        public ActionResult Create()
        {
            ViewBag.codigoProyectoFK = new SelectList(db.proyectos, "codigoPK", "nombre");
            ViewBag.idModuloFK = new SelectList(db.modulos, "idPK", "nombre");
            return View();
        }

        // POST: requerimientos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "codigoProyectoFK,idModuloFK,idPK,descripcion,complejidad,estado,cedulaEmpleadoFK,fechaInicio,fechaFin,duraciónEstimada,duraciónReal")] requerimientos requerimientos)
        {
            if (ModelState.IsValid)
            {
                db.requerimientos.Add(requerimientos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.codigoProyectoFK = new SelectList(db.proyectos, "codigoPK", "nombre", requerimientos.codigoProyectoFK);
            ViewBag.idModuloFK = new SelectList(db.modulos, "idPK", "nombre", requerimientos.idModuloFK);
            return View(requerimientos);
        }

        // GET: requerimientos/Edit/5
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
            ViewBag.codigoProyectoFK = new SelectList(db.proyectos, "codigoPK", "nombre", requerimientos.codigoProyectoFK);
            ViewBag.idModuloFK = new SelectList(db.modulos, "idPK", "nombre", requerimientos.idModuloFK);
            return View(requerimientos);
        }

        // POST: requerimientos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "codigoProyectoFK,idModuloFK,idPK,descripcion,complejidad,estado,cedulaEmpleadoFK,fechaInicio,fechaFin,duraciónEstimada,duraciónReal")] requerimientos requerimientos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(requerimientos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.codigoProyectoFK = new SelectList(db.proyectos, "codigoPK", "nombre", requerimientos.codigoProyectoFK);
            return View(requerimientos);
        }

        // GET: requerimientos/Delete/5
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
