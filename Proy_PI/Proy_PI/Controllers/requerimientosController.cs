using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Proy_PI.Models;

namespace Proy_PI.Controllers
{
    public class requerimientosController : Controller
    {
        private Gr02Proy1Entities2 db = new Gr02Proy1Entities2();

        // GET: requerimientos
        public ActionResult Index()
        {
            var requerimientos = db.requerimientos.Include(r => r.modulos);
            return View(requerimientos.ToList());
        }

        // GET: requerimientos/Details/5
        public ActionResult Details(string id)
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
            ViewBag.codigoProyectoFK = new SelectList(db.modulos, "codigoProyectoFK", "descripcion");
            return View();
        }

        // POST: requerimientos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "codigoProyectoFK,nombreModuloFK,idPK,descripcion,peso,estado,cedulaEmpleadoFK,fechaInicio,fechaFin,duraciónEstimada,duraciónReal")] requerimientos requerimientos)
        {
            if (ModelState.IsValid)
            {
                db.requerimientos.Add(requerimientos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.codigoProyectoFK = new SelectList(db.modulos, "codigoProyectoFK", "descripcion", requerimientos.codigoProyectoFK);
            return View(requerimientos);
        }

        // GET: requerimientos/Edit/5
        public ActionResult Edit(string id)
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
            ViewBag.codigoProyectoFK = new SelectList(db.modulos, "codigoProyectoFK", "descripcion", requerimientos.codigoProyectoFK);
            return View(requerimientos);
        }

        // POST: requerimientos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "codigoProyectoFK,nombreModuloFK,idPK,descripcion,peso,estado,cedulaEmpleadoFK,fechaInicio,fechaFin,duraciónEstimada,duraciónReal")] requerimientos requerimientos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(requerimientos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.codigoProyectoFK = new SelectList(db.modulos, "codigoProyectoFK", "descripcion", requerimientos.codigoProyectoFK);
            return View(requerimientos);
        }

        // GET: requerimientos/Delete/5
        public ActionResult Delete(string id)
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
        public ActionResult DeleteConfirmed(string id)
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
