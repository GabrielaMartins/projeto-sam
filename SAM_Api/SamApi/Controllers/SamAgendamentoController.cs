using DefaultException.Models;
using SamApi.Attributes.Authorization;
using SamApiModels.Models.Agendamento;
using SamServices.Services;
using Swashbuckle.Swagger.Annotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SamApi.Controllers
{
    /// <summary>
    /// Permite ações referente ao agendamento de um evento do SAM
    /// </summary>
    [RoutePrefix("api/sam/scheduling")]
    public class SamAgendamentoController : ApiController
    {

        /// <summary>
        /// Cria o agendamento de um evento no SAM
        /// </summary>
        [SwaggerResponse(HttpStatusCode.OK, "Caso seja possível agendar o evento do SAM", typeof(DescriptionMessage))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Caso a requisição não seja autorizada", typeof(DescriptionMessage))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Caso occora um erro não previsto", typeof(DescriptionMessage))]
        [SamResourceAuthorizer(Roles = "funcionario")]
        [HttpPost]
        [Route("create")]
        public HttpResponseMessage Create(AgendamentoViewModel agendamento)
        {
            EventoServices.CriaAgendamento(agendamento);
            return Request.CreateResponse(HttpStatusCode.Created, new DescriptionMessage(HttpStatusCode.Created, "Scheduling done", ""));
            
        }
    }
}
