using SamApi.Attributes.Authorization;
using SamApi.Filters;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SamApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            // Enable CORS
            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));

            // Apply exception filter
            config.Filters.Add(new SamExceptionFilter());

            // Apply model validation filter
            config.Filters.Add(new SamModelValidationFilter());

            // Apply token validation to all controllers
            config.Filters.Add(new SamTokenAuthorizer());

            // isso deveria habilitar o serializador json para camel case
            //config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            //config.Formatters.JsonFormatter.UseDataContractJsonSerializer = false;

            // Remove XML formatting 
            config.Formatters.Remove(config.Formatters.XmlFormatter);

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
