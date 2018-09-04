using System.Web;
using System.Web.Mvc;

namespace Mars_Rover_Webservices
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
