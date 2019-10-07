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
            TempData["conocimientos"] = conocimientos;
            TempData.Keep();
            return RedirectToAction("Lista", "equipos");
        }


        // GET: equipos
        public ActionResult Lista()
        {
            // List<proyecto> proyectos = TempData["proyectos"] as List<proyecto>;
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
             List<roles> rol =  new rolesController().UpdateRol();
             
             */
        }
    }
}