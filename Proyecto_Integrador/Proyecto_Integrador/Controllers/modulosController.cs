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
    public class modulosController : Controller
    {
        private Gr02Proy1Entities db = new Gr02Proy1Entities();

        // GET: modulos
        public ActionResult Index()
        {
            var modulos = db.modulos.Include(m => m.proyectos);
            return View(modulos.ToList());
        }

        // GET: modulos/Details/5
        public ActionResult Details(int? codProyecto, string nombreMod)
        {
            if (codProyecto == null || nombreMod == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            modulos modulos = db.modulos.Find(codProyecto, nombreMod);
            if (modulos == null)
            {
                return HttpNotFound();
            }
            return View(modulos);
        }

        // GET: modulos/Create
        public ActionResult Create()
        {
            ViewBag.codigoProyectoFK = new SelectList(db.proyectos, "codigoPK", "nombre");
            return View();
        }

        // POST: modulos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "codigoProyectoFK,nombrePK,descripcion")] modulos modulos)
        {
            if (ModelState.IsValid)
            {
                db.modulos.Add(modulos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.codigoProyectoFK = new SelectList(db.proyectos, "codigoPK", "nombre", modulos.codigoProyectoFK);
            return View(modulos);
        }

        // GET: modulos/Edit/5
        public ActionResult Edit(int? codProyecto, string nombreMod)
        {
            if (codProyecto == null || nombreMod == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            modulos modulos = db.modulos.Find(codProyecto, nombreMod);
            if (modulos == null)
            {
                return HttpNotFound();
            }
            ViewBag.codigoProyectoFK = new SelectList(db.proyectos, "codigoPK", "codigo", modulos.codigoProyectoFK);
            ViewBag.nombrePK = new SelectList(db.modulos, "nombrePK", "nombre", modulos.nombrePK);
            return View(modulos);
        }

        // POST: modulos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "codigoProyectoFK,nombrePK,descripcion")] modulos modulos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(modulos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.codigoProyectoFK = new SelectList(db.proyectos, "codigoPK", "codigo", modulos.codigoProyectoFK);
            return View(modulos);
        }

        // GET: modulos/Delete/5
        public ActionResult Delete(int? codProyecto, string nombreMod)
        {
            if (codProyecto == null || nombreMod == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            modulos modulos = db.modulos.Find(codProyecto, nombreMod);
            if (modulos == null)
            {
                return HttpNotFound();
            }
            return View(modulos);
        }

        // POST: modulos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int codProyecto, string nombreMod)
        {
            modulos modulos = db.modulos.Find(codProyecto, nombreMod);
            db.modulos.Remove(modulos);
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
