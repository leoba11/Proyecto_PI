using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoIntegrador_mejorado.Models
{
    public class FechasModel
    {
        /* Atributo de modelo para manejar fecha inicial en diferentes reportes */
        public DateTime Fecha1 { get; set; }

        /* Atributo de modelo para manejar fecha final en diferentes reportes */
        public DateTime Fecha2 { get; set; }

        /* Atributo de modelo para manejar código de proyecto en el reporte que se requiera */
        public int codigoProy { get; set; }

        /* Atributo de modelo para manejar cédula de empleado en el reporte que se requiera */
        public string cedulaEmp { get; set; }

        /* Atributo de modelo para manejar conocimiento en el reporte que se requiera */
        public string conocimiento { get; set; }
    }
}