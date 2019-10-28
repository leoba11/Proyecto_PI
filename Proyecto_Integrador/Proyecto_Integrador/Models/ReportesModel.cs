using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_Integrador.Models
{
    using System.ComponentModel.DataAnnotations;

    public class ReportesModel
    {
        public string Nombre { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime fechaInicial { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime fechaFinal { get; set; }
    }
}