using AutoMapper;
using DefaultException.Models;
using Opus.DataBaseEnvironment;
using SamApiModels.Evento;
using SamApiModels.Votacao;
using SamDataBase.Model;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SamApi.Controllers
{
    [RoutePrefix("api/sam/vote")]
    public class SamVotacaoController : ApiController
    {

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
    }
}
