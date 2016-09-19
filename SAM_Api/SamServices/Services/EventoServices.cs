﻿using AutoMapper;
using Opus.DataBaseEnvironment;
using SamApiModels.Evento;
using SamApiModels.User;
using SamDataBase.Model;
using System.Collections.Generic;
using SamApiModels.Models.Agendamento;
using System.Linq;
using System.Net;
using MessageSystem.Erro;

namespace SamServices.Services
{
    public static class EventoServices
    {
        public static List<EventoViewModel> RecuperaEventos(string tipo = null, int? quantidade = null)
        {
            using (var eventRep = DataAccess.Instance.GetEventoRepository())
            {
                var events = eventRep.RecuperaEventos(tipo, quantidade);

                var eventsViewModel = Mapper.Map<List<Evento>, List<EventoViewModel>>(events);

                return eventsViewModel;
            }
        }

        public static List<UltimoEventoViewModel> RecuperaUltimosEventos(int? quantidade = null)
        {
            using (var itemRep = DataAccess.Instance.GetItemRepository())
            {
                var lastEventsViewModel = new List<UltimoEventoViewModel>();
                var events = RecuperaEventos(null, quantidade);
                foreach (var evt in events)
                {
                    var usersInThisEvent = itemRep.RecuperaUsuariosQueFizeram(evt.id);
                    var usersViewModel = Mapper.Map<List<Usuario>, List<UsuarioViewModel>>(usersInThisEvent);

                    var lastEventViewModel = new UltimoEventoViewModel()
                    {
                        Evento = evt,
                        UsuariosQueFizeram = usersViewModel
                    };

                    lastEventsViewModel.Add(lastEventViewModel);
                }

                return lastEventsViewModel;
            }
        }

        /// <summary>
        /// Implements the business logic described in https://goo.gl/UejZYN at 1.
        /// </summary>
        /// <param name="evt">It's the evento to be approved</param>
        public static void CriaAgendamento(AgendamentoViewModel agendamento)
        {
            using (var rep = DataAccess.Instance.GetEventoRepository())
            {
                var evento = Mapper.Map<AgendamentoViewModel, Evento>(agendamento);
               
                rep.Add(evento);
                rep.SubmitChanges();

                // create a pendency to rh based on this event
                PendenciaServices.GenerateHrPendencyFor(evento);

                // create a pendency to employee based on this event
                PendenciaServices.GenerateEmployeePendencyFor(evento);
            }
        }

        /// <summary>
        /// Implements the business logic described in https://goo.gl/UejZYN at 1.
        /// </summary>
        /// <param name="evt">It's the evento to be approved</param>
        public static void AprovaAgendamento(int evt)
        {

            using (var eventRep = DataAccess.Instance.GetEventoRepository())
            using (var pendencyRep = DataAccess.Instance.GetPendenciaRepository())
            {

                // recupera o evento
                var eventoAgendamento = eventRep.Find(e => e.id == evt).SingleOrDefault();
                if(eventoAgendamento.tipo != "agendamento")
                {
                    throw new ErroEsperado(HttpStatusCode.BadRequest, "It's not a scheduling event", $"The event #{evt} could not be approved because it's not a scheduling event");
                }

                // encerra o evento de agendamento
                eventoAgendamento.estado = true;
                eventRep.Update(eventoAgendamento);
                eventRep.SubmitChanges();

                // remove a(s) pendencia(s) associada(s) a esse evento de agendamento vinculadas ao RH
                PendenciaServices.RemoveHrPendencyFor(eventoAgendamento);

                // atualiza o valor do estado da pendencia do usuario aguardando o resultado do agendamento
                var pendency = pendencyRep.Find(p => p.evento == eventoAgendamento.id && p.usuario == eventoAgendamento.usuario).SingleOrDefault();
                PendenciaServices.CloseEmployeePendencyFor(eventoAgendamento, eventoAgendamento.usuario.Value);
               
                // cria o evento resultante da aprovação do agendamento
                eventRep.AddAndCommit(new Evento()
                {
                    estado = false,
                    data = eventoAgendamento.data,
                    item = eventoAgendamento.item,
                    usuario = eventoAgendamento.usuario,
                    tipo = "atividade"
                });

                // Gera um evento de votacao se o item da atividade tem a categoria marcada como:
                if (eventoAgendamento.Item.Categoria.nome == "apresentacao" ||
                   eventoAgendamento.Item.Categoria.nome == "blog tecnico" ||
                   eventoAgendamento.Item.Categoria.nome == "comunidade de software" ||
                   eventoAgendamento.Item.Categoria.nome == "repositorio de codigo")
                {
                    var eventoVotacao = new Evento()
                    {
                        estado = false,
                        data = eventoAgendamento.data,
                        item = eventoAgendamento.item,
                        usuario = eventoAgendamento.usuario,
                        tipo = "votacao"
                    };

                    eventRep.AddAndCommit(eventoVotacao);

                    // gera pendencias para o evento de votacao para todos os funcionarios, significando que é preciso votar
                    PendenciaServices.GenerateEmployeesPendencyFor(eventoVotacao);

                    // gera pendencias para o evento de votacao para o RH, significando que o RH precisa encerrar a votacao
                    PendenciaServices.GenerateHrPendencyFor(eventoVotacao);
                }
                else
                {
                    var eventoAtribuicao = new Evento()
                    {
                        estado = false,
                        data = eventoAgendamento.data,
                        item = eventoAgendamento.item,
                        usuario = eventoAgendamento.usuario,
                        tipo = "atribuicao"
                    };

                    eventRep.AddAndCommit(eventoAtribuicao);

                    // gera pendencia para o evento de atribuicao para o funcionario, significando que está aguardando o resultado
                    PendenciaServices.GenerateEmployeePendencyFor(eventoAtribuicao);

                    // gera pendencia para o evento de atribuicao para o RH, significando que o RH precisa atribuir pontos ao funcionario participante
                    PendenciaServices.GenerateHrPendencyFor(eventoAtribuicao);
                }                
            }
        }
    }
}
