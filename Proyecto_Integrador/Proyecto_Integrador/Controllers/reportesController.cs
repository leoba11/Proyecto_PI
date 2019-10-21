using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_Integrador.Controllers
{
    public class reportesController : Controller
    {
        // GET: reportes
        [HttpGet]
        public ActionResult Index()
        {
            List<SelectListItem> mySkills = new List<SelectListItem>() {
                new SelectListItem {
                    Text = "opcion 1", Value = "1"
                },
                new SelectListItem {
                    Text = "ASP.NET WEB API", Value = "2"
                },
                new SelectListItem {
                    Text = "ENTITY FRAMEWORK", Value = "3"
                },
                new SelectListItem {
                    Text = "DOCUSIGN", Value = "4"
                },
                new SelectListItem {
                    Text = "ORCHARD CMS", Value = "5"
                },
                new SelectListItem {
                    Text = "JQUERY", Value = "6"
                },
                new SelectListItem {
                    Text = "ZENDESK", Value = "7"
                },
                new SelectListItem {
                    Text = "LINQ", Value = "8"
                },
                new SelectListItem {
                    Text = "C#", Value = "9"
                },
                new SelectListItem {
                    Text = "GOOGLE ANALYTICS", Value = "10"
                },
            };
            TempData["reportes"] = mySkills;
            TempData.Keep();
            return RedirectToAction("SelectReport", "reportes");
        }

        public ActionResult SelectReport()
        {
            TempData.Keep();
            return View();
        }

        [HttpPost]
        public ActionResult SelectReport(string reporte)
        {
            TempData.Keep();


            /*aqui se agrega if, para redirigir del reporte correspondiente*/
            return View();
        }
    }
}