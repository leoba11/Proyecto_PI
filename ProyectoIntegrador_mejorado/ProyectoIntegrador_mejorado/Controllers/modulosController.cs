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
//------------------------------------------------------------------------------
// @author: María José Aguilar Barboza B60115
// @Date: 09/11/19
// Este código corresponde al controlador de módulos
// Este código en su mayoría fue autogenerado.
//------------------------------------------------------------------------------
namespace ProyectoIntegrador_mejorado.Controllers
{
    [Authorize] //esto corrresponde a una función de seguridad, inidica que sólo puede acceder a este módulo, usuarios que ya se hayan registrado en el sistema.
    public class modulosController : Controller
    {
        private Gr02Proy1Entities db = new Gr02Proy1Entities();

        //Esta método es llamado para desplegar el dropdown de selección del proyecto al cual se le quiere consultar sus módulos
        public ActionResult Index()
        {
            var user = User.Identity.GetUserName();
            var emple = new empleadosController().ExistEmail(user);
            var clien = new clientesController().ExistEmail(user);

            if (emple.Count() > 0)   //es empleado
            {
                //Se obtiene la cedula del empleado
                var cedula = emple[0].cedulaPK;
                //se buscan los proyectos donde participa el empleado con la cedula
                var proyectos = new proyectosController().ProyectsByEmployee(cedula);
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
                TempData["proyectos"] = proyectos;
                TempData.Keep();
                return View();
            }
            else  //es jefe de desarrollo o soporte
            {
                List<proyectos> proyectos = new proyectosController().Pass();
                TempData["proyectos"] = proyectos;
                TempData.Keep();
                return View();
            }
            
        }

        [HttpPost]
        public ActionResult Index(proyectos proyectito)
        {
            //Aquí se seleccionan solo los módulos relacionados con el proyecto que el usuario previamente seleccionó
            if (proyectito.codigoPK != 0) //si el cóidgo de proyecto es diferente de 0 
            {
                TempData["proyecto"] = proyectito.codigoPK; //aquí se obtiene el código del proyecto
                //Aquí se llama al controlador de proyectos, para que devuelva el nombre del proyecto cuyo código es el que se obtuvo en la línea anterior
                TempData["nombreProyecto"] = new proyectosController().ProjectByCode(int.Parse(TempData["proyecto"].ToString())).nombre;
                TempData.Keep(); //hacemos que mantenga los datos temporales
                return RedirectToAction("Lista", "modulos"); //se redirecciona a la pagina de que lista los módulos
            }
            else
            {
                return View(); //si el código del proyecto es 0, se vuelve a la página de selección de proyecto
            }
        }

        public ActionResult Lista()
        {
            //Este método despliega los datos de los modulos del proyecto que el usuario seleccionó
            //el método toma el código del proyecto, para desplegar solo los módulos correspondientes
            TempData.Keep(); //pedimos que mantenga los datos temporales
            if (TempData["proyecto"] != null)
            {
                int codigo = int.Parse(TempData["proyecto"].ToString()); //se obtiene el código del proyecto
                List<modulos> modulos = db.modulos.Where(x => x.codigoProyectoFK == codigo).ToList(); //se buscan los módulos cuyo código de proyecto sea igual al obtenido en la línea anterior
                TempData["modulos"] = modulos; //la lista de módulos se pone en los datos temporales
                return View(); //se envían para ser desplegados
            }
            else
            { //si sucede un error, se redirecciona a la página de seleccionar proyecto
                return RedirectToAction("Index", "modulos");
            }
        }


        // GET: modulos/Details/5
        //Este método es llamado cuando se solicita ver los detalles de un módulo específico
        [Authorize(Roles = "Soporte, JefeDesarrollo, Lider, Desarrollador")]//esto es parte de seguridad, indica que solo esos roles pueden acceder a esta opción, es decir, visualizar detalles de los módulos
        public ActionResult Details(int? codProyecto, int? id)
        {
            //sus parametros son el código de proyecto y el código del módulo
            TempData.Keep(); //pedimos que se mantegan los datos temporales (esto más que nada, para poder desplegar el nombre del proyecto sobre el cual se esta operando)
            if (codProyecto == null || id == null) //si alguno de los parámetros es nulo
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest); //se envía a página de error
            }
            modulos modulos = db.modulos.Find(codProyecto, id); //se llama a la función Find(), que busca dentro de la base de datos, el módulo específico que contiene los códigos (parámentros ingresados)
            if (modulos == null) //si lo que devuelve la base de datos es nulo, o sea, no devuelve nada
            {
                return HttpNotFound(); //se envía a página de error
            }
            return View(modulos); //sino, se muestran los detalles del módulo específico.
        }

        // GET: modulos/Create
        [Authorize(Roles = "Soporte, JefeDesarrollo, Lider")]//esto es parte de seguridad, idica que solo esos roles pueden crear módulos
        public ActionResult Create()
        {
            var mod = new modulos(); //se crea una nueva varable de tipo módulos
            TempData.Keep(); //se le solita que conserve los datos temporales
            if (TempData["proyecto"] != null) // si los datos temporales del proyecto no son nulos
            {
                int codigo = int.Parse(TempData["proyecto"].ToString()); //se obtiene el código del proyecto
                //se le solicita al controlador de proyectos, que pase el proyecto cuyo código corresponde al de la línea anterior, es decir codigoPK = codigo
                ViewBag.codigoProyectoFK = new SelectList(new proyectosController().PassByCode(codigo), "codigoPK", "nombre");
                return View(mod);//se muestra en la vista todo lo que se obtuvo en las líneas anteriores
            }
            else //si és nulo, se redirige a la vista de selección de proyectos
            {
                return RedirectToAction("Lista", "modulos");
            }
        }

        // POST: modulos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "codigoProyectoFK,nombre,descripcion")] modulos modulos)
        {
            //el bind lo que hace es mandar los datos recolectados a la base de datos
            if (ModelState.IsValid)//si lo que se trata de enviar cumple con las reglas 
            {
                db.modulos.Add(modulos); //se hace una especie de solicitud de "insert"
                db.SaveChanges(); //se "inserta"
                return RedirectToAction("Lista"); //hacemos que se redireccione a la página del listado de módulos
            }

            TempData.Keep(); //hacemos que se mantengan los datos temporales, esto más que nada para la redirección y despliegue de títulos que dependen de estos datos temporales
            if (TempData["proyecto"] != null) //si no es nulo el proyecto
            {
                int codigo = int.Parse(TempData["proyecto"].ToString()); //se obtiene código 
                ViewBag.codigoProyectoFK = new SelectList(new proyectosController().PassByCode(codigo), "codigoPK", "nombre", modulos.codigoProyectoFK); //se le solicita al controlador que devuelva el proyecto en el que se está
                return View(modulos); //se envían a la vista
            }
            else//si es nulo, se redirecciona de igual manera a la lista
            {
                return RedirectToAction("Lista", "modulos");
            }
        }

        // GET: modulos/Edit/5
        [Authorize(Roles = "Soporte, JefeDesarrollo, Lider")]//parte de seguridad, hace que solo estos roles puedan editar un módulo 
        public ActionResult Edit(int? codProyecto, int? id)
        {
            //se reciben como parámetros los códigos de proyecto y módulo
            TempData.Keep();//se le pide mantener los datos temporales, por motivos de despliegue de títulos que dependen de estos datos
            if (codProyecto == null || id == null) //se revisa si los parámetros recibidos son nulos
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest); //si es así se envía a pagina de error
            }
            modulos modulos = db.modulos.Find(codProyecto, id); //sino se pide buscarlos 
            if (modulos == null) //si lo que se devuleve después de la búsqueda es nulo
            {
                return HttpNotFound(); //página de error
            }
            //Si todo sale bien, se ingresa a la vista de edición de ese módulo específico
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
            //con bind se envían todos los valores correspoendientes a los atributos a la base de datos
            TempData.Keep();//se solicita mantener datos temporales, para despleigue de títulos que depende de ellos
            if (ModelState.IsValid) //si lo que se envía está correcto y no viola nada
            {
                db.Entry(modulos).State = EntityState.Modified;
                db.SaveChanges(); //se guardan los cambios en la base de datos
                return RedirectToAction("Lista");
            }
            //se devuelve a la página donde se listan los módulos
            ViewBag.idPK = new SelectList(db.modulos, "idPK", "id", modulos.idPK);
            return View(modulos);


        }

        // GET: modulos/Delete/5
        [Authorize(Roles = "Soporte, JefeDesarrollo, Lider")]
        public ActionResult Delete(int? codProyecto, int? id)
        {
            TempData.Keep();//se solicita mantener datos temporales, para despleigue de títulos que depende de ellos
            if (codProyecto == null || id == null)//se revisa si los parámetros recibidos son nulos
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest); //se manda a página de error
            }
            modulos modulos = db.modulos.Find(codProyecto, id); //se manda a buscar el módulo específico
            if (modulos == null) //si es nulo
            {
                return HttpNotFound(); //página de error
            }
            return View(modulos); //sino se manda a desplega la página de confirmación de eliminación
        }

        // POST: modulos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int codProyecto, int? id)
        {
            modulos modulos = db.modulos.Find(codProyecto, id); //se manda a buscar nuevamente ese módulo específico que se desea borrar
            db.modulos.Remove(modulos); //se manda a eliminar desde la base de datos
            db.SaveChanges();//se guardan los cambios
            return RedirectToAction("Lista"); //se redirecciona a la pagina del listado de módulos.
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

        /* Despliega la lista de proyectos (es para ser llamado por otros controladores) */
        public List<modulos> Pass()
        {
            List<modulos> modulos = db.modulos.ToList(); //se pasan a formato de lista todos los módulos
            return modulos;
        }

        //Este método es utilizado por otros controladores, para obtener un módulo específico
        public modulos ModByCode(int projectCode, int id)
        {
            modulos mod = db.modulos.Find(projectCode, id); //se obtiene el módulo con el código de proyecto y propio, proporcionado en parámetros
            return mod; //se devuelve ese módulo
        }

        //Despliega una lista de módulos según el código (para ser usado por otros controladores)
        public List<modulos> PassByCode(int code)
        {
            List<modulos> modulos = db.modulos.Where(p => p.idPK == code).ToList(); //se buscan y se gurandan como lista los módulos cuyo código sea el igual al que se recibió por parámetro
            return modulos;//se devuelve esa lista
        }

        //Despliega la lista de modulos por proyecto (para ser usado por otros controladores)
        public List<modulos> PassByProyect(int code)
        {
            List<modulos> modulos = db.modulos.Where(x => x.codigoProyectoFK == code).ToList(); //se busca y guarda en lista, los módulos cuyo código de proyecto es igual al recibido por parámetro
            return modulos;//se devuleve la lista
        }


    }
}