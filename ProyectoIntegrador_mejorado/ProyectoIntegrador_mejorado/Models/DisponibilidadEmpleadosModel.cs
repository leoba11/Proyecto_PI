using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoIntegrador_mejorado.Models
{
    public class DisponibilidadEmpleadosModel
    {
        public int codigoProy { get; set; }

        public string nombreProyecto { get; set; }

        public string nombreEmpleado { get; set; }
        public string apellido1Empleado { get; set; }
        public string apellido2Empleado { get; set; }

        public DateTime  fechaIniciopry { get; set; }

        public DateTime fechaEstimadapry { get; set; }


    }
}