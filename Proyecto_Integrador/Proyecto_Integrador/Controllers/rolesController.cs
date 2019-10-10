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
    public class rolesController : Controller
    {
        private Gr02Proy1Entities db = new Gr02Proy1Entities();

        // GET: roles
        public ActionResult Index()
        {
            var roles = db.roles.Include(r => r.empleados).Include(r => r.proyectos);
            return View(roles.ToList());
        }

        // GET: roles/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            roles roles = db.roles.Find(id);
            if (roles == null)
            {
                return HttpNotFound();
            }
            return View(roles);
        }

        // GET: roles/Create
        public ActionResult Create()
        {
            ViewBag.cedulaFK = new SelectList(db.empleados, "cedulaPK", "nombre");
            ViewBag.codigoProyectoFK = new SelectList(db.proyectos, "codigoPK", "nombre");
            return View();
        }

        // POST: roles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "rol,cedulaFK,codigoProyectoFK")] roles roles)
        {
            if (ModelState.IsValid)
            {
                db.roles.Add(roles);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cedulaFK = new SelectList(db.empleados, "cedulaPK", "nombre", roles.cedulaFK);
            ViewBag.codigoProyectoFK = new SelectList(db.proyectos, "codigoPK", "nombre", roles.codigoProyectoFK);
            return View(roles);
        }

        // GET: roles/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            roles roles = db.roles.Find(id);
            if (roles == null)
            {
                return HttpNotFound();
            }
            ViewBag.cedulaFK = new SelectList(db.empleados, "cedulaPK", "nombre", roles.cedulaFK);
            ViewBag.codigoProyectoFK = new SelectList(db.proyectos, "codigoPK", "nombre", roles.codigoProyectoFK);
            return View(roles);
        }

        // POST: roles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "rol,cedulaFK,codigoProyectoFK")] roles roles)
        {
            if (ModelState.IsValid)
            {
                db.Entry(roles).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cedulaFK = new SelectList(db.empleados, "cedulaPK", "nombre", roles.cedulaFK);
            ViewBag.codigoProyectoFK = new SelectList(db.proyectos, "codigoPK", "nombre", roles.codigoProyectoFK);
            return View(roles);
        }

        // GET: roles/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            roles roles = db.roles.Find(id);
            if (roles == null)
            {
                return HttpNotFound();
            }
            return View(roles);
        }

        // POST: roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            roles roles = db.roles.Find(id);
            db.roles.Remove(roles);
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


        public void UddRol(int codProyecto, string cedulaEmp)
        {
            roles rol = db.roles.Create();
            rol.codigoProyectoFK = codProyecto;
            rol.cedulaFK = cedulaEmp;
            rol.rol = "desarrollador";
        }

        public void EraseRol(int codProyecto, string cedulaEmp)
        {
            roles roles = db.roles.Find(codProyecto, cedulaEmp);
            db.roles.Remove(roles);
            db.SaveChanges();
        }


        public bool UpdateRol(int codProyecto, string cedulaEmp)
        {
            roles role = db.roles.Create();
            role.codigoProyectoFK = codProyecto;
            role.cedulaFK = cedulaEmp;
            role.rol = "desarrollador";
            return true;
        }

        public bool QuiteRol(int codProyecto, string cedulaEmp)
        {
            roles rol = db.roles.Find(codProyecto, cedulaEmp);
            db.roles.Remove(rol);
            return true;
        }
    }


}
