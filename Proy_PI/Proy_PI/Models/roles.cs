//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Proy_PI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class roles
    {
        public string rol { get; set; }
        [Key]
        public string cedulaFK { get; set; }
        [Key]
        public string codigoProyectoFK { get; set; }
    
        public virtual empleados empleados { get; set; }
        public virtual proyectos proyectos { get; set; }
    }
}
