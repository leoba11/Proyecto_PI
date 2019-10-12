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

       
        // GET: conocimientos/Delete/5
        public ActionResult Delete(string cedulaEmp, string cono)
        {
            if (cedulaEmp == null || cono == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            conocimientos conocimientos = db.conocimientos.Find(cedulaEmp, cono);
            if (conocimientos == null)
            {
                return HttpNotFound();
            }
            return View(conocimientos);
        }

        // POST: conocimientos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string cedulaEmp, string cono)
        {
            conocimientos conocimientos = db.conocimientos.Find(cedulaEmp, cono);
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

        public List<conocimientos> PassKnowledge()
        {
            List<conocimientos> conocimientos = db.conocimientos.OrderBy(d => d.cedulaEmpleadoFK).GroupBy(d => d.conocimientoPK).SelectMany(g => g).ToList();
            return conocimientos;
        }
    }
}
