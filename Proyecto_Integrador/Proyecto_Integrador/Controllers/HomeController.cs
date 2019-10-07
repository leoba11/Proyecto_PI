using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_Integrador.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Models.UserLoginModel objUserLogin)
        {
            if (objUserLogin.UserName == "admin" && objUserLogin.Password == "1234")
                return RedirectToAction("About");
            else
            {
                objUserLogin.Message = "Nombre de usuario/contraseña inválido";
                return View(objUserLogin);
            }
        }
            public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}