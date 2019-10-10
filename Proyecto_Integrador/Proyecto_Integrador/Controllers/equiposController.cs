using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Proyecto_Integrador.Models;

namespace Proyecto_Integrador.Controllers
{
    public class equiposController : Controller
    {
        // GET: equipos
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


        // GET: equipos
        public ActionResult Lista(string conocimientoPK)
        {
            // List<proyecto> proyectos = TempData["proyectos"] as List<proyecto>;
            List<empleados> employeesList = new empleadosController().GetEmployeeByKnowledge(conocimientoPK);
            List<empleados> employeesList2 = new empleadosController().GetEmployeeByProyect(int.Parse(TempData["proyecto"].ToString()));
            TempData["empleadosK"] = employeesList;
            TempData["empleadosP"] = employeesList2;
            TempData.Keep();


            return View();
        }

        // GET: modulos/Details/5
        public ActionResult Details(string cedula)
        {
            TempData.Keep();
            empleados em = new empleadosController().GetEmployee(cedula);
            TempData["empleado"] = em;
            TempData.Keep();
            return View();
        }



        public ActionResult SelectProject()
        {
            TempData.Keep();
            return View();
        }

        [HttpPost]
        public ActionResult SelectProject(proyectos proyectito)
        {
            //TempData["proyecto"] = proyectito.codigoPK;
            if (proyectito.codigoPK != null)
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


            /*
             List<roles> rol =  new rolesController().UddeRol();

            List<roles> rol =  new rolesController().EraseeRol();
             
             */
        }



        public JsonResult GetEmployees3(string conocimientoPK)
        {
            List<empleados> employeesList = new empleadosController().GetEmployeeByKnowledge(conocimientoPK);
            //TempData["empleados"] = employeesList;
            //TempData.Keep();
            return Json(employeesList, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetEmployees(string conocimientoPK)
        {
            List<empleados> employeesList = new empleadosController().GetEmployeeByKnowledge(conocimientoPK);
            TempData["empleados"] = employeesList;
            TempData.Keep();
            return Json(employeesList, JsonRequestBehavior.AllowGet);
        }



    }
}