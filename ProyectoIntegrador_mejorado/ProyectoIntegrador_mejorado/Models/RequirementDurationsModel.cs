using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoIntegrador_mejorado.Models
{
    public class RequirementDurationsModel
    {
        /* Atributo de modelo para manejar complejidad de requerimientos */
        public int complexity { get; set; }

        /* Atributo de modelo para manejar cantidad de requerimientos en esa complejidad */
        public int requirementCount { get; set; }

        /* Atributo de modelo para manejar diferencia mínima entre duración estimada y real */
        public int minDiff { get; set; }

        /* Atributo de modelo para manejar diferencia máxima entre duración estimada y real */
        public int maxDiff { get; set; }

        /* Atributo de modelo para manejar promedio de duración real */
        public int avgDuration { get; set; }
    }
}