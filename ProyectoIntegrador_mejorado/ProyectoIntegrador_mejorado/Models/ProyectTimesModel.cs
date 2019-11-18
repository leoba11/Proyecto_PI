using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoIntegrador_mejorado.Models
{
    public class ProyectTimesModel
    {

        public int codigoProy { get; set; }
        
        public string nombreProyecto { get; set; }
        
        public int? tiempoEstimado { get; set; }

        public int? tiempoReal { get; set; }

        public bool terminado { get; set; }

        public string lider { get; set; }
    }
}