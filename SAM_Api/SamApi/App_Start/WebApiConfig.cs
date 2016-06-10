using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net.Http.Headers;
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
            
            // Web API configuration and services
            var formatters = config.Formatters;
            var jsonFormatter = formatters.JsonFormatter;
            var serializerSettings = jsonFormatter.SerializerSettings;

            // Remove XML formatting 
            formatters.Remove(config.Formatters.XmlFormatter);

            jsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json"));
            
            // Configure our JSON output
            serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            serializerSettings.Formatting = Formatting.Indented;
            serializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            serializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;

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
