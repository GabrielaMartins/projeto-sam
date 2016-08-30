using SamApi.Attributes.Authorization;
using SamApi.Filters;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SamApi
{
    /// <summary>
    /// 
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config)
        {

            // Enable CORS
            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));

            // Apply exception filter
            config.Filters.Add(new SamExceptionFilter());

            // Apply model validation filter
            config.Filters.Add(new SamModelValidationFilter());

            // Apply token validation filter
            config.Filters.Add(new SamTokenAuthorizer());

            // Isso deveria habilitar o serializador json para camel case
            //config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            //config.Formatters.JsonFormatter.UseDataContractJsonSerializer = false;

            // Remove o formatador de XML 
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            // Web API routes
            config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/Home",
            //    defaults: new { id = RouteParameter.Optional }
            //);

        }
    }
}
