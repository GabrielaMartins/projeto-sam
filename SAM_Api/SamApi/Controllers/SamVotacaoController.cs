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
    }
}
