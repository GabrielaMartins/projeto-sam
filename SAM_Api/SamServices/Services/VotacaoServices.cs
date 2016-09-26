using AutoMapper;
using Opus.DataBaseEnvironment;
using MessageSystem.Erro;
using SamApiModels.Evento;
using SamApiModels.Models.Votacao;
using SamApiModels.Votacao;
using SamDataBase.Model;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System;

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
                    throw new ErroEsperado(HttpStatusCode.Unauthorized, "Not a voting event", "you can not get events that are not voting events");
                }

                var resultados = repVotacao.RecuperaVotacaoParaOEvento(evt);

                var votos = Mapper.Map<List<ResultadoVotacao>, List<VotoViewModel>>(resultados);

                return new VotacaoViewModel()
                {
                    Evento = Mapper.Map<Evento, EventoViewModel>(evento),
                    Votos = votos
                };
            }
        }

        public static void CriaVoto(AddVotoViewModel vote)
        {
            using (var pendencyRep = DataAccess.Instance.GetPendenciaRepository())
            using (var rep = DataAccess.Instance.GetResultadoVotacoRepository())
            using (var eventRep = DataAccess.Instance.GetEventoRepository())
            {
                // some validations
                var evt = eventRep.Find(e => e.id == vote.Evento).SingleOrDefault();
                if (evt.tipo != "votacao")
                {
                    throw new ErroEsperado(HttpStatusCode.BadRequest, "It's not a voting event", $"The event #{vote.Evento} could not be voted because it's not a voting event");
                }

                var resultadoVotacao = Mapper.Map<AddVotoViewModel, ResultadoVotacao>(vote);

                rep.Add(resultadoVotacao);
                rep.SubmitChanges();

                // atualiza a pendencia desse usuario
                var pendencia = pendencyRep.Find(p => p.evento == vote.Evento && p.Usuario.samaccount == vote.Usuario).SingleOrDefault();
                pendencia.estado = true;
                pendencyRep.Update(pendencia);
                pendencyRep.SubmitChanges();
            }
        }

        public static void CriaVotacao(AddEventoVotacaoViewModel evt)
        {
            using (var eventRep = DataAccess.Instance.GetEventoRepository())
            {
                var evento = Mapper.Map<AddEventoVotacaoViewModel, Evento>(evt);
                var x = eventRep.AddAndCommit(evento);
                
                // gera pendencia para os funcionarios comuns votarem
                // e no caso do rh, a pendencia significa fechar a votacao
                using (var pendencyRep = DataAccess.Instance.GetPendenciaRepository())
                {
                    var usuarios = UsuarioServices.RecuperaTodos();
                    foreach (var u in usuarios)
                    {
                        var pendencia = new Pendencia()
                        {
                            usuario = u.id,
                            evento = x.id,
                            estado = false,
                            Evento = null,
                            Usuario = null
                        };

                        pendencyRep.Add(pendencia);
                        pendencyRep.SubmitChanges();
                    }
                }
            }
        }

        public static void EncerraVotacao(CloseVotacaoViewModel votacao)
        {
            using (var itemRep = DataAccess.Instance.GetItemRepository())
            using (var eventRep = DataAccess.Instance.GetEventoRepository())
            {
                var eventoVotacao = eventRep.Find(e => e.id == votacao.Evento).SingleOrDefault();
                if(eventoVotacao.tipo != "votacao")
                {
                    throw new ErroEsperado(HttpStatusCode.BadRequest, "It's not a voting event", $"The event #{votacao.Evento} could not be closed because it's not a voting event");
                }

                // encerra o evento
                eventoVotacao.estado = true;
                eventRep.Update(eventoVotacao);
                eventRep.SubmitChanges();

                // atribui pontos ao item caso ainda não tenha sido pontuado
                var item = itemRep.Find(i => i.id == eventoVotacao.item).SingleOrDefault();
                if (!item.votado)
                {
                    item.dificuldade = votacao.Dificuldade;
                    item.modificador = votacao.Modificador;
                    item.votado = true;
                    itemRep.Update(item);
                    itemRep.SubmitChanges();
                }

                // remove as pendencias para o evento de votacao associadas ao rh
                PendenciaServices.RemoveHrPendencyFor(eventoVotacao);

                // remove as pendencias dos usuarios que votaram
                PendenciaServices.RemoveEmployeesPendencyFor(eventoVotacao);

                // gerar evento de atribuição de pontos para o usuario
                var eventoAtribuicao = new Evento()
                {
                    estado = false,
                    data = eventoVotacao.data,
                    item = eventoVotacao.item,
                    usuario = eventoVotacao.usuario,
                    tipo = "atribuicao"
                };
                eventRep.AddAndCommit(eventoAtribuicao);

                // gera a pendência para o evento associada ao usuario
                PendenciaServices.GenerateEmployeePendencyFor(eventoAtribuicao);

                // gera as pendências para o evento associadas ao rh
                PendenciaServices.GenerateHrPendencyFor(eventoAtribuicao);
            }
        }
    }
}
