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
    public class modulosController : Controller
    {
        private Gr02Proy1Entities2 db = new Gr02Proy1Entities2();

        // GET: modulos
        public ActionResult Index()
        {
            var modulos = db.modulos.Include(m => m.proyectos);
            return View(modulos.ToList());
        }

        // GET: modulos/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            modulos modulos = db.modulos.Find(id);
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
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            modulos modulos = db.modulos.Find(id);
            if (modulos == null)
            {
                return HttpNotFound();
            }
            ViewBag.codigoProyectoFK = new SelectList(db.proyectos, "codigoPK", "nombre", modulos.codigoProyectoFK);
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
            ViewBag.codigoProyectoFK = new SelectList(db.proyectos, "codigoPK", "nombre", modulos.codigoProyectoFK);
            return View(modulos);
        }

        // GET: modulos/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            modulos modulos = db.modulos.Find(id);
            if (modulos == null)
            {
                return HttpNotFound();
            }
            return View(modulos);
        }

        // POST: modulos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            modulos modulos = db.modulos.Find(id);
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
