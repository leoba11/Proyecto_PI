using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ProyectoIntegrador_mejorado.Models;

namespace ProyectoIntegrador_mejorado.Controllers
{
    [Authorize]
    public class reportesController : Controller
    {
        private Gr02Proy1Entities db = new Gr02Proy1Entities();

        //EFE: crea la lista para el dropdownlistde reportes
        //REQ: NA
        //MOD: crea la lista para el dropdownlistde reportes




        //filtrar que se muestra en el dropdownlist, segun el usuario


        [HttpGet]
        public ActionResult Index()
        {
            var user = User.Identity.GetUserName();
            var emple = new empleadosController().ExistEmail(user);
            var clien = new clientesController().ExistEmail(user);

            if (emple.Count() > 0)   //es empleado, mostrar los reportes disponibles para los lideres
            {
                //obteniendo la cedula del empleado
                var cedula = emple[0].cedulaPK;

                List<StringModel> reportes = new List<StringModel>();
                reportes.Add(new StringModel { Nombre = "Requerimientos de desarrollador" });
                reportes.Add(new StringModel { Nombre = "Conocimientos más requeridos" });
                reportes.Add(new StringModel { Nombre = "Empleados disponibles entre fechas" });
                reportes.Add(new StringModel { Nombre = "Estado requerimientos de desarrollador" });
                reportes.Add(new StringModel { Nombre = "Tiempos totales por proyecto" });
                reportes.Add(new StringModel { Nombre = "Disponibilidad de desarrolladores" });
                TempData["reportes"] = reportes;
                TempData.Keep();


                // ajustar para mostrar solo los correpondientes




            }
            else if (clien.Count() > 0)     //es cliente, mostrar los reportes disponibles para los clientes
            {
                List<StringModel> reportes = new List<StringModel>();
                reportes.Add(new StringModel { Nombre = "Requerimientos de desarrollador" });
                reportes.Add(new StringModel { Nombre = "Conocimientos más requeridos" });
                reportes.Add(new StringModel { Nombre = "Empleados disponibles entre fechas" });
                reportes.Add(new StringModel { Nombre = "Estado requerimientos de desarrollador" });
                reportes.Add(new StringModel { Nombre = "Tiempos totales por proyecto" });
                reportes.Add(new StringModel { Nombre = "Disponibilidad de desarrolladores" });
                TempData["reportes"] = reportes;
                TempData.Keep();
            }
            else   // es de soporte o el jefe de desarrollo, desplegar todos los reportes
            {
                List<StringModel> reportes = new List<StringModel>();
                reportes.Add(new StringModel { Nombre = "Requerimientos de desarrollador" });
                reportes.Add(new StringModel { Nombre = "Conocimientos más requeridos" });
                reportes.Add(new StringModel { Nombre = "Empleados disponibles entre fechas" });
                reportes.Add(new StringModel { Nombre = "Estado requerimientos de desarrollador" });
                reportes.Add(new StringModel { Nombre = "Tiempos totales por proyecto" });
                reportes.Add(new StringModel { Nombre = "Disponibilidad de desarrolladores" });
                TempData["reportes"] = reportes;
                TempData.Keep();
            }
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
            else if (reporte.Nombre == "Tiempos totales por proyecto")
                return RedirectToAction("TotalTimes", "reportes");
            else if (reporte.Nombre == "Disponibilidad de desarrolladores")
                return RedirectToAction("DisponibilidadEmpleados", "reportes");
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
            /*si el usuario es empleado y no lider, mostrar de una vez su vista*/



            /*si el usuario es empleado y lider, mostrar de una vez su vista*/


            /*si es jefe de desarrollo o soporte*/
            TempData["empleados"] = new empleadosController().Pass();
            TempData["requerimientos"] = null;
            TempData["empSelect"] = null;
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
                TempData["empSelect"] = new empleadosController().GetEmployee(empleado.cedulaPK);
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

        /*
         * EFE: verifica si el usuario es desarrollaro o jefe de desarrollo y le presenta los datos de los proyectos correspondientes
         * REQ: NA
         * MOD: busca el nombre del proyecto y lo agrega
         */
        public ActionResult TotalTimes()
        {
            TempData["usuarioEsJefe"] = null;
            var user = User.Identity.GetUserName();
            var emple = new empleadosController().ExistEmail(user);
            /*si el usuario es empleado, mostrar de una vez su vista*/
            if (emple.Count() > 0)   //es empleado
            {
                //obteniendo la cedula del empleado
                var cedula = emple[0].cedulaPK;
                TempData["proyectos"] = new requerimientosController().GetTotalTimes(cedula);
                foreach (var item in (TempData["proyectos"] as IEnumerable<ProyectoIntegrador_mejorado.Models.ProyectTimesModel>))
                {
                    var proyecto = new proyectosController().ProjectByCode(item.codigoProy);
                    if (proyecto.fechaFinal != null)
                    {
                        item.terminado = false;
                    }
                    else
                    {
                        item.terminado = true;
                    }
                    item.nombreProyecto = proyecto.nombre;
                }
                TempData.Keep();
            }
            else   // es de soporte o el jefe de desarrollo
            {
                TempData["usuarioEsJefe"] = "si";
                TempData["proyectos"] = new requerimientosController().GetTotalTimes(null);
                foreach (var item in (TempData["proyectos"] as IEnumerable<ProyectoIntegrador_mejorado.Models.ProyectTimesModel>))
                {
                    var proyecto = new proyectosController().ProjectByCode(item.codigoProy);
                    var liderId = new rolesController().getLiderId(item.codigoProy);
                    var lider = new empleadosController().GetEmployee(liderId);
                    if (proyecto.fechaFinal != null)
                    {
                        item.terminado = false;
                    }
                    else
                    {
                        item.terminado = true;
                    }
                    item.nombreProyecto = proyecto.nombre;
                    if (lider != null)
                    {
                        item.lider = lider.nombre + " " + lider.apellido1 + " " + lider.apellido2;
                    }
                }
                TempData.Keep();
            }
            return View();
        }

        //[Authorize(Roles = "Soporte, JefeDesarrollo,Lider")]
        public ActionResult DisponibilidadEmpleados()
        {

            //limpio los datos 
            TempData["empDisponibles"] = null;
            TempData["empOcupados"] = null;
            TempData["pryActual"] = null;
            var user = User.Identity.GetUserName();
            var emple = new empleadosController().ExistEmail(user);
            //obteniendo la cedula del empleado
            
            /*si el usuario es empleado, mostrar de una vez su vista*/
            if (emple.Count() > 0)   //es empleado
            {
                var cedula = emple[0].cedulaPK;
                bool esLider = new rolesController().idLiderNow(cedula);
                if (esLider == true) {
                    TempData["empDisponibles"] = new empleadosController().GetFreeEmployees();
                    var proy = new proyectosController().GetLiderProyectoActual(cedula);
                    TempData["empOcupados"] = new empleadosController().GetEmployeeBusyProject(cedula,proy[0].codigoPK);
                    TempData.Keep();
                }
                
            }
            else
            { //es jefe de desarrollo/soporte 
                TempData["empDisponibles"] = new empleadosController().GetFreeEmployees();
                TempData["empOcupados"] = new empleadosController().GetEmployeeBusyProject(null, 0);
                TempData.Keep();
            }


            return View();
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
