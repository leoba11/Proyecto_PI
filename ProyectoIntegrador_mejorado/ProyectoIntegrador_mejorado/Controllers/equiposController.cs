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
            List<string> conocimientos = new conocimientosController().PassKnowledge();
            TempData["proyectos"] = proyectos;
            TempData["conocimientos"] = conocimientos;
            TempData.Keep();
            return RedirectToAction("SelectProject", "equipos");
        }


        //EFE: trae la lista de empleados filtrados por conocimiento y por proyecto y regresa la vista de los mismos
        //REQ: debe exitir al menos un proyecto
        //MOD: crea variables temporales para guardar la lista de empleados filtrados por conocimiento y por proyecto
        public ActionResult Lista(string conocimientoPK, string a)
        {
            List<empleados> employeesFree = new empleadosController().GetFreeEmployees();
            TempData["empleados"] = employeesFree;


            TempData["temp"] = conocimientoPK;
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
        public ActionResult Lista(string knowledge)
        {
            if (knowledge != "")
            {
                TempData.Keep();
                return RedirectToAction("Lista", "equipos", new { conocimientoPK = knowledge, a = "" });
            }
            else
            {
                TempData.Keep();
                return RedirectToAction("Lista", "equipos", new { conocimientoPK = "todos", a = "" });
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
            return View(em);
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
                return RedirectToAction("Lista", "equipos", new { conocimientoPK = know , a = ""});
            }
            else
            {
                return View();
            }
        }


        public ActionResult SetFree(int codigo, string cedula)
        {
            /*sacamos la cedula del string*/
            int counter = cedula.Length;
            string id = "";
            int i = 0;
            int counter2 = 1;
            bool fin = false;

            while (i < counter && fin == false)
            {
                if (cedula[i] == '0' || cedula[i] == '1' || cedula[i] == '2' || cedula[i] == '3' || cedula[i] == '4'
                    || cedula[i] == '5' || cedula[i] == '6' || cedula[i] == '7' || cedula[i] == '8' || cedula[i] == '9')
                {

                    id = id + cedula[i];
                    if (counter2 == 9)
                    {
                        fin = true;
                    }
                    else
                    {
                        counter2++;
                    }
                }
                i++;
            }

            /*se verifica que no tenga requerimientos asignados*/
            bool tiene = new requerimientosController().ExistEmployee(id);

            if (tiene != true)
            {
                /*se le quita el rol al empleado*/
                new rolesController().EraseRol(codigo, id);
            }

            

            /*recargamos la vista de la lista actualizada*/
            string knowledge = TempData["temp"] as string;
            if (knowledge != "")
            {
                TempData.Keep();
                return RedirectToAction("Lista", "equipos", new { conocimientoPK = knowledge, algo = "" });
            }
            else
            {
                TempData.Keep();
                return RedirectToAction("Lista", "equipos", new { conocimientoPK = "todos", algo = "" });
            }
        }

        
        public ActionResult SetBusy(int codigo, string cedula)
        {
            /*sacamos la cedula del string*/
            int counter = cedula.Length;
            string id = "";
            int i = 0;
            int counter2 = 1;
            bool fin = false;

            while (i < counter && fin == false)
            {
                if (cedula[i] == '0' || cedula[i] == '1' || cedula[i] == '2' || cedula[i] == '3' || cedula[i] == '4'
                    || cedula[i] == '5' || cedula[i] == '6' || cedula[i] == '7' || cedula[i] == '8' || cedula[i] == '9')
                {

                    id = id + cedula[i];
                    if (counter2 == 9)
                    {
                        fin = true;
                    }
                    else
                    {
                        counter2++;
                    }
                }
                i++;

            }


            /*se le da el rol de desarrollador al empleado*/
            new rolesController().AddRol(codigo, id, 1);
            
            /*recargamos la vista de la lista actualizada*/
            string knowledge = TempData["temp"] as string;
            if (knowledge != "")
            {
                TempData.Keep();
                return RedirectToAction("Lista", "equipos", new { conocimientoPK = knowledge, algo = "" });
            }
            else
            {
                TempData.Keep();
                return RedirectToAction("Lista", "equipos", new { conocimientoPK = "todos", algo = "" });
            }
        }

    }
}
