using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProyectoIntegrador_mejorado.Models;
using Microsoft.AspNet.Identity;

namespace ProyectoIntegrador_mejorado.Controllers
{
    [Authorize]
    public class proyectosController : Controller
    {
        private Gr02Proy1Entities db = new Gr02Proy1Entities();

        // GET: proyectos
        [Authorize(Roles = "Soporte, JefeDesarrollo, Lider, Desarrollador, Cliente")]
        public ActionResult Index()
        {
            /*Para relacionar la persona loggeada con su respectiva instancia en la bd,
             * con esto sabemos a que Rol pertenece
             * y facilitamos el acceso correspondiente con los condicionales de abajo
             */
            var user = User.Identity.GetUserName();
            var emple = new empleadosController().ExistEmail(user);
            var clien = new clientesController().ExistEmail(user);

            if (emple.Count() > 0)   //es empleado
            {
                //Se obtiene la cedula del empleado
                var cedula = emple[0].cedulaPK;
                //se buscan los proyectos donde participa el empleado con la cedula
                var proyectos = (from d in db.proyectos
                                 join f in db.roles
                                 on d.codigoPK equals f.codigoProyectoFK
                                 where f.cedulaFK == cedula
                                 select d).ToList();
                return View(proyectos.ToList());
            }
            else if (clien.Count() > 0) // es cliente
            {
                //Obtenemos al cliente segun el username dela bd clientes
                var existe = db.clientes.Where(w => w.correo == user);
                // buscamos un proyecto asignado al cliente pero ahora segun su cedula
                var proyectos = db.proyectos.Where(p => existe.Any(w => w.cedulaPK == p.cedulaClienteFK));
                //var proyectos = db.proyectos.Include(p => p.clientes).Where(p => p.cedulaClienteFK == clien[0].cedulaPK);
                //var proyectos = db.proyectos.Include(p => p.clientes);
                return View(proyectos.ToList());
            }
            else  //es jefe de desarrollo o soporte
            {
                var proyectos = db.proyectos.Include(p => p.clientes);
                return View(proyectos.ToList());
            }
        }

        // GET: proyectos/Details/5
        //Metodo limitado a estos Roles
        [Authorize(Roles = "Soporte, JefeDesarrollo, Lider, Desarrollador, Cliente")]
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
            ViewBag.lider = "";
            /*Si el proyecto tiene líder se envía su nombre a la vista de detalles*/
            empleados lider = new empleadosController().GetEmployee(new rolesController().getLiderId(id.Value));
            if (lider != null)
                ViewBag.lider = lider.nombre;
            return View(proyectos);
        }

        // GET: proyectos/Create
        //Metodo limitado a estos Roles
        [Authorize(Roles = "Soporte, JefeDesarrollo")]
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
                if (proyectos.cedulaLider != null)
                    new rolesController().AddRol(proyectos.codigoPK, proyectos.cedulaLider, 0);
                return RedirectToAction("Index");
            }

            ViewBag.cedulaClienteFK = new SelectList(db.clientes, "cedulaPK", "nombre", proyectos.cedulaClienteFK);
            ViewBag.cedulaLider = new SelectList(new empleadosController().GetFreeEmployees(), "cedulaPK", "nombre", proyectos.cedulaLider);
            return View(proyectos);
        }

        // GET: proyectos/Edit/5
        //Metodo limitado a estos Roles
        [Authorize(Roles = "Soporte, JefeDesarrollo")]
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
            /* Se envía el nombre del líder para mostrarlo en el dropdownlist */
            empleados lider = new empleadosController().GetEmployee(new rolesController().getLiderId(id.Value));
            if (lider != null)
            {
                ViewBag.hayLider = true;
                ViewBag.lider = lider.nombre;
            }
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
        //Metodo limitado a estos Roles
        [Authorize(Roles = "Soporte, JefeDesarrollo")]
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

        //Despliega un proyecto específico para ser mostrado en lista
        public List<proyectos> PassByCode(int code)
        {
            List<proyectos> projectList = db.proyectos.Where(p => p.codigoPK == code).ToList();
            return projectList;    
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

        /*
         * Efecto: devuelve un bool indicando si el proyecto tiene fecha de finalizacion
         * Requiere: NA
         * Modifica: NA
         */
        public bool Ended(int cod)
        {
            bool resp = false;
            proyectos proy = db.proyectos.Find(cod);
            if (proy.fechaFinal != null)
                resp = true;
            //TempData["proyectos"] = proyectos;
            //TempData.Keep();
            return resp;
        }

        /*
         * Efecto: devuelve los proyectos que lidero un empleado
         * Requiere: NA
         * Modifica: NA
         */
        public List<proyectos> GetLidetedProyects(string cedula)
        {
            var proyectos = (from d in db.proyectos
                             join f in db.roles
                             on d.codigoPK equals f.codigoProyectoFK
                             where f.cedulaFK == cedula && f.rol == "Líder"
                             select d).ToList();
            return proyectos;
        }

        public List<proyectos> GetLiderProyectoActual(string cedula)
        {
            var proyectos = (from d in db.proyectos
                             join f in db.roles
                             on d.codigoPK equals f.codigoProyectoFK
                             where(( f.cedulaFK == cedula && f.rol == "Líder" ) && d.fechaFinal == null)
                             select d).ToList();
            return proyectos;
        }

        /*
         * Efecto: devuelve los proyectos de un cliente
         * Requiere: NA
         * Modifica: NA
         */
        public List<proyectos> ProyectsByClient(string cedula)
        {
            var proyectos = (from d in db.proyectos
                             where d.cedulaClienteFK == cedula
                             select d).ToList();
            return proyectos;
        }

        /*
         * Efecto: devuelve los proyectos en que participa un empleado
         * Requiere: NA
         * Modifica: NA
         */
        public List<proyectos> ProyectsByEmployee(string cedula)
        {
            var proyectos = (from d in db.proyectos
                             join f in db.roles
                             on d.codigoPK equals f.codigoProyectoFK
                             where f.cedulaFK == cedula
                             select d).ToList();
            return proyectos;
        }
    }
}
