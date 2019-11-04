using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoIntegrador_mejorado.Models
{
    public class FechasModel
    {
        
        public DateTime Fecha1 { get; set; }
        
        public DateTime Fecha2 { get; set; }

        public int idProyecto { get; set; }

        public string cedulaEmp { get; set; }

    }
}