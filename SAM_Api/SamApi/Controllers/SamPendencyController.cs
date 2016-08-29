using DefaultException.Models;
using SamApiModels.Pendencia;
using SamServices.Services;
using Swashbuckle.Swagger.Annotations;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SamApi.Controllers
{
    /// <summary>
    /// Controla as ações referentes ás pendências do SAM
    /// </summary>
    [RoutePrefix("api/sam/pendency")]
    public class SamPendencyController : ApiController
    {

        /// <summary>
        /// Recupera uma pendência específica do sam
        /// </summary>
        [SwaggerResponse(HttpStatusCode.OK, "Caso seja possível obter a pendência do SAM", typeof(PendenciaEventoViewModel))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Caso a requisição não seja autorizada", typeof(DescriptionMessage))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Caso occora um erro não previsto", typeof(DescriptionMessage))]
        [HttpGet]
        [Route("{pendency}")]
        public HttpResponseMessage Get(int pendency)
        {
            var pendencia = PendencyServices.Recupera(pendency);
            return Request.CreateResponse(HttpStatusCode.OK, pendencia);
            
        }

        /// <summary>
        /// Recupera a lista de pendências do sam
        /// </summary>
        [SwaggerResponse(HttpStatusCode.OK, "Caso seja possível obter a lista de pendências do SAM", typeof(PendenciaEventoViewModel))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Caso a requisição não seja autorizada", typeof(DescriptionMessage))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Caso occora um erro não previsto", typeof(DescriptionMessage))]
        [HttpGet]
        [Route("all")]
        public HttpResponseMessage GetAll()
        {
         
            var pendencias = PendencyServices.RecuperaTodas();
            return Request.CreateResponse(HttpStatusCode.OK, pendencias);
            
        }

        /// <summary>
        /// Remove uma pendência específica do sam
        /// </summary>
        [SwaggerResponse(HttpStatusCode.OK, "Caso seja possível remover a pendência do SAM", typeof(DescriptionMessage))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Caso a requisição não seja autorizada", typeof(DescriptionMessage))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Caso occora um erro não previsto", typeof(DescriptionMessage))]
        [HttpDelete]
        [Route("delete/{pendency}")]
        public HttpResponseMessage Delete(int pendency)
        {
            PendencyServices.Delete(pendency);
            return Request.CreateResponse(HttpStatusCode.OK, new DescriptionMessage(HttpStatusCode.OK, "Pendency removed"));
            
        }

    }
}
