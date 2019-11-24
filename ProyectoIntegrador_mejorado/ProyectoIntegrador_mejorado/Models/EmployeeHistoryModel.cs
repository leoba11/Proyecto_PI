using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoIntegrador_mejorado.Models
{
    public class EmployeeHistoryModel
    {
        /* Atributo de modelo para manejar nombre de proyecto */
        public string projectName { get; set; }

        /* Atributo de modelo para manejar rol ejecutado en el proyecto */
        public string executedRole { get; set; }

        /* Atributo de modelo para cantidad de horas dedicadas al proyecto */
        public int dedicatedHours { get; set; }
    }
}