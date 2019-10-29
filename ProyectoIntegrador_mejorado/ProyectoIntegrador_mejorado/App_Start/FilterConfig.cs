using System.Web;
using System.Web.Mvc;

namespace ProyectoIntegrador_mejorado
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
