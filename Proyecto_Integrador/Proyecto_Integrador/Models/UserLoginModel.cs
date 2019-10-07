using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Proyecto_Integrador.Models
{
    public class UserLoginModel
    {
        [Display(Name = "Nombre de usuario")]
        public string UserName { get; set; }
        [Display(Name = "Contraseña")]
        public string Password { get; set; }
        public string Message { get; set; }
    }
}