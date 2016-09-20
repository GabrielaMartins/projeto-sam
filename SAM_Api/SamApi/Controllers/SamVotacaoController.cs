using SamApi.Attributes.Authorization;
using SamApiModels.Evento;
using MessageSystem.Mensagem;
using SamApiModels.Votacao;
using SamServices.Services;
using Swashbuckle.Swagger.Annotations;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SamModelValidationRules.Attributes.Validation;
using SamApiModels.Models.Votacao;

namespace SamApi.Controllers
{
    /// <summary>
    /// Fornece ações para controlar as informações sobre a votação de um item do SAM
    /// </summary>
    [RoutePrefix("api/sam/vote")]
    public class SamVotacaoController : ApiController
    {

        /// <summary>
        /// Retorna o valor da votação de um evento
        /// </summary>
        /// <param name="evt">
        /// Identifica o evento que foi votado
        /// </param>
        [SwaggerResponse(HttpStatusCode.OK, "Caso seja possível obter o resultado da votação de um evento do do SAM", typeof(List<VotoViewModel>))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Caso a requisição não seja autorizada", typeof(DescriptionMessage))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Caso occora um erro não previsto", typeof(DescriptionMessage))]
        [SamResourceAuthorizer(Roles = "rh,funcionario")]
        [Route("{evt}")]
        [HttpGet]
        public HttpResponseMessage Get(int evt)
        {
            var votacao = VotacaoServices.RecuperaVotacao(evt);
            return Request.CreateResponse(HttpStatusCode.OK, votacao);

        }


        /// <summary>
        /// Encerra uma votação do SAM e atribuí a pontuação votada ao item do evento de votação
        /// </summary>
        /// <param name="votacao">Representa os dados da votação a ser encerrada</param>
        [SwaggerResponse(HttpStatusCode.OK, "Caso seja possível encerrar o evento do SAM", typeof(DescriptionMessage))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Caso a requisição não seja autorizada", typeof(DescriptionMessage))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Caso occora um erro não previsto", typeof(DescriptionMessage))]
        [SamResourceAuthorizer(Roles = "rh")]
        [Route("close")]
        [HttpPost]
        public HttpResponseMessage Close(CloseVotacaoViewModel votacao)
        {
            VotacaoServices.EncerraVotacao(votacao);
            return Request.CreateResponse(HttpStatusCode.OK, new DescriptionMessage(HttpStatusCode.OK, "Closed", $"You closed the event #{votacao.Evento}"));
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
        [SamResourceAuthorizer(Roles = "rh,funcionario")]
        [Route("")]
        [HttpPost]
        public HttpResponseMessage Vote(AddVotoViewModel vote)
        {
            VotacaoServices.CriaVoto(vote);
            return Request.CreateResponse(HttpStatusCode.Created, new DescriptionMessage(HttpStatusCode.Created, "You have voted", "Thanks for your vote"));

        }

        /*
        /// <summary>
        /// Cria um novo evento de votacao
        /// </summary>
        /// <param name="evt">
        /// É o evento que será votado
        /// </param>
        [SwaggerResponse(HttpStatusCode.Created, "Caso seja possível criar evento do SAM", typeof(DescriptionMessage))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Caso a requisição não seja autorizada", typeof(DescriptionMessage))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Caso occora um erro não previsto", typeof(DescriptionMessage))]
        [SamResourceAuthorizer(Roles = "rh")]
        [Route("new")]
        [HttpPost]
        public HttpResponseMessage Create(AddEventoVotacaoViewModel evt)
        {
            VotacaoServices.CriaVotacao(evt);
            return Request.CreateResponse(HttpStatusCode.Created, new DescriptionMessage(HttpStatusCode.Created, "Created"));

        }
        */
    }
}
