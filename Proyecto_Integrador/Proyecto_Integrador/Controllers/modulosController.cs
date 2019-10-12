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
            /*var modulos = db.modulos.Include(m => m.proyectos);
            return View(modulos.ToList());*/
            List<proyectos> proyectos = new proyectosController().Pass();
            TempData["proyectos"] = proyectos;
            TempData.Keep();
            return View();
        }

        [HttpPost]
        public ActionResult Index(proyectos proyectito)
        {
            //TempData["proyecto"] = proyectito.codigoPK;
            if (proyectito.codigoPK != null)
            {
                TempData["proyecto"] = proyectito.codigoPK;
                TempData["nombreProyecto"] = new proyectosController().ProjectByCode(int.Parse(TempData["proyecto"].ToString())).nombre;
                TempData.Keep();
                return RedirectToAction("Lista", "modulos");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Lista()
        {
            TempData.Keep();
            int codigo = int.Parse(TempData["proyecto"].ToString());
            List<modulos> modulos = db.modulos.Where(x => x.codigoProyectoFK == codigo).ToList();
            TempData["modulos"] = modulos;
            return View();
        }


        // GET: modulos/Details/5
        public ActionResult Details(int? codProyecto, int? id)
        {
            if (codProyecto == null || id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            modulos modulos = db.modulos.Find(codProyecto, id);
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
        public ActionResult Create([Bind(Include = "codigoProyectoFK,nombre,descripcion")] modulos modulos)
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
        public ActionResult Edit(int? codProyecto, int? id)
        {
            if (codProyecto == null || id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            modulos modulos = db.modulos.Find(codProyecto, id);
            if (modulos == null)
            {
                return HttpNotFound();
            }
            ViewBag.codigoProyectoFK = new SelectList(db.proyectos, "codigoPK", "codigo", modulos.codigoProyectoFK);
            ViewBag.idPK = new SelectList(db.modulos, "idPK", "id", modulos.idPK);
            return View(modulos);
        }

        // POST: modulos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "codigoProyectoFK,idPK,nombre,descripcion")] modulos modulos)
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
        public ActionResult Delete(int? codProyecto, int? id)
        {
            if (codProyecto == null || id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            modulos modulos = db.modulos.Find(codProyecto, id);
            if (modulos == null)
            {
                return HttpNotFound();
            }
            return View(modulos);
        }

        // POST: modulos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int codProyecto, int? id)
        {
            modulos modulos = db.modulos.Find(codProyecto, id);
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
