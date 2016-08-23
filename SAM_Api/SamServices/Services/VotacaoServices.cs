using AutoMapper;
using DefaultException.Models;
using Opus.DataBaseEnvironment;
using SamApiModels.Evento;
using SamApiModels.Votacao;
using SamDataBase.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SamServices.Services
{
    public static class VotacaoServices
    {
        public static VotacaoViewModel RecuperaVotacao(int evt)
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

                return votacaoViewModel;
            }
        }

        public static void CriaVoto(AddVotoViewModel vote)
        {
            using (var rep = DataAccess.Instance.GetResultadoVotacoRepository())
            using (var eventRep = DataAccess.Instance.GetEventoRepository())
            {
                // some validations
                var evt = eventRep.Find(e => e.id == vote.Evento).SingleOrDefault();
                if (evt == null)
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
            }
        }
    }
}
