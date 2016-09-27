using SamApi.Attributes.Authorization;
using MessageSystem.Mensagem;
using SamApiModels.Agendamento;
using SamServices.Services;
using Swashbuckle.Swagger.Annotations;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SamApiModels.Evento;
using SamApiModels.Votacao;
using SamApiModels.Models.Votacao;

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
        /// <param name="agendamento">Representa informações sobre aprovação do evento</param>
        [SwaggerResponse(HttpStatusCode.OK, "Caso seja possível aceitar a solicitação do evento do SAM", typeof(DescriptionMessage))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Caso a requisição não seja autorizada", typeof(DescriptionMessage))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Caso occora um erro não previsto", typeof(DescriptionMessage))]
        [SamResourceAuthorizer(Roles = "rh")]
        [HttpPost]
        [Route("schedule/approve")]
        public HttpResponseMessage ApproveScheduling(AprovaAgendamentoViewModel agendamento)
        {
            var foiAprovado = EventoServices.AprovaAgendamento(agendamento);
            if (foiAprovado)
                return Request.CreateResponse(HttpStatusCode.OK, new DescriptionMessage(HttpStatusCode.OK, "Scheduling Approved", $"You accepted the event #{agendamento.Evento}"));
            else
                return Request.CreateResponse(HttpStatusCode.OK, new DescriptionMessage(HttpStatusCode.OK, "Scheduling Denied", $"You denied the event #{agendamento.Evento}"));
        }

        /// <summary>
        /// Atribui pontos ao usuário baseado em um evento do SAM
        /// </summary>
        /// <param name="atribuicao">Representa os dados da atribuição de pontos</param>
        [SwaggerResponse(HttpStatusCode.OK, "Caso seja possível atribuir os pontos ao usuário do SAM", typeof(DescriptionMessage))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Caso o usuário requerido não seja encontrado na base de dados do SAM", typeof(DescriptionMessage))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Caso a requisição não seja autorizada", typeof(DescriptionMessage))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Caso occora um erro não previsto", typeof(DescriptionMessage))]
        [SamResourceAuthorizer(Roles = "rh")]
        [HttpPost]
        [Route("assignment")]
        public HttpResponseMessage AtribuiPontos(AtribuicaoPontosUsuarioViewModel atribuicao)
        {
            var foiAtribuido = EventoServices.ProcessaAtribuicaoDePontos(atribuicao);

            if (foiAtribuido)
                return Request.CreateResponse(HttpStatusCode.OK, new DescriptionMessage(HttpStatusCode.OK, "points granted", $"You granted points to user '{atribuicao.Usuario}' for the event #{atribuicao.Evento}"));
            else
                return Request.CreateResponse(HttpStatusCode.OK, new DescriptionMessage(HttpStatusCode.OK, "Denied", $"You denied points to user '{atribuicao.Usuario}' for the event #{atribuicao.Evento}"));
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
        [Route("promotion/approve")]
        public HttpResponseMessage ApprovePromotion(EventoPromocaoViewModel promocao)
        {
            var foiAprovado = PromocaoServices.AprovaPromocao(promocao);
            if(foiAprovado)
                return Request.CreateResponse(HttpStatusCode.OK, new DescriptionMessage(HttpStatusCode.OK, "Promotion Approved", $"You accepted the event #{promocao.Evento}"));
            else
                return Request.CreateResponse(HttpStatusCode.OK, new DescriptionMessage(HttpStatusCode.OK, "Promotion Denied", $"You denied the event #{promocao.Evento}"));
        }

        /// <summary>
        /// Registra o valor da votação de um usuário em um item do SAM
        /// </summary>
        /// <param name="vote">
        /// Representa os valores de um voto para um certo evento do SAM
        /// </param>
        [SwaggerResponse(HttpStatusCode.OK, "Caso seja possível efetuar o voto", typeof(DescriptionMessage))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Caso a requisição não seja autorizada", typeof(DescriptionMessage))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Caso occora um erro não previsto", typeof(DescriptionMessage))]
        [SamResourceAuthorizer(Roles = "funcionario")]
        [Route("vote")]
        [HttpPost]
        public HttpResponseMessage Vote(AddVotoViewModel vote)
        {
            VotacaoServices.CriaVoto(vote);
            return Request.CreateResponse(HttpStatusCode.Created, new DescriptionMessage(HttpStatusCode.Created, "You have voted", "Thanks for your vote"));

        }

        /// <summary>
        /// Encerra uma votação do SAM e atribuí a pontuação votada ao item do evento de votação
        /// </summary>
        /// <param name="votacao">Representa os dados da votação a ser encerrada</param>
        [SwaggerResponse(HttpStatusCode.OK, "Caso seja possível encerrar o evento do SAM", typeof(DescriptionMessage))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Caso a requisição não seja autorizada", typeof(DescriptionMessage))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Caso occora um erro não previsto", typeof(DescriptionMessage))]
        [SamResourceAuthorizer(Roles = "rh")]
        [Route("vote/close")]
        [HttpPost]
        public HttpResponseMessage Close(CloseVotacaoViewModel votacao)
        {
            VotacaoServices.EncerraVotacao(votacao);
            return Request.CreateResponse(HttpStatusCode.OK, new DescriptionMessage(HttpStatusCode.OK, "Closed", $"You closed the event #{votacao.Evento}"));
        }
    }
}
