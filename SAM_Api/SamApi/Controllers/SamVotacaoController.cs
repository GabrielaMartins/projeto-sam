using AutoMapper;
using DefaultException.Models;
using Opus.DataBaseEnvironment;
using SamApi.Attributes.Authorization;
using SamApiModels.Evento;
using SamApiModels.Votacao;
using SamDataBase.Model;
using Swashbuckle.Swagger.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

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
        [SwaggerResponse(HttpStatusCode.OK, "Caso seja possível obter o resultado da votação de um evento do do SAM", typeof(VotacaoViewModel))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Caso a requisição não seja autorizada", typeof(DescriptionMessage))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Caso occora um erro não previsto", typeof(DescriptionMessage))]
        [SamResourceAuthorizer(Roles = "rh,funcionario")]
        [Route("{evt}")]
        [HttpGet]
        public HttpResponseMessage Get(int evt)
        {

            using (var repVotacao = DataAccess.Instance.GetResultadoVotacoRepository())
            using (var repEvento = DataAccess.Instance.GetEventoRepository())
            {
                var evento = repEvento.Find(x => x.id == evt).SingleOrDefault();
                if (evento.tipo != "votacao")
                {
                    throw new ExpectedException(HttpStatusCode.Unauthorized, "Not a voting event", "you can not get events that are not voting events");
                }

                var resultados = repVotacao.RecuperaVotacaoParaOEvento(evt);

                var votos = Mapper.Map<List<ResultadoVotacao>, List<VotoViewModel>>(resultados);

                var votacaoViewModel = new VotacaoViewModel()
                {
                    Evento = Mapper.Map<Evento, EventoViewModel>(evento),
                    Votos = votos
                };

                return Request.CreateResponse(HttpStatusCode.OK, votacaoViewModel);
            }
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
            using (var rep = DataAccess.Instance.GetResultadoVotacoRepository())
            using(var eventRep = DataAccess.Instance.GetEventoRepository())
            {
                // some validations
                var evt = eventRep.Find(e => e.id == vote.Evento).SingleOrDefault();
                if(evt == null)
                {
                    throw new ExpectedException(HttpStatusCode.NotFound, "Event not found", $"The event #{vote.Evento} could not be find");
                }
                else if (evt.tipo != "votacao")
                {
                    throw new ExpectedException(HttpStatusCode.BadRequest, "Is not a voting event", $"The event #{vote.Evento} could not be voted because it's not a voting event");
                }

                var resultadoVotacao = Mapper.Map<AddVotoViewModel, ResultadoVotacao>(vote);

                rep.Add(resultadoVotacao);
                rep.SubmitChanges();

                return Request.CreateResponse(HttpStatusCode.Created, new DescriptionMessage(HttpStatusCode.Created, "You have voted", "Thanks for your vote"));
            }
        }
    }
}
