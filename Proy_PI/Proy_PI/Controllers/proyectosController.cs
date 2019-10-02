﻿using System;
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
    public class proyectosController : Controller
    {
        private Gr02Proy1Entities2 db = new Gr02Proy1Entities2();

        // GET: proyectos
        public ActionResult Index()
        {
            var proyectos = db.proyectos.Include(p => p.clientes);
            return View(proyectos.ToList());
        }

        // GET: proyectos/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            proyectos proyectos = db.proyectos.Find(id);
            if (proyectos == null)
            {
                return HttpNotFound();
            }
            return View(proyectos);
        }

        // GET: proyectos/Create
        public ActionResult Create()
        {
            ViewBag.cedulaClienteFK = new SelectList(db.clientes, "cedulaPK", "nombre");
            return View();
        }

        // POST: proyectos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "codigoPK,nombre,fechaInicio,fechaFinalEstimada,costoEstimado,objetivo,cedulaClienteFK,idEquipo,fechaFinal,costoReal")] proyectos proyectos)
        {
            if (ModelState.IsValid)
            {
                db.proyectos.Add(proyectos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cedulaClienteFK = new SelectList(db.clientes, "cedulaPK", "nombre", proyectos.cedulaClienteFK);
            return View(proyectos);
        }

        // GET: proyectos/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            proyectos proyectos = db.proyectos.Find(id);
            if (proyectos == null)
            {
                return HttpNotFound();
            }
            ViewBag.cedulaClienteFK = new SelectList(db.clientes, "cedulaPK", "nombre", proyectos.cedulaClienteFK);
            return View(proyectos);
        }

        // POST: proyectos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "codigoPK,nombre,fechaInicio,fechaFinalEstimada,costoEstimado,objetivo,cedulaClienteFK,idEquipo,fechaFinal,costoReal")] proyectos proyectos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(proyectos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cedulaClienteFK = new SelectList(db.clientes, "cedulaPK", "nombre", proyectos.cedulaClienteFK);
            return View(proyectos);
        }

        // GET: proyectos/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            proyectos proyectos = db.proyectos.Find(id);
            if (proyectos == null)
            {
                return HttpNotFound();
            }
            return View(proyectos);
        }

        // POST: proyectos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            proyectos proyectos = db.proyectos.Find(id);
            db.proyectos.Remove(proyectos);
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