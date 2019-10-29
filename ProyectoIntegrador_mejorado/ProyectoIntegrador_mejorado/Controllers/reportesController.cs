using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoIntegrador_mejorado.Models;

namespace ProyectoIntegrador_mejorado.Controllers
{
    [Authorize]
    public class reportesController : Controller
    {
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
        public ActionResult EmployeesDates()
        {
            TempData.Keep();
            return View();
        }
    }
}
