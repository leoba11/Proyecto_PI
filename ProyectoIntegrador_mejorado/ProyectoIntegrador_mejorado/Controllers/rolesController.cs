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

        /*
         * Efecto: añade tupla de rol a tabla de roles
         * Requiere: código proyecto, cédula empleado y rol (0 = líder, cualquier otro número para desarrollador)
         * Modifica tabla roles en BD
         */
        [ValidateAntiForgeryToken]
        public void AddRol(int codProyecto, string cedulaEmp, int rolOpt)
        {
            roles rol = db.roles.Create();
            rol.codigoProyectoFK = codProyecto;
            rol.cedulaFK = cedulaEmp;
            if (rolOpt == 0)
                rol.rol = "Líder";
            else
                rol.rol = "Desarrollador";
            db.roles.Add(rol);
            db.SaveChanges();
        }

        /*
         * Efecto: elimina tupla de rol de tabla de roles
         * Requiere: código proyecto y cédula empleado
         * Modifica tabla roles en BD
         */
        public void EraseRol(int codProyecto, string cedulaEmp)
        {
            roles rol = db.roles.Find(cedulaEmp, codProyecto);
            if (rol != null)
            {
                db.roles.Remove(rol);
                db.SaveChanges();
            }
        }
        

        /*
         * Efecto: retorna string que representa cédula de líder de proyecto
         * Requiere: código del proyecto
         * No realiza modificaciones
         */
        public string getLiderId(int codProyecto)
        {
            roles rol = db.roles.FirstOrDefault(r => r.codigoProyectoFK == codProyecto && r.rol == "Líder");
            if (rol == null)
                return null;
            return rol.cedulaFK;
        }

        public bool idLiderNow(string cedula)
        {
            bool resp = false;
            List<roles> lista = (from r in db.roles
                                 join p in db.proyectos
                                 on r.codigoProyectoFK equals p.codigoPK
                                 where (r.cedulaFK == cedula && r.rol == "Líder" && p.fechaFinal == null)
                                 select r
                                 ).ToList();
            if (lista.Count > 0)
            {
                resp = true;
            }

            return resp;
        }

        /*
         * Efecto: Retorna lista de roles en los que aparece el empleado
         * Requiere: cedula del empleado
         * Modifica: NA
         */
        public List<roles> getEmployeeRoles(string cedula)
        {
            List<roles> historyRoles = db.roles.Where(r => r.cedulaFK == cedula).ToList();
            return historyRoles;
        }
    }
}
