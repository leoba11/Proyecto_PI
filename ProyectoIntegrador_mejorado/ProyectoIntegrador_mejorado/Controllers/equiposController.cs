using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoIntegrador_mejorado.Models;
namespace ProyectoIntegrador_mejorado.Controllers
{
    [Authorize(Roles = "Soporte, JefeDesarrollo, Lider")]
    public class equiposController : Controller
    {
        //EFE: trae los datos necesarios para equipos y llama el metodo para seleccionar el proyecto
        //REQ: 
        //MOD: crea variables temporales para guardar los empleados libres, los proyectos y la lista de conocimientos
        public ActionResult Index()
        {
            List<proyectos> proyectos = new proyectosController().Pass();
            List<empleados> employeesList = new empleadosController().GetFreeEmployees();
            List<conocimientos> conocimientos = new conocimientosController().PassKnowledge();
            TempData["proyectos"] = proyectos;
            TempData["empleados"] = employeesList;
            TempData["empleados2"] = employeesList;
            TempData["conocimientos"] = conocimientos;
            TempData.Keep();
            return RedirectToAction("SelectProject", "equipos");
        }


        //EFE: trae la lista de empleados filtrados por conocimiento y por proyecto y regresa la vista de los mismos
        //REQ: debe exitir al menos un proyecto
        //MOD: crea variables temporales para guardar la lista de empleados filtrados por conocimiento y por proyecto
        public ActionResult Lista(string conocimientoPK)
        {
            // List<proyecto> proyectos = TempData["proyectos"] as List<proyecto>;
            if (conocimientoPK != "todos")
            {
                List<empleados> employeesList = new empleadosController().GetEmployeeByKnowledge(conocimientoPK);
                TempData["empleadosK"] = employeesList;
            }
            else
            {
                List<empleados> employeesList = new empleadosController().GetFreeEmployees();
                TempData["empleadosK"] = employeesList;
            }
            ViewBag.know = conocimientoPK;
            if (TempData["proyecto"] != null)
            {
                List<empleados> employeesList2 = new empleadosController().GetEmployeeByProyect(int.Parse(TempData["proyecto"].ToString()));
                TempData["empleadosP"] = employeesList2;


                string liderID = new rolesController().getLiderId(int.Parse(TempData["proyecto"].ToString()));
                empleados lider = new empleadosController().GetEmployee(liderID);

                TempData["lider"] = lider;
                TempData.Keep();


                return View();
            }
            else
            {
                TempData.Keep();
                return RedirectToAction("Index", "equipos");
            }

        }

        //EFE: valida el conocimiento del filtro y vuelve a la vista de equipos con dicho valor
        //REQ: debe exitir al menos un proyecto
        //MOD: 
        [HttpPost]
        public ActionResult Lista(conocimientos knowledge)
        {
            if (knowledge.conocimientoPK != null)
            {
                TempData.Keep();
                return RedirectToAction("Lista", "equipos", new { conocimientoPK = knowledge.conocimientoPK });
            }
            else
            {
                TempData.Keep();
                return RedirectToAction("Lista", "equipos", new { conocimientoPK = "todos" });
            }

        }




        //EFE: trae y presenta los datos correpondientes para un empleado en especifico 
        //REQ: que el empleado seleccionado sea valido
        //MOD:
        public ActionResult Details(string cedula)
        {
            TempData.Keep();
            empleados em = new empleadosController().GetEmployee(cedula);
            TempData["empleado"] = em;
            TempData.Keep();
            return View();
        }


        //EFE: regresa una vista para seleccionar el proyecto
        //REQ:
        //MOD:
        public ActionResult SelectProject()
        {
            TempData.Keep();
            return View();
        }

        //EFE: valida que se haya seleccionado un proyecto
        //REQ:
        //MOD: guarda el codigo del proyecto en una variable temporal y trae el nombre
        [HttpPost]
        public ActionResult SelectProject(proyectos proyectito)
        {
            //TempData["proyecto"] = proyectito.codigoPK;
            if (proyectito.codigoPK != 0)
            {
                string know = "----------";
                TempData["proyecto"] = proyectito.codigoPK;
                TempData["nombreProyecto"] = new proyectosController().ProjectByCode(int.Parse(TempData["proyecto"].ToString())).nombre;
                TempData.Keep();
                return RedirectToAction("Lista", "equipos", new { conocimientoPK = know });
            }
            else
            {
                return View();
            }
        }

        /*
        //EFE:
        //REQ:
        //MOD:
        public ActionResult Refresh(int codProyecto)
        {
            TempData.Keep();

            


            return PartialView("empleadosPartial");
        }
        
        public ActionResult UpdateItem(string itemIds)
        {
            Gr02Proy1Entities db = new Gr02Proy1Entities();
            int count = 1;
            List<int> itemIdList = new List<int>();
            itemIdList = itemIds.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            foreach (var itemId in itemIds)
            {
                try
                {
                    empleados item = db.empleados.Where(x => x.cedulaPK == itemId.ToString()).FirstOrDefault();
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    continue;
                }
                count++;
            }
            return Json(true, JsonRequestBehavior.AllowGet);


            
             List<roles> rol =  new rolesController().UddeRol();

            List<roles> rol =  new rolesController().EraseeRol();
             
             
        }
        //EFE:
        //REQ:
        //MOD:
        public JsonResult GetEmployees3(string conocimientoPK)
        {
            List<empleados> employeesList = new empleadosController().GetEmployeeByKnowledge(conocimientoPK);
            //TempData["empleados"] = employeesList;
            //TempData.Keep();
            return Json(employeesList, JsonRequestBehavior.AllowGet);
        }

        //EFE:
        //REQ:
        //MOD:
        public JsonResult GetEmployees(string conocimientoPK)
        {
            List<empleados> employeesList = new empleadosController().GetEmployeeByKnowledge(conocimientoPK);
            TempData["empleados"] = employeesList;
            TempData.Keep();
            return Json(employeesList, JsonRequestBehavior.AllowGet);
        }
        */

    }
}
