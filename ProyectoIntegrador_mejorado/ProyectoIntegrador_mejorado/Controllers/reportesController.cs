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

        // GET: reportes
        [HttpGet]
        public ActionResult Index()
        {
            List<StringModel> reportes = new List<StringModel>();
            reportes.Add(new StringModel { Nombre = "Requerimientos de desarrollador" });
            reportes.Add(new StringModel { Nombre = "Conocimientos más requeridos" });
            reportes.Add(new StringModel { Nombre = "Empleados disponibles entre fechas" });
            TempData["reportes"] = reportes;
            TempData.Keep();

            return RedirectToAction("SelectReport", "reportes");
        }

        public ActionResult SelectReport()
        {
            TempData.Keep();
            return View();
        }

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
            else
                return RedirectToAction("SelectReport", "reportes");
        }

        /*
         * Efecto: Request GET de EmployeesDates
         * Requiere: NA
         * Modifica: NA
         */
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

            //List<empleados> empleados = new empleadosController().Pass();
            //ViewBag.empleados = new SelectList(empleados, "cedulaPK", "nombre");
            //TempData["empleados"] = new SelectList(empleados, "cedulaPK", "nombre");

            List<proyectos> proyectos = new proyectosController().Pass();
            ViewBag.proyectos = new SelectList(proyectos, "codigoPK", "nombre");
            TempData["proyectos"] = new SelectList(proyectos, "codigoPK", "nombre");
            TempData.Keep();
            return View();
        }

        /*
         * Efecto: Request POST de requerimientosDesarrollador
         * Requiere: código de proyecto y cédula de empleado
         * Modifica: NA
         */
        [HttpPost]
        public ActionResult requerimientosDesarrollador(proyectos proy, empleados emp)
        {
            if(proy.codigoPK != 0 && emp.cedulaPK != null)
            {
                TempData["proyecto"] = proy.codigoPK; //se obtiene el código del proyecto
                TempData["nombreProyecto"] = new proyectosController().ProjectByCode(int.Parse(TempData["proyecto"].ToString())).nombre; //se obtiene el nombre a partir del código anterior
                TempData["empleados"] = emp.cedulaPK;//se obtiene el código del empleado

                try
                {//se comunica con el controlador de empleados para que le pase los nombres de empleado asociados con el proyecto y cedula de empleado seleccioando
                    TempData["nombreEmpleado"] = new empleadosController().EmpByCode(int.Parse(TempData["proyecto"].ToString()), TempData["empleados"].ToString()).nombre;
                }
                catch (NullReferenceException)
                {
                    return RedirectToAction("requerimientosDesarrollador", "reportes");//si ocurre error se redirige a página de selección
                }

                TempData.Keep(); //se le solicita mantener los datos nuevamente
                TempData["req"] = db.cantidadReq(proy.codigoPK, emp.cedulaPK).AsEnumerable();
                return View();
            }
            else
            {
                return View();
            }
            //TempData.Keep();
            //TempData["req"] = db.cantidadReq(modelo.codigoProy, modelo.cedulaEmp).AsEnumerable();
            //return View();
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
                if (fechas.Fecha1 <= fechas.Fecha2)
                {
                    TempData.Keep();
                    TempData["conocimientos"] = db.conocimientos_en_rango(fechas.Fecha1, fechas.Fecha2).AsEnumerable();
                    TempData["fechas"] = fechas;
                    return View();
                }
                else
                {

                    return View();
                }
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
