using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoIntegrador_mejorado.Models;

namespace ProyectoIntegrador_mejorado.Controllers
{
    [Authorize(Roles = "Soporte, JefeDesarrollo, Lider, Desarrollador")]
    public class reportesController : Controller
    {
        private Gr02Proy1Entities db = new Gr02Proy1Entities();

        //EFE: crea la lista para el dropdownlistde reportes
        //REQ: NA
        //MOD: crea la lista para el dropdownlistde reportes
        [HttpGet]
        public ActionResult Index()
        {
            List<StringModel> reportes = new List<StringModel>();
            reportes.Add(new StringModel { Nombre = "Requerimientos de desarrollador" });
            reportes.Add(new StringModel { Nombre = "Conocimientos más requeridos" });
            reportes.Add(new StringModel { Nombre = "Empleados disponibles entre fechas" });
            reportes.Add(new StringModel { Nombre = "Estado requerimientos de desarrollador" });
            TempData["reportes"] = reportes;
            TempData.Keep();

            return RedirectToAction("SelectReport", "reportes");
        }

        //EFE: redirige a la vista de seleccion de reporte
        //REQ: entrar como redireccion de index
        //MOD: NA
        public ActionResult SelectReport()
        {
            TempData.Keep();
            return View();
        }

        //EFE: comprueba si el valor elegido en el dropdownlist es valido y redirige a la vista correspondiente
        //REQ: entrar como redireccion de la vista de seleccionar proyecto
        //MOD: NA
        [HttpPost]
        public ActionResult SelectReport(StringModel reporte)
        {
            //invocar como ReportesModel
            TempData.Keep();
            if (reporte.Nombre == "Requerimientos de desarrollador")
                return RedirectToAction("requerimientosDesarrollador", "reportes");
            else if (reporte.Nombre == "Conocimientos más requeridos")
                return RedirectToAction("KnowledgesReport", "reportes");
            else if (reporte.Nombre == "Empleados disponibles entre fechas")
                return RedirectToAction("EmployeesDates", "reportes");
            else if (reporte.Nombre == "Estado requerimientos de desarrollador")
                return RedirectToAction("EmployeeRequirements", "reportes");
            else
                return RedirectToAction("SelectReport", "reportes");
        }

        //EFE: redirige a la vista del reporte de empleados disponibles entre fechas
        //REQ: NA
        //MOD: NA
        public ActionResult EmployeesDates()
        {
            TempData.Keep();
            return View();
            //return RedirectToAction("SelectReport", "reportes");
        }

        /*
         * Efecto: Request GET de requerimientosDesarrollador
         * Requiere: NA
         * Modifica: NA
         */
        public ActionResult requerimientosDesarrollador()
        {
            List<proyectos> proyectos = new proyectosController().Pass();
            List<empleados> empleados = new empleadosController().Pass();
            //ViewBag.proyectos = new SelectList(proyectos, "codigoPK", "nombre");
            //ViewBag.empleados = new SelectList(empleados, "cedulaPK", "nombre");
            TempData["proyectos"] = new SelectList(proyectos, "codigoPK", "nombre");
            TempData["empleados"] = new SelectList(empleados, "cedulaPK", "nombre");
            TempData.Keep();
            return View();
        }

        /*
         * Efecto: Request POST de requerimientosDesarrollador
         * Requiere: código de proyecto y cédula de empleado
         * Modifica: NA
         */
        [HttpPost]
        public ActionResult requerimientosDesarrollador(FechasModel modelo)
        {
            TempData.Keep(); // Para mantener los datos
            TempData["req"] = new empleadosController().GetEmployeeByProyect(modelo.codigoProy);
            return View(); // Regresar a la vista
        }

        public ActionResult GetEmpList(int codigoProyecto)
        {
            List<empleados> employees = new empleadosController().GetEmployeeByProyect(codigoProyecto);
            ViewBag.Employees = new SelectList(employees, "cedulaPK", "nombre");

            return PartialView("EmployeesPartial");
        }

        /*
         * Efecto: Request POST de EmployeesDates
         * Requiere: fecha inicial y final
         * Modifica: NA
         */
        [HttpPost]
        public ActionResult EmployeesDates(FechasModel fechas)
        {
            if (fechas.Fecha1 == null || fechas.Fecha2 == null || fechas.Fecha2 < fechas.Fecha1)
            {
                TempData.Keep();
                return View();
            }
            else
            {
                
                TempData.Keep();
                TempData["empl"] = db.EmpleadosParaReporteFechas(fechas.Fecha1, fechas.Fecha2).AsEnumerable();
                TempData["fechas"] = fechas;
                return View();
            }
        }



        /*
         * Efecto: Request GET de KnowledgesReport
         * Requiere: NA
         * Modifica: NA
         */
        public ActionResult KnowledgesReport()
        {
            TempData["conocimientos"] = null;
            ViewBag.alert = false;
            TempData.Keep();
            return View();
        }

        //Método POST de la vista de reporte de conocimientos
        /*
         * Efecto: Request POST de KnowledgesReport
         * Requiere: fecha inicial y final
         * Modifica: NA
         */
        [HttpPost]
        public ActionResult KnowledgesReport(FechasModel fechas)
        {
            if (fechas.Fecha1 != null && fechas.Fecha2 != null)
            {
                ViewBag.alert = false;
                if (fechas.Fecha1 <= fechas.Fecha2)
                {
                    TempData.Keep();
                    TempData["conocimientos"] = db.conocimientos_en_rango(fechas.Fecha1, fechas.Fecha2).ToList();
                    TempData["fechas"] = fechas;
                    return View();
                }
                else
                {
                    ViewBag.alert = true;
                    ViewBag.alertMessage = "La fecha inicial debe ser menor a la fecha final.";
                    return View();
                }
            }
            else
            {
                TempData["conocimientos"] = null;
                return View();
            }
        }




        //EFE: redirige a la vista del reporte de estado de requerimientos para un desarrollador
        //REQ: NA
        //MOD: NA
        public ActionResult EmployeeRequirements()
        {
            /*si el usuario es empleado, mostrar de una vez su vista*/


            /*si es jefe de desarrollo o soporte*/
            TempData["empleados"] = new empleadosController().Pass();
            TempData.Keep();
            return View();
            //return RedirectToAction("SelectReport", "reportes");
        }


        /*
         * Efecto: Request POST de EmployeeRequirements
         * Requiere: fecha inicial y final
         * Modifica: NA
         */
        [HttpPost]
        public ActionResult EmployeeRequirements(empleados empleado)
        {
            if (empleado.cedulaPK != null)
            {
                TempData["requerimientos"] = new requerimientosController().GetRequirementsByEmployee(empleado.cedulaPK);
                TempData.Keep();
                return View();
            }
            else
            {

                TempData.Keep();
                return View();
            }
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
