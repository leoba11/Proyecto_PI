using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
//------------------------------------------------------------------------------
// @author: María José Aguilar Barboza B60115
// @Date: 16/11/19
// Este código corresponde la consulta de desarrolladores disponibles
//------------------------------------------------------------------------------
namespace ProyectoIntegrador_mejorado.Models
{
    public class DisponibilidadEmpleadosModel
    {
        //para almacenar el código del proyecto en el que el desarrollador está participando
        public Nullable<int> codigoProy { get; set; }

        //para almacenar el nombre del proyecto en el que el desarrollador está participando
        public string nombreProyecto { get; set; }

        //para almacenar el nombre del empleado con ambos apellidos
        public string nombreEmpleado { get; set; }
        public string apellido1Empleado { get; set; }
        public string apellido2Empleado { get; set; }


        //para almacenar la fecha de inicio del proyecto en el que el desarrollador está participando
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> fechaIniciopry { get; set; }

        //para almacenar la fecha final estimada del proyecto en el que el desarrollador está participando
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> fechaEstimadapry { get; set; }


    }
}