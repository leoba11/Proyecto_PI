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
    public class requerimientosController : Controller
    {
        private Gr02Proy1Entities db = new Gr02Proy1Entities();

        // GET: requerimientos
        public ActionResult Index()
        {
            //var requerimientos = db.requerimientos.Include(r => r.empleados).Include(r => r.modulos);
            //return View(requerimientos.ToList());

            List<proyectos> proyectos = new proyectosController().Pass();
            ViewBag.ProyectList = new SelectList(proyectos, "codigoPK", "nombre");
            TempData["proyectos"] = proyectos;

            /*
            List<modulos> modulos = new modulosController().Pass();
            TempData["modulos"] = modulos;*/
            TempData.Keep();
            return View();
        }
        [HttpPost]
        public ActionResult Index(proyectos proyectito, modulos modulito)
        {
            //acá se cambió para que solo agarre los requerimientos relacionados con el proyecto que el usuario escogio
            if (proyectito.codigoPK != 0 && modulito.idPK != 0)
            {
                TempData["proyecto"] = proyectito.codigoPK;
                TempData["nombreProyecto"] = new proyectosController().ProjectByCode(int.Parse(TempData["proyecto"].ToString())).nombre;

                TempData["modulos"] = modulito.idPK;

                try
                {
                    TempData["nombreModulo"] = new modulosController().ModByCode(int.Parse(TempData["proyecto"].ToString()), int.Parse(TempData["modulos"].ToString())).nombre;
                }
                catch (NullReferenceException)
                {
                    return RedirectToAction("Index", "requerimientos");
                }

                TempData.Keep();
                return RedirectToAction("Lista", "requerimientos");
            }
            else
            {
                return View();
            }
        }


        public ActionResult GetModulList(int codigoProyecto)
        {
            List<modulos> modulos = new modulosController().PassByProyect(codigoProyecto);
            ViewBag.Moduls = new SelectList(modulos, "idPK", "nombre");

            return PartialView("ModulsPartial");
        }


        public ActionResult Lista()
        {
            //Se agrega este método para deplegar los datos de los modulos del proyecto que el usuario seleccionó
            //el método agarra el id del proyecto, para desplegar entonces solo los modulos correspondientes
            TempData.Keep();
            if (TempData["proyecto"] != null && TempData["modulos"] != null)
            {
                int codigo = int.Parse(TempData["proyecto"].ToString());
                int idMod = int.Parse(TempData["modulos"].ToString());
                List<requerimientos> requerimientos = db.requerimientos.Where(x => x.codigoProyectoFK == codigo && x.idModuloFK == idMod).ToList();
                TempData["requerimientos"] = requerimientos;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "requerimientos");
            }
        }


        // GET: requerimientos/Details/5
        [Authorize(Roles = "Soporte, JefeDesarrollo, Lider, Desarrollador")]
        public ActionResult Details(int? idProyecto, int? idModulo, int? id)
        {
            TempData.Keep();
            if (idProyecto == null || idModulo == null || id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            requerimientos requerimientos = db.requerimientos.Find(idProyecto, idModulo, id);
            if (requerimientos == null)
            {
                return HttpNotFound();
            }
            return View(requerimientos);
        }

        // GET: requerimientos/Create
        [Authorize(Roles = "Soporte, JefeDesarrollo, Lider")]
        public ActionResult Create()
        {

            //ViewBag.cedulaEmpleadoFK = new SelectList(db.empleados.Where(p => p.disponibilidad == false), "cedulaPK", "nombre");
            //ViewBag.codigoProyectoFK = new SelectList(db.proyectos, "codigoPK", "nombre");
            //ViewBag.idModuloFK = new SelectList(db.modulos, "idPK", "nombre");

            var estados = GetAllStates();
            var req = new requerimientos();
            // Set these states on the model. We need to do this because
            // only the selected value from the DropDownList is posted back, not the whole
            // list of states.
            req.estados = GetSelectListItems(estados);
            TempData.Keep();
            if (TempData["proyecto"] != null && TempData["modulos"] != null)
            {
                int codigo = int.Parse(TempData["proyecto"].ToString());
                int idMod = int.Parse(TempData["modulos"].ToString());

                ViewBag.cedulaEmpleadoFK = new SelectList(new empleadosController().GetFreeEmployees(), "cedulaPK", "nombre");
                ViewBag.codigoProyectoFK = new SelectList(new proyectosController().PassByCode(codigo), "codigoPK", "nombre");
                ViewBag.idModuloFK = new SelectList(new modulosController().PassByCode(idMod), "idPK", "nombre");
                return View(req);
            }
            else
            {
                return RedirectToAction("Lista");
            }
            //return View(req);
        }

        // POST: requerimientos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "codigoProyectoFK,idModuloFK,idPK,descripcion,complejidad,estado,cedulaEmpleadoFK,fechaInicio,fechaFin,duracionEstimada,duracionDias,nombre")] requerimientos requerimientos)
        {
           
            // Get all states again
            var estados = GetAllStates();
            // Set these states on the model. We need to do this because
            // only the selected value from the DropDownList is posted back, not the whole
            // list of states.
            requerimientos.estados= GetSelectListItems(estados);

            if (ModelState.IsValid)
            {
                db.requerimientos.Add(requerimientos);
                db.SaveChanges();
                return RedirectToAction("Lista");
            }
            TempData.Keep();
            if (TempData["proyecto"] != null && TempData["modulos"] != null)
            {
                int codigo = int.Parse(TempData["proyecto"].ToString());
                int idMod = int.Parse(TempData["modulos"].ToString());

                ViewBag.cedulaEmpleadoFK = new SelectList(new empleadosController().GetFreeEmployees(), "cedulaPK", "nombre", requerimientos.cedulaEmpleadoFK);
                ViewBag.codigoProyectoFK = new SelectList(new proyectosController().PassByCode(codigo), "codigoPK", "nombre", requerimientos.codigoProyectoFK);
                ViewBag.idModuloFK = new SelectList(new modulosController().PassByCode(idMod), "idPK", "nombre", requerimientos.idModuloFK);
                return View(requerimientos);
            }
            else {
                return RedirectToAction("Lista");
            }
        }
        // Just return a list of states - in a real-world application this would call
        // into data access layer to retrieve states from a database.
        private IEnumerable<string> GetAllStates()
        {
            return new List<string>
            {
                
                "No iniciado",
                "Cancelado",
                "Finalizado",
                "En curso",
            };
        }

        // This is one of the most important parts in the whole example.
        // This function takes a list of strings and returns a list of SelectListItem objects.
        // These objects are going to be used later in the SignUp.html template to render the
        // DropDownList.
        private IEnumerable<SelectListItem> GetSelectListItems(IEnumerable<string> elements)
        {
            // Create an empty list to hold result of the operation
            var selectList = new List<SelectListItem>();

            // For each string in the 'elements' variable, create a new SelectListItem object
            // that has both its Value and Text properties set to a particular value.
            // This will result in MVC rendering each item as:
            //     <option value="State Name">State Name</option>
            foreach (var element in elements)
            {
                selectList.Add(new SelectListItem
                {
                    Value = element,
                    Text = element
                });
            }

            return selectList;
        }

        // GET: requerimientos/Edit/5
        [Authorize(Roles = "Soporte, JefeDesarrollo, Lider, Desarrollador")]
        public ActionResult Edit(int? idProyecto, int? idModulo, int? id)
        {
            TempData.Keep();
            if (idProyecto == null || idModulo == null || id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            requerimientos requerimientos = db.requerimientos.Find(idProyecto, idModulo, id);
            if (requerimientos == null)
            {
                return HttpNotFound();
            }
            var estados = GetAllStates();
          
                
                requerimientos.estados = GetSelectListItems(estados);
                ViewBag.cedulaEmpleadoFK = new SelectList(new empleadosController().GetFreeEmployees(), "cedulaPK", "nombre");
                return View(requerimientos);
            

                // Set these states on the model. We need to do this because
                // only the selected value from the DropDownList is posted back, not the whole
                // list of states.
               
            
        }

        // POST: requerimientos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "codigoProyectoFK,idModuloFK,idPK,descripcion,complejidad,estado,cedulaEmpleadoFK,fechaInicio,fechaFin,duracionEstimada,duracionDias,nombre")] requerimientos requerimientos)
        {
            TempData.Keep();
            if (ModelState.IsValid)
            {
                db.Entry(requerimientos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Lista");
            }
            // Get all states again
            var estados = GetAllStates();

            // Set these states on the model. We need to do this because
            // only the selected value from the DropDownList is posted back, not the whole
            // list of states.
            
                
                requerimientos.estados = GetSelectListItems(estados);
                ViewBag.cedulaEmpleadoFK = new SelectList(new empleadosController().GetFreeEmployees(), "cedulaPK", "nombre", requerimientos.cedulaEmpleadoFK);
                return View(requerimientos);
            
            
        }

        // GET: requerimientos/Delete/5
        [Authorize(Roles = "Soporte, JefeDesarrollo, Lider")]
        public ActionResult Delete(int? idProyecto, int? idModulo, int? id)
        {
            TempData.Keep();
            if (idProyecto == null || idModulo == null || id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            requerimientos requerimientos = db.requerimientos.Find(idProyecto, idModulo, id);
            if (requerimientos == null)
            {
                return HttpNotFound();
            }
            return View(requerimientos);
        }

        // POST: requerimientos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? idProyecto, int? idModulo, int? id)
        {
            requerimientos requerimientos = db.requerimientos.Find(idProyecto, idModulo, id);
            if (requerimientos.estado == "Cancelado")
            {
                db.requerimientos.Remove(requerimientos);
                db.SaveChanges();
            }
           
            return RedirectToAction("Lista");
        }



        public bool ExistEmployee(string cedula)
        {
            //Se devuelve un bool indicando si el empleado tiene algun requerimiento asignado
            TempData.Keep();
            bool resp = false;

            var listaReq = (from d in db.requerimientos
                            where d.cedulaEmpleadoFK == cedula && (d.estado != "Finalizado" && d.estado != "Cancelado")
                            select d).Count();
            if (listaReq != 0)
                resp = true;

            return resp;
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
