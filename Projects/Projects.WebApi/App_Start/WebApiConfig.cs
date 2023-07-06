using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Projects.WebApi
{
    public static class WebApiConfig
    {
        public static readonly string connectionString = "Server=localhost;Port=5432;Database=postgres;User Id=postgres;Password=22482433;";
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
