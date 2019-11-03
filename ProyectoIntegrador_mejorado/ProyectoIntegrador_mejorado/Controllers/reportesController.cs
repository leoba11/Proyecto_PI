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
            List<ReportesModel> reportes = new List<ReportesModel>();
            reportes.Add(new ReportesModel { Nombre = "Total de requerimientos de desarrollador" });
            reportes.Add(new ReportesModel { Nombre = "Conocimientos más requeridos" });
            reportes.Add(new ReportesModel { Nombre = "Empleados disponibles entre fechas" });
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
        public ActionResult SelectReport(ReportesModel reporte)
        {
            //invocar como ReportesModel
            TempData.Keep();
            if (reporte.Nombre == "Total de requerimientos de desarrollador")
                return RedirectToAction("requerimientosDesarrollador", "reportes");
            else if (reporte.Nombre == "Conocimientos más requeridos")
                return RedirectToAction("KnowledgesReport", "reportes");
            else if (reporte.Nombre == "Empleados disponibles entre fechas")
                return RedirectToAction("EmployeesDates", "reportes");
            else
                return RedirectToAction("SelectReport", "reportes");
        }

        //Método GET de la vista de reporte de empleados desocupados
        public ActionResult EmployeesDates()
        {
            TempData.Keep();
            return View();
            //return RedirectToAction("SelectReport", "reportes");
        }

        //Método POST de la vista de reporte de empleados desocupados
        [HttpPost]
        public ActionResult EmployeesDates(FechasModel fechas)
        {
            if (fechas.Fecha1 == null || fechas.Fecha2 == null)
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

        //Método GET de la vista de reporte de conocimientos
        public ActionResult KnowledgesReport()
        {
            TempData.Keep();
            return View();
        }

        //Método POST de la vista de reporte de conocimientos
        [HttpPost]
        public ActionResult KnowledgesReport(FechasModel fechas)
        {
            if (fechas.Fecha1 != null && fechas.Fecha2 != null)
            {
                TempData.Keep(); // verificar fechas !!!
                TempData["conocimientos"] = db.conocimientos_en_rango(fechas.Fecha1, fechas.Fecha2).AsEnumerable();
                TempData["fechas"] = fechas;
                return View();
            }
            else
            {
                TempData.Keep();
                return View();
            }
        }
    }
}
