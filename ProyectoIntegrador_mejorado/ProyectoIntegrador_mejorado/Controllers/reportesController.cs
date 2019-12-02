﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ProyectoIntegrador_mejorado.Models;

namespace ProyectoIntegrador_mejorado.Controllers
{
    [Authorize]
    public class reportesController : Controller
    {
        private Gr02Proy1Entities db = new Gr02Proy1Entities();

        //EFE: crea la lista para el dropdownlistde reportes
        //REQ: NA
        //MOD: crea la lista para el dropdownlistde reportes




        //filtrar que se muestra en el dropdownlist, segun el usuario


        [HttpGet]
        public ActionResult Index()
        {
            var user = User.Identity.GetUserName();
            var emple = new empleadosController().ExistEmail(user);
            var clien = new clientesController().ExistEmail(user);

            if (emple.Count() > 0)   //es empleado, mostrar los reportes disponibles para los lideres
            {
                //obteniendo la cedula del empleado
                var cedula = emple[0].cedulaPK;

                List<StringModel> reportes = new List<StringModel>();
                reportes.Add(new StringModel { Nombre = "Requerimientos de desarrollador" });
                reportes.Add(new StringModel { Nombre = "Estado requerimientos de desarrollador" });
                reportes.Add(new StringModel { Nombre = "Tiempos totales por proyecto" });
                reportes.Add(new StringModel { Nombre = "Disponibilidad de desarrolladores" });
                TempData["reportes"] = reportes;
                TempData.Keep();


                // ajustar para mostrar solo los correpondientes




            }
            else if (clien.Count() > 0)     //es cliente, mostrar los reportes disponibles para los clientes
            {
                List<StringModel> reportes = new List<StringModel>();
                reportes.Add(new StringModel { Nombre = "Requerimientos de desarrollador" });
                reportes.Add(new StringModel { Nombre = "Disponibilidad de desarrolladores" });
                reportes.Add(new StringModel { Nombre = "Estado y responsable de requerimientos de proyecto" });
                reportes.Add(new StringModel { Nombre = "Cuenta de Requerimientos Finalizados y En Curso" });
                TempData["reportes"] = reportes;
                TempData.Keep();
            }
            else   // es de soporte o el jefe de desarrollo, desplegar todos los reportes
            {
                List<StringModel> reportes = new List<StringModel>();
                reportes.Add(new StringModel { Nombre = "Requerimientos de desarrollador" });
                reportes.Add(new StringModel { Nombre = "Información sobre conocimientos" });
                reportes.Add(new StringModel { Nombre = "Disponibilidad de empleados entre fechas" });
                reportes.Add(new StringModel { Nombre = "Estado requerimientos de desarrollador" });
                reportes.Add(new StringModel { Nombre = "Tiempos totales por proyecto" });
                reportes.Add(new StringModel { Nombre = "Disponibilidad de desarrolladores" });
                reportes.Add(new StringModel { Nombre = "Historial de desarrollador" });
                reportes.Add(new StringModel { Nombre = "Análisis de duraciones en requerimientos" });
                reportes.Add(new StringModel { Nombre = "Diferencia entre fecha estimada y real" });
                TempData["reportes"] = reportes;
                TempData.Keep();
            }
            return RedirectToAction("SelectReport", "reportes");
        }

        //EFE: redirige a la vista de seleccion de reporte
        //REQ: entrar como redireccion de index
        //MOD: NA
        public ActionResult SelectReport()
        {
            TempData.Keep();
            return View();
        }

        //EFE: comprueba si el valor elegido en el dropdownlist es valido y redirige a la vista correspondiente
        //REQ: entrar como redireccion de la vista de seleccionar proyecto
        //MOD: NA
        [HttpPost]
        public ActionResult SelectReport(StringModel reporte)
        {
            //invocar como ReportesModel
            TempData.Keep();
            if (reporte.Nombre == "Requerimientos de desarrollador")
                return RedirectToAction("requerimientosDesarrollador", "reportes");
            else if (reporte.Nombre == "Información sobre conocimientos")
                return RedirectToAction("KnowledgesReport", "reportes");
            else if (reporte.Nombre == "Disponibilidad de empleados entre fechas")
                return RedirectToAction("EmployeesDates", "reportes");
            else if (reporte.Nombre == "Estado requerimientos de desarrollador")
                return RedirectToAction("EmployeeRequirements", "reportes");
            else if (reporte.Nombre == "Tiempos totales por proyecto")
                return RedirectToAction("TotalTimes", "reportes");
            else if (reporte.Nombre == "Disponibilidad de desarrolladores")
                return RedirectToAction("DisponibilidadEmpleados", "reportes");
            else if (reporte.Nombre == "Historial de desarrollador")
                return RedirectToAction("EmployeeHistory", "reportes");
            else if (reporte.Nombre == "Análisis de duraciones en requerimientos")
                return RedirectToAction("RequirementDurationAnalisis", "reportes");
            else if (reporte.Nombre == "Diferencia entre fecha estimada y real")
                return RedirectToAction("diferenciaEstimadaReal", "reportes");
            else if (reporte.Nombre == "Estado y responsable de requerimientos de proyecto")
                return RedirectToAction("ProjectRequirementsState", "reportes");
            else if (reporte.Nombre == "Cuenta de Requerimientos Finalizados y En Curso")
                return RedirectToAction("cuentaDeRequerimientos", "reportes");
            else
                return RedirectToAction("SelectReport", "reportes");
        }

        //EFE: redirige a la vista del reporte de empleados disponibles entre fechas
        //REQ: NA
        //MOD: NA
        public ActionResult EmployeesDates()
        {
            TempData["empl"] = null;
            TempData["empl2"] = null;
            TempData["nulo"] = null;
            TempData.Keep();
            return View();
            //return RedirectToAction("SelectReport", "reportes");
        }

        /*
         * Efecto: Request GET de requerimientosDesarrollador
         * Requiere: NA
         * Modifica: NA
         */
        public ActionResult requerimientosDesarrollador()
        {
            List<proyectos> proyectos = new proyectosController().Pass();
            List<empleados> empleados = new empleadosController().Pass();
            //ViewBag.proyectos = new SelectList(proyectos, "codigoPK", "nombre");
            //ViewBag.empleados = new SelectList(empleados, "cedulaPK", "nombre");
            TempData["proyectos"] = new SelectList(proyectos, "codigoPK", "nombre");
            TempData["empleados"] = new SelectList(empleados, "cedulaPK", "nombre");
            TempData.Keep();
            return View();
        }

        /*
         * Efecto: Request POST de requerimientosDesarrollador
         * Requiere: código de proyecto y cédula de empleado
         * Modifica: NA
         */
        [HttpPost]
        public ActionResult requerimientosDesarrollador(FechasModel modelo)
        {
            TempData.Keep(); // Para mantener los datos
            TempData["req"] = new empleadosController().GetEmployeeByProyect(modelo.codigoProy);
            return View(); // Regresar a la vista
        }





        /*
        * Efecto: Request GET de diferenciaEstimadaReal
        * Requiere: NA
        * Modifica: NA
        */
        public ActionResult diferenciaEstimadaReal()
        {
            List<empleados> empleados = new empleadosController().Pass();
            ViewBag.EmpleadosList = new SelectList(empleados, "cedulaPK", "nombre");
            TempData["empleados"] = new SelectList(empleados, "cedulaPK", "nombre");
            TempData.Keep();
            return View();
        }


        /*
         * Efecto: Request POST de diferenciaEstimadaReal
         * Requiere: código de proyecto y cédula de empleado
         * Modifica: NA
         */
        [HttpPost]
        public ActionResult diferenciaEstimadaReal(DiferenciaEstimadaFinal modelo)
        {
            TempData.Keep(); // Para mantener los datos
            TempData["proyectosD"] = db.ComparacionFechasInicioEst(modelo.cedulaEmp, modelo.codigoProy).ToList();
            return View(); // Regresar a la vista
        }



        //Este método es utilizado en el dropdown en cascada de selección de proyecto y módulo
        public ActionResult GetProyList(string ced)
        {
            List<proyectos> proy = new proyectosController().ProyectsByEmployee(ced);//se comunica con el controlador de módulos para que pase el listado de módulos de acuerdo al proyecto
            ViewBag.Projects = new SelectList(proy, "codigoPK", "nombre"); //ese listado se guarda en esta "vista"

            TempData.Keep(); //se le solicita mantener los datos nuevamente
            //return View();
            return PartialView("ProjectsPartial");
        }



        public ActionResult GetEmpList(int codigoProyecto)
        {
            List<empleados> employees = new empleadosController().GetEmployeeByProyect(codigoProyecto);
            ViewBag.Employees = new SelectList(employees, "cedulaPK", "nombre");

            return PartialView("EmployeesPartial");
        }

        /*
         * Efecto: Request POST de EmployeesDates
         * Requiere: fecha inicial y final
         * Modifica: NA
         */
        [HttpPost]
        public ActionResult EmployeesDates(FechasModel fechas)
        {
            if (fechas.Fecha1 == null || fechas.Fecha2 == null || fechas.Fecha2 < fechas.Fecha1)
            {
                TempData.Keep();
                return View();
            }
            else
            {
                
                TempData.Keep();
                var empl = db.DiasDisponiblesEmpleado(fechas.Fecha1, fechas.Fecha2).ToList(); //lista principal, sin fechas
                int size = empl.Count();
                var empl2 = db.PeriodosDeDisponibilidadEmpleado(fechas.Fecha1, fechas.Fecha2).ToList(); //lista con las fechas
                int size2 = empl2.Count();

                //variables temporales
                //bool created = false;
                bool done;
                int counter = 0;
                int counter2;
                int counter3;
                int times = 0;
                int counter4;
                string cedula = "000000000";
                DateTime [] periodos;
                if (size2 > 0)
                {
                    periodos = new DateTime[size2 * 2]; //para maximo *2
                }
                else
                {
                    periodos = new DateTime[2]; //para maximo *2
                }
                TempData["nulo"] = periodos[0];

                //primer ciclo, controla por cedula
                while (counter < size2)
                {
                    if (empl2[counter].cedulaPK != cedula) //se guarda el periodo la primera vez que se ve la cedula
                    {
                        cedula = empl2[counter].cedulaPK;
                        periodos[0] = empl2[counter].Fecha1.Value;
                        periodos[1] = empl2[counter].Fecha2.Value;
                        times = 2;
                    }
                    else //
                    {
                        counter2 = 0;
                        while (counter2 < times)    //se recorre por cada par de fechas actualmente en periodos
                        {
                            if (empl2[counter].Fecha1.Value > periodos[counter2] && empl2[counter].Fecha2.Value < periodos[counter2 + 1])//inicia y termina durante
                            {
                                counter4 = times;
                                while (counter4 > counter2) //movemos todos los valores dos lugares a la derecha
                                {
                                    periodos[counter4 + 2] = periodos[counter4];
                                    counter4--;
                                }
                                periodos[counter2 + 1] = empl2[counter].Fecha1.Value;
                                periodos[counter2 + 2] = empl2[counter].Fecha2.Value;
                                times += 2;
                            }
                            else if (empl2[counter].Fecha1.Value <= periodos[counter2] && empl2[counter].Fecha2.Value > periodos[counter2] && empl2[counter].Fecha2.Value < periodos[counter2 + 1]) //inicia antes, termina durante
                            {
                                periodos[counter2] = empl2[counter].Fecha2.Value;
                            }
                            else if (empl2[counter].Fecha1.Value <= periodos[counter2] &&  empl2[counter].Fecha2.Value >= periodos[counter2 + 1]) //inicia antes, termina despues
                            {
                                counter4 = counter2;
                                while (counter4 < times) //movemos todos los valores dos a la izquierda
                                {
                                    periodos[counter4] = periodos[counter4 + 2];
                                    counter4++;
                                }
                                times -= 2;
                            }
                            else if (empl2[counter].Fecha1.Value > periodos[counter2] && empl2[counter].Fecha1.Value < periodos[counter2+1] && empl2[counter].Fecha2.Value >= periodos[counter2 + 1]) //inicia durante, termina despues
                            {
                                periodos[counter2+1] = empl2[counter].Fecha1.Value;
                            }
                            counter2 += 2;
                        }
                    }
                    if ((counter + 1) == size2 || empl2[counter + 1].cedulaPK != cedula) // es el ultimo valor para este empleado y se guarda
                    {
                        done = false;
                        counter3 = 0;
                        while (done == false && counter3 < size)
                        {
                            if (empl[counter3].cedulaPK == cedula)
                            {
                                empl[counter3].fechas = new DateTime[size2* 2];
                                periodos.CopyTo(empl[counter3].fechas, 0);
                                done = true;
                            }
                            counter3++;
                        }
                        //se limpia el array periodos
                        for (int i =0; i < times; i++)
                        {
                            periodos[i] = periodos[size2* 2 - 1];
                        }
                    }
                    counter++;
                }
                TempData["empl"] = empl;
                TempData["fechas"] = fechas;
                return View();
            }
        }



        /*
         * Efecto: Request GET de KnowledgesReport
         * Requiere: NA
         * Modifica: NA
         */
        public ActionResult KnowledgesReport()
        {
            List<string> knowledges = new conocimientosController().Pass();
            TempData["conocimientos"] = new SelectList(knowledges);
            TempData.Keep();
            return View();
        }

        //Método POST de la vista de reporte de conocimientos
        /*
         * Efecto: Request POST de KnowledgesReport
         * Requiere: conocimiento
         * Modifica: NA
         */
        [HttpPost]
        public ActionResult KnowledgesReport(FechasModel fechas)
        {
            TempData.Keep();
            TempData["reporteConocimientos"] = db.ReporteConocimientos(fechas.conocimiento).ToList();
            return View();
        }

        //EFE: redirige a la vista del reporte de estado de requerimientos para un desarrollador
        //REQ: NA
        //MOD: NA
        public ActionResult EmployeeRequirements()
        {

            TempData["requerimientos"] = null;
            TempData["empSelect"] = null;
            TempData["proyecto"] = null;

            var user = User.Identity.GetUserName();
            var emple = new empleadosController().ExistEmail(user);
            if (emple.Count() > 0)
            {
                bool liderNow = new rolesController().idLiderNow(emple[0].cedulaPK);
                if(liderNow == true)
                {
                    /*si el usuario es empleado y lider, mostrar de una vez su vista*/
                    TempData["rol"] = "lider";
                    var actualProyect = new rolesController().ProyectoLiderNow(emple[0].cedulaPK);
                    TempData["proyecto"] = actualProyect;
                    TempData["requerimientos"] = new requerimientosController().GetRequirementsByProyect(actualProyect.codigoPK);
                    foreach (var item in TempData["requerimientos"] as List<ProyectoIntegrador_mejorado.Models.requerimientos>)
                    {
                        var empleado = new empleadosController().GetEmployee(item.cedulaEmpleadoFK);
                        item.descripcion = empleado.nombre + " " + empleado.apellido1 + " " + empleado.apellido2;
                    }
                }
                else
                {
                    /*si el usuario es empleado y no lider, mostrar de una vez su vista*/
                    TempData["rol"] = "desarrollador";
                    var requirementList = new requerimientosController().GetRequirementsByEmployee(emple[0].cedulaPK);
                    if (requirementList != null)
                    {
                        var actualProyect = new proyectosController().getProjectName(requirementList[0].codigoProyectoFK);
                        TempData["proyecto"] = actualProyect;
                    }
                    TempData["requerimientos"] = requirementList;
                }
                TempData.Keep();
                return View();
            }
            else
            {
                /*si es jefe de desarrollo o soporte*/
                TempData["rol"] = "boss";
                TempData["empleados"] = new empleadosController().Pass();
                List<proyectos> proyectos = new proyectosController().Pass();
                TempData["proyectos"] = proyectos;
                TempData.Keep();
                return View();
            }

            //return RedirectToAction("SelectReport", "reportes");
        }


        /*
         * Efecto: Request POST de EmployeeRequirements
         * Requiere: empleado seleccionado (cedula)
         * Modifica: NA
         */
        [HttpPost]
        public ActionResult EmployeeRequirements(empleados empleado)
        {
            TempData["liderDeProyecto"] = null;
            if (empleado.cedulaPK != null)
            {
                TempData["empSelect"] = new empleadosController().GetEmployee(empleado.cedulaPK);
                TempData["requerimientos"] = new requerimientosController().GetRequirementsByEmployee(empleado.cedulaPK);
                TempData.Keep();
                return View();
            }
            else
            {

                TempData.Keep();
                return View();
            }
        }

        /*
         * Efecto: Request POST de EmployeeRequirements
         * Requiere: proyecto seleccionado (codigo)
         * Modifica: NA
         */
        [HttpPost]
        public ActionResult EmployeeRequirements2(proyectos proyecto)
        {
            TempData["empSelect"] = null;
            if (proyecto.codigoPK != 0)
            {
                TempData["proyectoSeleccionado"] = proyecto;
                var lider = new rolesController().getLiderId(proyecto.codigoPK);
                TempData["liderDeProyecto"] = new empleadosController().GetEmployee(lider);
                TempData["requerimientos"] = new requerimientosController().GetRequirementsByProyect(proyecto.codigoPK);
                foreach (var item in TempData["requerimientos"] as List<ProyectoIntegrador_mejorado.Models.requerimientos>)
                {
                    var empleado = new empleadosController().GetEmployee(item.cedulaEmpleadoFK);
                    item.descripcion = empleado.nombre + " " + empleado.apellido1 + " " + empleado.apellido2;
                }
                TempData.Keep();
                return View();
            }
            else
            {
                TempData.Keep();
                return RedirectToAction("EmployeeRequirements", "reportes");
            }
        }

        /*
         * EFE: verifica si el usuario es desarrollaro o jefe de desarrollo y le presenta los datos de los proyectos correspondientes
         * REQ: NA
         * MOD: busca el nombre del proyecto y lo agrega
         */
        public ActionResult TotalTimes()
        {
            TempData["usuarioEsJefe"] = null;
            TempData["proyectos"] = null;
            var user = User.Identity.GetUserName();
            var emple = new empleadosController().ExistEmail(user);
            /*si el usuario es empleado, mostrar de una vez su vista*/
            if (emple.Count() > 0)   //es empleado
            {
                //obteniendo la cedula del empleado
                var cedula = emple[0].cedulaPK;
                bool liderNow = new rolesController().idLiderNow(cedula);
                if (liderNow == true)
                {
                    TempData["proyectos"] = new requerimientosController().GetTotalTimes(cedula);
                    foreach (var item in (TempData["proyectos"] as IEnumerable<ProyectoIntegrador_mejorado.Models.ProyectTimesModel>))
                    {
                        var proyecto = new proyectosController().ProjectByCode(item.codigoProy);
                        if (proyecto.fechaFinal != null)
                        {
                            item.terminado = true;
                        }
                        else
                        {
                            item.terminado = false;
                        }
                        item.nombreProyecto = proyecto.nombre;
                    }
                    TempData.Keep();
                }
            }
            else   // es de soporte o el jefe de desarrollo
            {
                TempData["usuarioEsJefe"] = "si";
                TempData["proyectos"] = new requerimientosController().GetTotalTimes(null);
                foreach (var item in (TempData["proyectos"] as IEnumerable<ProyectoIntegrador_mejorado.Models.ProyectTimesModel>))
                {
                    var proyecto = new proyectosController().ProjectByCode(item.codigoProy);
                    var liderId = new rolesController().getLiderId(item.codigoProy);
                    var lider = new empleadosController().GetEmployee(liderId);
                    if (proyecto.fechaFinal != null)
                    {
                        item.terminado = true;
                    }
                    else
                    {
                        item.terminado = false;
                    }
                    item.nombreProyecto = proyecto.nombre;
                    if (lider != null)
                    {
                        item.lider = lider.nombre + " " + lider.apellido1 + " " + lider.apellido2;
                    }
                }
                TempData.Keep();
            }
            return View();
        }

        //[Authorize(Roles = "Soporte, JefeDesarrollo,Lider")]
        //método para obtener los empleados asignados y disponibles
        public ActionResult DisponibilidadEmpleados()
        {

            //limpio los datos  para asegurarme que no hayan datos que no correspondan
            TempData["empDisponibles"] = null;
            TempData["empOcupados"] = null;
            TempData["pryActual"] = null;
            var user = User.Identity.GetUserName(); //obteniendo la identidad del empleado
            var emple = new empleadosController().ExistEmail(user);
           
            
            /*si el usuario es empleado, mostrar de una vez su vista*/
            if (emple.Count() > 0)   //es empleado
            {
                var cedula = emple[0].cedulaPK;
                bool esLider = new rolesController().idLiderNow(cedula);
                if (esLider == true) { //si es líder de algun proyecto puedo mostrar lo que corresponde a su proyecto, de lo contrario vista en blanco
                    TempData["empDisponibles"] = new empleadosController().GetFreeEmployees();//empleados disponibles
                    var proy = new proyectosController().GetLiderProyectoActual(cedula);//obtenego de cual proyecto soy lider
                    TempData["empOcupados"] = new empleadosController().GetEmployeeBusyProject(cedula,proy[0].codigoPK);//obtengo los empleados ocupados en mi proyecto
                    TempData.Keep();
                }
                
            }
            else
            { //es jefe de desarrollo/soporte , puedo mostrar todos los datos sin restricción
                TempData["empDisponibles"] = new empleadosController().GetFreeEmployees();//obtengo los empleados libres
                TempData["empOcupados"] = new empleadosController().GetEmployeeBusyProject(null, 0);//obtengo los empleados ocupados
                TempData.Keep();//hago que conserve los datos
            }

            //regreso la vista
            return View();
        }

        /*
         * Efecto: Request GET de EmployeeHistory
         * Requiere: NA
         * Modifica: NA
         */
        public ActionResult EmployeeHistory()
        {
            List<empleados> employees = new empleadosController().Pass();
            TempData["employees"] = new SelectList(employees, "cedulaPK", "nombre");
            TempData.Keep();
            return View();
        }

        /*
         * Efecto: Request POST de EmployeeHistory
         * Requiere: cédula de empleado
         * Modifica: NA
         */
        [HttpPost]
        public ActionResult EmployeeHistory(empleados employee)
        {
            TempData.Keep();
            if (employee.cedulaPK != null)
            {
                List<EmployeeHistoryModel> employeeHistory = new List<EmployeeHistoryModel>();
                List<roles> employeeRoles = new rolesController().getEmployeeRoles(employee.cedulaPK);
                foreach (roles rol in employeeRoles)
                {
                    EmployeeHistoryModel participation = new EmployeeHistoryModel();
                    participation.projectName = new proyectosController().getProjectName(rol.codigoProyectoFK);
                    participation.executedRole = rol.rol;
                    int? requirementsDays = new requerimientosController().GetRequirementsDays(rol.codigoProyectoFK, rol.cedulaFK);
                    if (requirementsDays != null && rol.rol == "Desarrollador")
                        participation.dedicatedHours = (int)requirementsDays * 8;
                    employeeHistory.Add(participation);
                }
                TempData["employeeHistory"] = employeeHistory;
            }
            return View();
        }

        /*
         * Efecto: Request GET de RequirementDurationAnalisis
         * Requiere: NA
         * Modifica: NA
         */
        public ActionResult RequirementDurationAnalisis()
        {
            List<ComplexityModel> complexities = new requerimientosController().GetComplexities();
            TempData["complexities"] = new SelectList(complexities, "complexity", "strComplexity");
            TempData.Keep();
            return View();
        }

        /*
         * Efecto: Request POST de RequirementDurationAnalisis
         * Requiere: complejidad de requerimiento
         * Modifica: NA
         */
        [HttpPost]
        public ActionResult RequirementDurationAnalisis(ComplexityModel complejidad)
        {
            TempData.Keep();
            List<RequirementDurationsModel> requirementDurationsInfo = new requerimientosController().GetRequirementDurationsInfo(complejidad.complexity);
            TempData["requirementsDurationInfo"] = requirementDurationsInfo;
            return View();
        }

        /*
        * Efecto: Request GET de ProjectRequirementsState
        * Requiere: NA
        * Modifica: NA
        */
        public ActionResult ProjectRequirementsState()
        {
            var user = User.Identity.GetUserName();
            var client = new clientesController().ExistEmail(user);
            if ( client.Count() > 0 )
            {
                TempData["clientId"] = client[0].cedulaPK;
                List<proyectos> proyectos = new proyectosController().ProyectsByClient(client[0].cedulaPK);
                TempData["projects"] = new SelectList(proyectos, "codigoPK", "nombre");
                TempData.Keep();
            }
            return View();
        }

        /*
        * Efecto: Request POST de ProjectRequirementsState
        * Requiere: ID proyecto
        * Modifica: NA
        */
        [HttpPost]
        public ActionResult ProjectRequirementsState(FechasModel proyecto)
        {
            TempData.Keep();
            string cedCliente = TempData["clientId"] as string;
            int? cedula = Convert.ToInt32(cedCliente);
            if ( proyecto.codigoProy == 0 )
                TempData["requirementsInfo"] = db.requerimientosDeUnProyecto(cedula, null).ToList();
            else
                TempData["requirementsInfo"] = db.requerimientosDeUnProyecto(cedula, proyecto.codigoProy).ToList();
            return View();
        }

        /*
         * Efecto: Request GET de cuentaDeRequerimientos
         * Requiere: NA
         * Modifica: NA
         */
        public ActionResult cuentaDeRequerimientos()
        {
            var user = User.Identity.GetUserName();
            var client = new clientesController().ExistEmail(user);
            if (client.Count() > 0)
            {
                TempData["clientId"] = client[0].cedulaPK;
                List<proyectos> proyectos = new proyectosController().ProyectsByClient(client[0].cedulaPK);
                TempData["projects"] = new SelectList(proyectos, "codigoPK", "nombre");
                TempData.Keep();
            }
            return View();
        }

        /*
       * Efecto: Request POST de ProjectRequirementsState
       * Requiere: ID proyecto
       * Modifica: NA
       */
        [HttpPost]
        public ActionResult cuentaDeRequerimientos(FechasModel proyecto)
        {
            TempData.Keep();
            string cedCliente = TempData["clientId"] as string;
            int? cedula = Convert.ToInt32(cedCliente);
            if (proyecto.codigoProy == 0)
                TempData["requirementsData"] = db.requerimientosFinalizadosEncurso(cedula, null).ToList();
            else
                TempData["requirementsData"] = db.requerimientosFinalizadosEncurso(cedula, proyecto.codigoProy).ToList();
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
