using SamApi.Attributes.Authorization;
using MessageSystem.Mensagem;
using SamApiModels.Models.Agendamento;
using SamModelValidationRules.Attributes.Validation;
using SamServices.Services;
using Swashbuckle.Swagger.Annotations;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SamApiModels.Evento;

namespace SamApi.Controllers
{
    /// <summary>
    /// Permite ações referente ao agendamento de um evento do SAM
    /// </summary>
    [RoutePrefix("api/sam/activity")]
    public class SamAtividadeController : ApiController
    {

        /// <summary>
        /// Cria o agendamento de um evento no SAM
        /// </summary>
        [SwaggerResponse(HttpStatusCode.OK, "Caso seja possível agendar o evento do SAM", typeof(DescriptionMessage))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Caso a requisição não seja autorizada", typeof(DescriptionMessage))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Caso occora um erro não previsto", typeof(DescriptionMessage))]
        [SamResourceAuthorizer(Roles = "funcionario")]
        [HttpPost]
        [Route("schedule")]
        public HttpResponseMessage Create(AgendamentoViewModel agendamento)
        {
            EventoServices.CriaAgendamento(agendamento);
            return Request.CreateResponse(HttpStatusCode.Created, new DescriptionMessage(HttpStatusCode.Created, "Scheduling Done", "A new scheduling was created"));
            
        }

        /// <summary>
        /// Aprova o agendamento de um evento no SAM
        /// </summary>
        /// <param name="evt">Identifica o evento a ser aprovado</param>
        [SwaggerResponse(HttpStatusCode.OK, "Caso seja possível aceitar a solicitação do evento do SAM", typeof(DescriptionMessage))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Caso a requisição não seja autorizada", typeof(DescriptionMessage))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Caso occora um erro não previsto", typeof(DescriptionMessage))]
        [SamResourceAuthorizer(Roles = "rh")]
        [HttpGet]
        [Route("schedule/approve/{evt}")]
        public HttpResponseMessage ApproveScheduling([ValidKey(ValidKeyAttribute.Entities.Evento)]int evt)
        {
            EventoServices.AprovaAgendamento(evt);
            return Request.CreateResponse(HttpStatusCode.OK, new DescriptionMessage(HttpStatusCode.OK, "Scheduling Approved", $"You accepted the event #{evt}"));
        }


        /// <summary>
        /// Aprova a promoção de um usuário do SAM
        /// </summary>
        /// <param name="promocao">Representa os dados da promoção</param>
        [SwaggerResponse(HttpStatusCode.OK, "Caso seja possível aceitar a solicitação do evento do SAM", typeof(DescriptionMessage))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Caso a requisição não seja autorizada", typeof(DescriptionMessage))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Caso occora um erro não previsto", typeof(DescriptionMessage))]
        [SamResourceAuthorizer(Roles = "rh")]
        [HttpGet]
        [Route("promotion/approve/{evt}")]
        public HttpResponseMessage ApprovePromotion(EventoPromocaoViewModel promocao)
        {
            EventoServices.AprovaPromocao(promocao);
            return Request.CreateResponse(HttpStatusCode.OK, new DescriptionMessage(HttpStatusCode.OK, "Scheduling Approved", $"You accepted the event #{promocao.Evento}"));
        }
    }
}
