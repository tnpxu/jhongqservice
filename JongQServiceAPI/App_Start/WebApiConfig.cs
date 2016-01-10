using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;
using JongQServiceAPI.Models;

namespace JongQServiceAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "IncludeAction",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //change xml formatt to json
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            //binding rule
            //config.ParameterBindingRules.Insert(0, typeof(User), x => x.BindWithAttribute(new FromUriAttribute()));
        }
    }
}
