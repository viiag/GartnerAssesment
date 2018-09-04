using System.Web.Http;

namespace Mars_Rover_Webservices
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.MapHttpAttributeRoutes();
        }
    }
}
