using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ProyectoIntegrador_mejorado.Models;

namespace ProyectoIntegrador_mejorado.Controllers
{
    [Authorize]//esto corrresponde a una función de seguridad, inidica que sólo puede acceder a este módulo, usuarios que ya se hayan registrado en el sistema.
    public class requerimientosController : Controller
    {
        private Gr02Proy1Entities db = new Gr02Proy1Entities();

        // GET: requerimientos
        //Esta método es llamado para desplegar el dropdown de selección del proyecto y módulo al cual se le quiere consultar sus requerimientos
        public ActionResult Index()
        {

            var user = User.Identity.GetUserName();
            var emple = new empleadosController().ExistEmail(user);
            var clien = new clientesController().ExistEmail(user);

            if (emple.Count() > 0)   //es empleado
            {
                var cedula = emple[0].cedulaPK;
                //se buscan los proyectos donde participa el empleado con la cedula
                var proyectos = new proyectosController().ProyectsByEmployee(cedula);
                ViewBag.ProyectList = new SelectList(proyectos, "codigoPK", "nombre");
                TempData["proyectos"] = proyectos;
                TempData.Keep();
                return View();
            }
            else if (clien.Count() > 0) // es cliente
            {
                //Se obtiene la cedula del cliente
                var cedula = clien[0].cedulaPK;
                // buscamos un proyecto asignado al cliente pero ahora segun su cedula
                var proyectos = new proyectosController().ProyectsByClient(cedula);
                ViewBag.ProyectList = new SelectList(proyectos, "codigoPK", "nombre");
                TempData["proyectos"] = proyectos;
                TempData.Keep();
                return View();
            }
            else  //es jefe de desarrollo o soporte
            {
                List<proyectos> proyectos = new proyectosController().Pass();
                ViewBag.ProyectList = new SelectList(proyectos, "codigoPK", "nombre");
                TempData["proyectos"] = proyectos;
                TempData.Keep();
                return View();
            }




            /*

            List<proyectos> proyectos = new proyectosController().Pass(); //se comunica con el controlador de proyectos para que le la lista de proyectos
            ViewBag.ProyectList = new SelectList(proyectos, "codigoPK", "nombre"); //contiene la lista de proyectos
            TempData["proyectos"] = proyectos; //se alacena en esa variable de datos temporales el proyecto seleccionado
            TempData.Keep();//se le pide mantener esos datos temporales
            return View(); //se envía a la vista*/
        }
        [HttpPost]
        public ActionResult Index(proyectos proyectito, modulos modulito)
        {
            //Aquí solo selecciona los requerimientos relacionados con el proyecto y módulo que el usuario escogio
            if (proyectito.codigoPK != 0 && modulito.idPK != 0) //si los parámetros no son igual a 0
            {
                TempData["proyecto"] = proyectito.codigoPK; //se obtiene el código del proyecto
                TempData["nombreProyecto"] = new proyectosController().ProjectByCode(int.Parse(TempData["proyecto"].ToString())).nombre; //se obtiene el nombre a partir del código anterior
                TempData["modulos"] = modulito.idPK;//se obtiene el código del módulo

                try
                {//se comunica con el controlador de módulos para que le pase los nombres de módulos asociados con el proyecto y código de módulo seleccioando
                    TempData["nombreModulo"] = new modulosController().ModByCode(int.Parse(TempData["proyecto"].ToString()), int.Parse(TempData["modulos"].ToString())).nombre;
                }
                catch (NullReferenceException)
                {
                    TempData.Keep(); //se le solicita mantener los datos nuevamente
                    return RedirectToAction("Index", "requerimientos");//si ocurre error se redirige a página de selección
                }

                TempData.Keep(); //se le solicita mantener los datos nuevamente
                return RedirectToAction("Lista", "requerimientos");//se redirecciona a la vista del listado de requerimientos
            }
            else //si los parámetros son igual a 0
            {
                return View(); //se recarga la vista
            }
        }


        //Este método es utilizado en el dropdown en cascada de selección de proyecto y módulo
        public ActionResult GetModulList(int codigoProyecto)
        {
            List<modulos> modulos = new modulosController().PassByProyect(codigoProyecto);//se comunica con el controlador de módulos para que pase el listado de módulos de acuerdo al proyecto
            ViewBag.Moduls = new SelectList(modulos, "idPK", "nombre"); //ese listado se guarda en esta "vista"

            TempData.Keep(); //se le solicita mantener los datos nuevamente
            return PartialView("ModulsPartial"); //se devuelve estos valores obtenidos a la vista parcial
        }


        public ActionResult Lista()
        {
            //Se agrega este método para deplegar los datos de los requerimientos del proyecto y módulo que el usuario seleccionó
            //el método agarra el id del proyecto y id módulo  para desplegar entonces solo los requerimientos correspondientes
            TempData.Keep();//se le solicita mantener los datos temporales por motivos de títulos que dependen de estos datos
            if (TempData["proyecto"] != null && TempData["modulos"] != null)//si no son nulos
            {
                int codigo = int.Parse(TempData["proyecto"].ToString()); //se obtiene el código del proyecto
                int idMod = int.Parse(TempData["modulos"].ToString()); //se obtiene el código del módulo
                List<requerimientos> requerimientos = db.requerimientos.Where(x => x.codigoProyectoFK == codigo && x.idModuloFK == idMod).ToList(); //se obtienen los requermientos en donde se cumple que se tiene esa combinación de códigos
                TempData["requerimientos"] = requerimientos; //se alamacenan esos requerimientos obtenidos
                return View(); //se manda a la vista
            }
            else //si son nulos
            {
                return RedirectToAction("Index", "requerimientos"); //se redirecciona ala página de selección
            }
        }


        // GET: requerimientos/Details/5
        //Este método permite revisar los detalles de un requerimiento específico
        [Authorize(Roles = "Soporte, JefeDesarrollo, Lider, Desarrollador")] //parte de seguridad, indica que solo esos roles pueden ver detallles de un requerimiento
        public ActionResult Details(int? idProyecto, int? idModulo, int? id)
        {
            TempData.Keep();//se solicita mantener esos datos, por ótivos de títulos que dependen de los mismos (ej: titulo de proyecto y módulo sobre el cual se está trabajando)
            if (idProyecto == null || idModulo == null || id == null) //si los parámetros son nulos
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest); //se devuelve a página de error
            }
            requerimientos requerimientos = db.requerimientos.Find(idProyecto, idModulo, id); //se manda a buscar el requerimiento especificado
            if (requerimientos == null) //si lo que delvolvió es nulo
            {
                return HttpNotFound(); //página de error
            }
            return View(requerimientos);//se manda a la vista el requerimiento
        }

        // GET: requerimientos/Create
        [Authorize(Roles = "Soporte, JefeDesarrollo, Lider")]//parte de seguridad, indica que solo esos roles pueden crear requerimientos
        public ActionResult Create()
        {

            var estados = GetAllStates();//se llama la función que obtiene el listado de todos los estados que puede tener un requerimiento y el resultado se guarda en la var estados
            var req = new requerimientos(); //se crea un nuevo requerimiento

            // Establece estos estados en el modelo. 
            // Se debe hacer esto ya que solo el valor seleccionado de DropDownList se visualiza de nuevo, no toda lista de estados.
            req.estados = GetSelectListItems(estados);

            TempData.Keep();//se pide mantener los datos temporales, por títulos que dependen de ellos en las vistas
            if (TempData["proyecto"] != null && TempData["modulos"] != null)//si no son nulos
            {
                int codigo = int.Parse(TempData["proyecto"].ToString());//se obtiene el código de proyecto
                int idMod = int.Parse(TempData["modulos"].ToString());//se botiene el código de módulo

                ViewBag.cedulaEmpleadoFK = new SelectList(new empleadosController().GetFreeEmployees(), "cedulaPK", "nombre"); //se comunica con el controlador de empleados para que solo se muestren los empleados disponibles
                ViewBag.codigoProyectoFK = new SelectList(new proyectosController().PassByCode(codigo), "codigoPK", "nombre");//se comunica con el controlador de proyectos, para que solo se mueste el proyecto en el que se está
                ViewBag.idModuloFK = new SelectList(new modulosController().PassByCode(idMod), "idPK", "nombre");//se comunica con el controlador de módulos para que solo muestre el módulo  en el que se está
                return View(req); //se manda todo eso a la vista
            }
            else //si son nulos
            {
                return RedirectToAction("Lista"); //se redirige a la vista del listado de requerimeintos
            }
        }

        // POST: requerimientos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "codigoProyectoFK,idModuloFK,idPK,descripcion,complejidad,estado,cedulaEmpleadoFK,fechaInicio,fechaFin,duracionEstimada,duracionDias,nombre")] requerimientos requerimientos)
        {
            //El bind hace que se prepare todo para mandarse a la base de datos
            // De nuevo se obtienen todos los estados
            var estados = GetAllStates();

            // Establece estos estados en el modelo. 
            // Se debe hacer esto ya que solo el valor seleccionado de DropDownList se visualiza de nuevo, no toda lista de estados.
            requerimientos.estados = GetSelectListItems(estados);


            if (ModelState.IsValid)//si todo está bien y no se viola ninguna regla
            {
                db.requerimientos.Add(requerimientos); //se "inserta" en la base de datos
                db.SaveChanges(); //se guardan los cambios
                return RedirectToAction("Lista"); //se redirige ala vista del listado de requerimientos
            }
            TempData.Keep();//se solicita mantener los datos por títulos en las vistas que dependen de estos
            if (TempData["proyecto"] != null && TempData["modulos"] != null) //si no son nulos
            {
                int codigo = int.Parse(TempData["proyecto"].ToString()); //se obtiene el código del proyecto
                int idMod = int.Parse(TempData["modulos"].ToString()); //se obtiene el código del módulo

                ViewBag.cedulaEmpleadoFK = new SelectList(new empleadosController().GetFreeEmployees(), "cedulaPK", "nombre", requerimientos.cedulaEmpleadoFK); //se seleccionan solo los empleados disponibles
                ViewBag.codigoProyectoFK = new SelectList(new proyectosController().PassByCode(codigo), "codigoPK", "nombre", requerimientos.codigoProyectoFK); //se selecciona solo el proyecto actual
                ViewBag.idModuloFK = new SelectList(new modulosController().PassByCode(idMod), "idPK", "nombre", requerimientos.idModuloFK);//se selecciona solo el módulo actual
                return View(requerimientos); //se envía eso a la vista
            }
            else
            {//si son nulos 
                return RedirectToAction("Lista");//se redirige a lavista del listado de requerimientos
            }
        }
        // Delvuele el listado de todos los posibles estados de un  requerimiento
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

        // Esta función toma una lista de cadenas y devuelve una lista de objetos SelectListItem.
        // Estos objetos se utilizarán más adelante en la plantilla SignUp.html para representar el
        // La lista desplegable.
        private IEnumerable<SelectListItem> GetSelectListItems(IEnumerable<string> elements)
        {
            // Create an empty list to hold result of the operation
            var selectList = new List<SelectListItem>();

            // Para cada cadena en la variable 'elementos', cree un nuevo objeto SelectListItem
            // que tiene sus propiedades Value y Text establecidas en un valor particular.
            // Esto dará como resultado que MVC muestre cada elemento como:
            // <option value = "State Name"> Nombre del estado </option>
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
            //se reciben por parámetro el código de proyecto, módulo y requerimiento
            TempData.Keep(); //se solicita mantener  estos datos por los títlos de las vistas que dependen de ellos
            if (idProyecto == null || idModulo == null || id == null) //si alguno de los parámetros es nulo
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest); //página de error
            }
            requerimientos requerimientos = db.requerimientos.Find(idProyecto, idModulo, id); //se manda a buscar el requerimiento específico 
            if (requerimientos == null) //si es nulo
            {
                return HttpNotFound(); //página de error
            }
            var estados = GetAllStates(); //se obtienen de nuevo todos los estados de un requerimiento (esto se hace para el el dropdown sea editable)

            requerimientos.estados = GetSelectListItems(estados); //se vuelven a setear en el módelo
            ViewBag.cedulaEmpleadoFK = new SelectList(new empleadosController().GetFreeEmployees(), "cedulaPK", "nombre"); //hace que sólo se muestren los empelados que están disponibles
            return View(requerimientos);//se manda a la vista 
        }

        // POST: requerimientos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "codigoProyectoFK,idModuloFK,idPK,descripcion,complejidad,estado,cedulaEmpleadoFK,fechaInicio,fechaFin,duracionEstimada,duracionDias,nombre")] requerimientos requerimientos)
        {
            //El bind hace que todo se prepare para ser mandado a la base de datos
            TempData.Keep(); //se solicita mantener datos por los títulos de las vistas que dependen de ellos (ej: nombre de proyecto y módulo actual sobre los que se trabajan)
            if (ModelState.IsValid)//si todo está bien y no se viola ninguna restricción
            {
                db.Entry(requerimientos).State = EntityState.Modified;
                db.SaveChanges(); //se guardan los cambios en la base de datos
                return RedirectToAction("Lista"); //se redirige a la vista del listado de requerimientos
            }
            // Se obtienen de nuevo todos los estados de un requerimiento (esto se hace para el el dropdown sea editable)
            var estados = GetAllStates();

            requerimientos.estados = GetSelectListItems(estados); //se setean los estados en el modelo
            ViewBag.cedulaEmpleadoFK = new SelectList(new empleadosController().GetFreeEmployees(), "cedulaPK", "nombre", requerimientos.cedulaEmpleadoFK); //solo permite que se muestren los empelados disponibles
            return View(requerimientos); //se devuelve a la vista


        }

        // GET: requerimientos/Delete/5
        [Authorize(Roles = "Soporte, JefeDesarrollo, Lider")]
        public ActionResult Delete(int? idProyecto, int? idModulo, int? id)
        {
            TempData.Keep();//se mantienen los datos por títulos en las vistas que dependen de ellos
            if (idProyecto == null || idModulo == null || id == null)//si los parámetros recibidos son nulos
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);//página de error
            }
            requerimientos requerimientos = db.requerimientos.Find(idProyecto, idModulo, id); //se manda a buscar el requerimiento específico
            if (requerimientos == null)//si lo que devulve es nulo
            {
                return HttpNotFound();//página de error
            }
            return View(requerimientos); //se devulve a la vista
        }

        // POST: requerimientos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? idProyecto, int? idModulo, int? id)
        {
            requerimientos requerimientos = db.requerimientos.Find(idProyecto, idModulo, id); //se manda a buscar el requerimiento específico
            if (requerimientos.estado == "Cancelado")//solo si su estado es cancelado se procede a eliminar
            {
                db.requerimientos.Remove(requerimientos); //se remueve de la base de datos
                db.SaveChanges();//se guerdan los cambios
            }

            return RedirectToAction("Lista");//se redirige al listado de requerimientos. 
        }


        //Este método es para ser llamado por otro controlador. Devuelve True/False ante la pregunta si un determinado empleado tiene un requerimiento asignado
        public bool ExistEmployee(string cedula)
        {
            //Se devuelve un bool indicando si el empleado tiene algun requerimiento asignado
            TempData.Keep();
            bool resp = false;

            var listaReq = (from d in db.requerimientos
                            where d.cedulaEmpleadoFK == cedula && (d.estado != "Finalizado" && d.estado != "Cancelado")
                            select d).Count(); //se realiza la busqueda de aquellos requerimientos que no estén finalizados ni cacelados 
            if (listaReq != 0) //si su resultado es difetente de 0
                resp = true; //queire decir que hay un empleado asignado

            return resp; //se devulve la respuesta
        }

        /* Dispose es para liberar recursos "no administrados" (por ejemplo, sockets, identificadores de archivos, identificadores de mapa de bits, etc.), 
        y si se llama fuera de un finalizador  para eliminar otros objetos desechables que ya no son útiles. */
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