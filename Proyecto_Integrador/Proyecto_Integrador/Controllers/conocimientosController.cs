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
    public class conocimientosController : Controller
    {
        private Gr02Proy1Entities db = new Gr02Proy1Entities();

        // GET: conocimientos
        public ActionResult Index()
        {
            var conocimientos = db.conocimientos.Include(c => c.empleados);
            return View(conocimientos.ToList());
        }

        // GET: conocimientos/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            conocimientos conocimientos = db.conocimientos.Find(id);
            if (conocimientos == null)
            {
                return HttpNotFound();
            }
            return View(conocimientos);
        }

        // GET: conocimientos/Create
        public ActionResult Create()
        {
            ViewBag.cedulaEmpleadoFK = new SelectList(db.empleados, "cedulaPK", "nombre");
            return View();
        }

        // POST: conocimientos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cedulaEmpleadoFK,conocimientoPK")] conocimientos conocimientos)
        {
            if (ModelState.IsValid)
            {
                db.conocimientos.Add(conocimientos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cedulaEmpleadoFK = new SelectList(db.empleados, "cedulaPK", "nombre", conocimientos.cedulaEmpleadoFK);
            return View(conocimientos);
        }

        // GET: conocimientos/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            conocimientos conocimientos = db.conocimientos.Find(id);
            if (conocimientos == null)
            {
                return HttpNotFound();
            }
            ViewBag.cedulaEmpleadoFK = new SelectList(db.empleados, "cedulaPK", "nombre", conocimientos.cedulaEmpleadoFK);
            return View(conocimientos);
        }

        // POST: conocimientos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cedulaEmpleadoFK,conocimientoPK")] conocimientos conocimientos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(conocimientos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cedulaEmpleadoFK = new SelectList(db.empleados, "cedulaPK", "nombre", conocimientos.cedulaEmpleadoFK);
            return View(conocimientos);
        }

        // GET: conocimientos/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            conocimientos conocimientos = db.conocimientos.Find(id);
            if (conocimientos == null)
            {
                return HttpNotFound();
            }
            return View(conocimientos);
        }

        // POST: conocimientos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            conocimientos conocimientos = db.conocimientos.Find(id);
            db.conocimientos.Remove(conocimientos);
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
