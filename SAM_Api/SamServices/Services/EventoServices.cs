using AutoMapper;
using Opus.DataBaseEnvironment;
using SamApiModels.Evento;
using SamApiModels.User;
using SamDataBase.Model;
using System.Collections.Generic;
using SamApiModels.Agendamento;
using System.Linq;
using System.Net;
using MessageSystem.Erro;
using System.Globalization;

namespace SamServices.Services
{
    public static class EventoServices
    {
        public static List<EventoViewModel> RecuperaEventos(string tipo = null, int? quantidade = null)
        {
            using (var eventRep = DataAccess.Instance.GetEventoRepository())
            {
                var events = eventRep.RecuperaEventos(tipo, quantidade).Where(e => e.processado == false).ToList();

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

        public static bool ProcessaAtribuicaoDePontos(AtribuicaoPontosUsuarioViewModel atribuicao)
        {
            using (var userRep = DataAccess.Instance.GetUsuarioRepository())
            using (var eventRep = DataAccess.Instance.GetEventoRepository())
            {

                // recupera o evento pelo qual o usuário receberá os pontos
                var eventoAtribuicao = eventRep.Find(e => e.id == atribuicao.Evento).SingleOrDefault();
                if (eventoAtribuicao.tipo != "atribuicao")
                {
                    throw new ErroEsperado(HttpStatusCode.BadRequest, "It's not a grant event", $"The event #{eventoAtribuicao.id} could not be processed because it's not a grant event");
                }

                // se não aceitou a atribuição de pontos
                if (!atribuicao.ReceberPontos)
                {
                    eventoAtribuicao.processado = true;
                    eventoAtribuicao.estado = false;

                    eventRep.Update(eventoAtribuicao);
                    eventRep.SubmitChanges();

                    return false;
                }

                // recupera o usuario que receberá os pontos
                var usuario = userRep.Find(u => u.samaccount == atribuicao.Usuario).SingleOrDefault();

                // recupera o item desse evento
                var item = eventoAtribuicao.Item;

                // calcula a pontuação que o usuário receberá
                var pesoCategoria = item.Categoria.peso;
                var pontos = pesoCategoria * item.dificuldade * item.modificador;

                // atualiza a pontuação do usuário
                usuario.pontos += pontos;
                userRep.Update(usuario);
                userRep.SubmitChanges();

                // Remove as pendencias de atribuição para o evento associadas ao RH
                PendenciaServices.RemoveHrPendencyFor(eventoAtribuicao);

                // Remove as pendencias de atribuição para o evento associadas ao funcionário
                PendenciaServices.UpdateEmployeePendencyFor(eventoAtribuicao, usuario.id);

                // encerra o evento de atribuicao
                eventoAtribuicao.processado = true;

                // aceita a atribuição de pontos
                eventoAtribuicao.estado = true;

                eventRep.Update(eventoAtribuicao);
                eventRep.SubmitChanges();

                // encontra o evento de atividade atrelado a atribuição de pontos
                var atividades = eventRep.Find(e =>
                                              e.tipo == "atividade" &&
                                              e.item == eventoAtribuicao.item &&
                                              e.usuario == eventoAtribuicao.usuario &&
                                              e.data == eventoAtribuicao.data
                                              ).ToList();

                // marca como encerrada as atividades (se tudo der certo, deverá sempre vir uma atividade só)
                foreach (var atividade in atividades)
                {
                    atividade.processado = true;
                    atividade.estado = true;
                    eventRep.Update(atividade);
                    eventRep.SubmitChanges();

                    // encerra as pendencias associadas a essa atividade
                    PendenciaServices.RemoveHrPendencyFor(atividade);
                    PendenciaServices.UpdateEmployeePendencyFor(atividade, eventoAtribuicao.usuario.Value);
                }

                return true;
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
                // todo evento de agendamento quando vai para o banco já foi processado e aceito
                var evento = Mapper.Map<AgendamentoViewModel, Evento>(agendamento);
    
                // verifica se ja existe um evento igual no banco
                var eventos = rep.Find(e =>
                                              e.tipo == "agendamento" &&
                                              e.item == evento.item &&
                                              e.usuario == evento.usuario &&
                                              e.data == evento.data
                                              ).ToList();

                // se ja existe, retorna um erro
                if(eventos.Count > 0)
                {
                    throw new ErroEsperado(HttpStatusCode.Forbidden, "Duplicated scheduling", "We have recorded a schedule with this data");
                }

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
        public static bool AprovaAgendamento(AprovaAgendamentoViewModel agendamento)
        {

            using (var eventRep = DataAccess.Instance.GetEventoRepository())
            using (var pendencyRep = DataAccess.Instance.GetPendenciaRepository())
            {

                // recupera o evento
                var eventoAgendamento = eventRep.Find(e => e.id == agendamento.Evento).SingleOrDefault();
                if(eventoAgendamento.tipo != "agendamento")
                {
                    throw new ErroEsperado(HttpStatusCode.BadRequest, "It's not a scheduling event", $"The event #{agendamento.Evento} could not be approved because it's not a scheduling event");
                }

                // se não aprovou
                if (!agendamento.Aprova)
                {
                    // encerra o evento de agendamento
                    eventoAgendamento.processado = true;

                    // aprova o agendamento
                    eventoAgendamento.estado = false;

                    eventRep.Update(eventoAgendamento);
                    eventRep.SubmitChanges();

                    // remove a(s) pendencia(s) associada(s) a esse evento de agendamento vinculadas ao RH
                    PendenciaServices.RemoveHrPendencyFor(eventoAgendamento);

                    // altera a pendencia do usuario
                    PendenciaServices.UpdateEmployeePendencyFor(eventoAgendamento, eventoAgendamento.usuario.Value);

                    //var pendencia = pendencyRep.Find(p => p.evento == eventoAgendamento.id && p.usuario == eventoAgendamento.usuario).SingleOrDefault();
                    //pendencia.estado = true;
                    //pendencyRep.Update(pendencia);
                    //pendencyRep.SubmitChanges();

                    return false;
                }

                // encerra o evento de agendamento
                eventoAgendamento.processado = true;

                // aprova o agendamento
                eventoAgendamento.estado = true;

                // atualiza informações no banco
                eventRep.Update(eventoAgendamento);
                eventRep.SubmitChanges();

                // remove a(s) pendencia(s) associada(s) a esse evento de agendamento vinculadas ao RH
                PendenciaServices.RemoveHrPendencyFor(eventoAgendamento);

                // atualiza o valor do estado da pendencia do usuario aguardando o resultado do agendamento
                var pendency = pendencyRep.Find(p => p.evento == eventoAgendamento.id && p.usuario == eventoAgendamento.usuario).SingleOrDefault();
                PendenciaServices.UpdateEmployeePendencyFor(eventoAgendamento, eventoAgendamento.usuario.Value);
               
                // cria o evento resultante da aprovação do agendamento
                eventRep.AddAndCommit(new Evento()
                {
                    processado = false,
                    estado = false,
                    data = eventoAgendamento.data,
                    item = eventoAgendamento.item,
                    usuario = eventoAgendamento.usuario,
                    tipo = "atividade"
                });
              
                // Gera um evento de votacao se o item da atividade tem a categoria marcada como:
                if (
                    (string.Compare(eventoAgendamento.Item.Categoria.nome, "apresentacao",
                    CultureInfo.CurrentCulture,
                    CompareOptions.IgnoreNonSpace |
                    CompareOptions.IgnoreCase) == 0)
                    ||
                    (string.Compare(eventoAgendamento.Item.Categoria.nome, "blog tecnico",
                    CultureInfo.CurrentCulture,
                    CompareOptions.IgnoreNonSpace |
                    CompareOptions.IgnoreCase) == 0)
                    ||
                    (string.Compare(eventoAgendamento.Item.Categoria.nome, "comunidade de software",
                    CultureInfo.CurrentCulture,
                    CompareOptions.IgnoreNonSpace |
                    CompareOptions.IgnoreCase) == 0)
                    ||
                    (string.Compare(eventoAgendamento.Item.Categoria.nome, "repositorio de codigo",
                    CultureInfo.CurrentCulture,
                    CompareOptions.IgnoreNonSpace |
                    CompareOptions.IgnoreCase) == 0)
                    )
                {
                    var eventoVotacao = new Evento()
                    {
                        processado = false,
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
                        processado = false,
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

                return true;            
            }
        }
    }
}
