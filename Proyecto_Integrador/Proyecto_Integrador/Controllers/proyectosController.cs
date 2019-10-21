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
    public class proyectosController : Controller
    {
        private Gr02Proy1Entities db = new Gr02Proy1Entities();

        // GET: proyectos
        public ActionResult Index()
        {
            var proyectos = db.proyectos.Include(p => p.clientes);
            return View(proyectos.ToList());
        }

        // GET: proyectos/Details/5
        public ActionResult Details(int? id)
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
            ViewBag.cedulaLider = new SelectList(new empleadosController().GetFreeEmployees(), "cedulaPK", "nombre");
            return View();
        }

        // POST: proyectos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "nombre,fechaInicio,fechaFinalEstimada,costoEstimado,objetivo,cedulaClienteFK,idEquipo,fechaFinal,costoReal,cedulaLider")] proyectos proyectos)
        {
            if (ModelState.IsValid)
            {
                db.proyectos.Add(proyectos);
                db.SaveChanges();
                /* Si se selecciona líder se procede a agregar la tupla en roles correspondiente.
                   Llamada a método que agrega rol en el controlador de roles */
                if(proyectos.cedulaLider != null)
                    new rolesController().AddRol(proyectos.codigoPK, proyectos.cedulaLider, 0);
                return RedirectToAction("Index");
            }

            ViewBag.cedulaClienteFK = new SelectList(db.clientes, "cedulaPK", "nombre", proyectos.cedulaClienteFK);
            ViewBag.cedulaLider = new SelectList(new empleadosController().GetFreeEmployees(), "cedulaPK", "nombre", proyectos.cedulaLider);
            return View(proyectos);
        }

        // GET: proyectos/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.hayLider = false;
            /* Si ya hay líder asignado se envía booleano para deshabilitar edición de campo de líder */
            if (new rolesController().getLiderId(proyectos.codigoPK) != null)
                ViewBag.hayLider = true;
            ViewBag.cedulaLider = new SelectList(new empleadosController().GetFreeEmployees(), "cedulaPK", "nombre", proyectos.cedulaLider);
            return View(proyectos);
        }

        // POST: proyectos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "codigoPK,nombre,fechaInicio,fechaFinalEstimada,costoEstimado,objetivo,cedulaClienteFK,idEquipo,fechaFinal,costoReal,cedulaLider")] proyectos proyectos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(proyectos).State = EntityState.Modified;
                db.SaveChanges();
                /* Si se selecciona líder se procede a agregar la tupla en roles correspondiente.
                   Llamada a método que agrega rol en el controlador de roles */
                if (proyectos.cedulaLider != null)
                    new rolesController().AddRol(proyectos.codigoPK, proyectos.cedulaLider, 0);
                return RedirectToAction("Index");
            }
            ViewBag.cedulaClienteFK = new SelectList(db.clientes, "cedulaPK", "nombre", proyectos.cedulaClienteFK);
            ViewBag.hayLider = false;
            /* Si ya hay líder asignado se envía booleano para deshabilitar edición de campo de líder */
            if (new rolesController().getLiderId(proyectos.codigoPK) != null)
                ViewBag.hayLider = true;
            ViewBag.cedulaLider = new SelectList(new empleadosController().GetFreeEmployees(), "cedulaPK", "nombre", proyectos.cedulaLider);
            return View(proyectos);
        }

        // GET: proyectos/Delete/5
        public ActionResult Delete(int? id)
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
        public ActionResult DeleteConfirmed(int id)
        {
            proyectos proyectos = db.proyectos.Find(id);
            /* Si el proyecto tiene líder se procede a eliminar la tupla en roles correspondiente */
            string liderId = new rolesController().getLiderId(proyectos.codigoPK);
            /* Llamada a método que elimina rol en el controlador de roles */
            if (liderId != null)
                new rolesController().EraseRol(proyectos.codigoPK, liderId);
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

        public List<proyectos> Pass()//dispone la lista de proyectos para otros controladores
        {
            List<proyectos> proyectos = db.proyectos.ToList();
            return proyectos;
        }
        public proyectos ProjectByCode(int cod)
        {
            proyectos proy = db.proyectos.Find(cod);
            //TempData["proyectos"] = proyectos;
            //TempData.Keep();
            return proy;
        }
    }
}
