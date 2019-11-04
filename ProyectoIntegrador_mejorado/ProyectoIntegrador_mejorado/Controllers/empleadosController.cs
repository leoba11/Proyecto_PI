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
    [Authorize(Roles = "Soporte, JefeDesarrollo, Lider, Desarrollador")]
    public class empleadosController : Controller
    {
        private Gr02Proy1Entities db = new Gr02Proy1Entities();

        // GET: empleados
        public ActionResult Index()
        {
            return View(db.empleados.ToList());
        }

        // GET: empleados/Details/5
        public ActionResult Details(string cedulaPk)
        {
            if (cedulaPk == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            empleados empleados = db.empleados.Find(cedulaPk);
            if (empleados == null)
            {
                return HttpNotFound();
            }
            return View(empleados);
        }

        // GET: empleados/Create
        [Authorize(Roles = "Soporte, JefeDesarrollo")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: empleados/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cedulaPK,nombre,apellido1,apellido2,edad,fechaNacimiento,telefono,provincia,canton,distrito,correo,direccionDetallada,disponibilidad")] empleados empleados)
        {
            //Validacion de la cedula para no repetirla
            if (db.empleados.Any(x => x.cedulaPK == empleados.cedulaPK))
            {
                ModelState.AddModelError("cedulaPK", "No se pueden agregar empleados con la misma cedula");
                return View(empleados);
            }

            if (ModelState.IsValid)
            {
                db.empleados.Add(empleados);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(empleados);
        }

        // GET: empleados/Edit/5
        [Authorize(Roles = "Soporte, JefeDesarrollo")]
        public ActionResult Edit(string cedulaPk)
        {
            if (cedulaPk == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            empleados empleados = db.empleados.Find(cedulaPk);
            if (empleados == null)
            {
                return HttpNotFound();
            }
            return View(empleados);
        }

        // POST: empleados/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cedulaPK,nombre,apellido1,apellido2,edad,fechaNacimiento,telefono,provincia,canton,distrito,correo,direccionDetallada,disponibilidad")] empleados empleados)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empleados).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(empleados);
        }

        // GET: empleados/Delete/5
        [Authorize(Roles = "Soporte, JefeDesarrollo")]
        public ActionResult Delete(string cedulaPk)
        {
            if (cedulaPk == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            empleados empleados = db.empleados.Find(cedulaPk);
            if (empleados == null)
            {
                return HttpNotFound();
            }
            return View(empleados);
        }

        // POST: empleados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string cedulaPk)
        {
            empleados empleados = db.empleados.Find(cedulaPk);
            db.empleados.Remove(empleados);
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



        public List<empleados> GetFreeEmployees()
        {
            List<empleados> employeesList = db.empleados.Where(x => x.disponibilidad == true).ToList();
            return employeesList;
        }

        public List<empleados> GetFreeEmployeesNotInProyect(int codigoProyecto)
        {
            List<empleados> lis1 = db.empleados.Where(x => x.disponibilidad == true).ToList();
            var list2 = GetEveryEmployeeByProyect(codigoProyecto);
            var employeesList = lis1.Except(list2).ToList();

            return employeesList;
        }



        public empleados GetEmployee(string cedula)
        {
            empleados employee = db.empleados.Find(cedula);
            return employee;
        }


        public List<empleados> GetEmployeeByKnowledge(string conoc)
        {
            db.Configuration.ProxyCreationEnabled = false;

            var listaEmp = (from d in db.empleados
                            join f in db.conocimientos
                            on d.cedulaPK equals f.cedulaEmpleadoFK
                            where f.conocimientoPK == conoc && d.disponibilidad == true
                            select d).ToList();



            return listaEmp;


        }


        public List<empleados> GetEmployeeByProyect(int codigo)
        {
            var listaEmpPr = (from d in db.empleados
                              join f in db.roles
                              on d.cedulaPK equals f.cedulaFK
                              where f.codigoProyectoFK == codigo && f.rol != "Líder"
                              select d).ToList();



            return listaEmpPr;
        }


        public List<empleados> GetEveryEmployeeByProyect(int codigo)
        {
            var listaEmpPr = (from d in db.empleados
                              join f in db.roles
                              on d.cedulaPK equals f.cedulaFK
                              where f.codigoProyectoFK == codigo
                              select d).ToList();



            return listaEmpPr;
        }

        public List<empleados> Pass()//dispone la lista de proyectos para otros controladores
        {
            List<empleados> empleados = db.empleados.ToList();
            return empleados;
        }
    }
}
