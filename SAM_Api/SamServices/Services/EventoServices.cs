using AutoMapper;
using Opus.DataBaseEnvironment;
using SamApiModels.Evento;
using SamApiModels.User;
using SamDataBase.Model;
using System.Collections.Generic;
using SamApiModels.Models.Agendamento;
using System.Linq;

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

        public static void CriaAgendamento(AgendamentoViewModel agendamento)
        {
            using (var rep = DataAccess.Instance.GetEventoRepository())
            {
                var evento = Mapper.Map<AgendamentoViewModel, Evento>(agendamento);
               
                rep.Add(evento);
                rep.SubmitChanges();

                // create a pendency to rh based on this event
                GenerateHrPendencyFor(evento);

                // create a pendency to employee based on this event
                GenerateEmployeePendencyFor(evento);
            }
        }

        public static void AprovaAgendamento(int evt, int user)
        {
            /* DUVIDA:
                Todo evento aprovado é também votado?
                Se sim, então criar o evento de votacao após a aprovação

            */

            using (var eventRep = DataAccess.Instance.GetEventoRepository())
            using (var pendencyRep = DataAccess.Instance.GetPendenciaRepository())
            {

                // recupera o evento
                var evento = eventRep.Find(e => e.id == evt).SingleOrDefault();

                // cria o evento resultante da aprovação do agendamento
                var atividade = new Evento()
                {
                    estado = false,
                    data = evento.data,
                    item = evento.item,
                    usuario = evento.usuario,
                    tipo = "atividade"
                };
               
                eventRep.Add(atividade);
                eventRep.SubmitChanges();

                // atualiza o valor do status da pendencia do usuario aguardando o resultado do agendamento
                var pendency = pendencyRep.Find(p => p.evento == evento.id && p.usuario == evento.usuario).SingleOrDefault();
                pendency.estado = true;
                pendencyRep.Update(pendency);
                pendencyRep.SubmitChanges();

                // gera pendencia para o RH dizendo que o usuario tem atribuição de pontos pendente
                GenerateHrPendencyFor(atividade);

                // remove a(s) pendencia(s) associada(s) ao evento de agendamento e ao usuario que está aprovando o agendamento
                var pendencies = pendencyRep.Find(p => p.evento == evt && p.usuario == user).ToList();
                foreach(var p in pendencies)
                {
                    pendencyRep.Delete(p.id);
                    pendencyRep.SubmitChanges();
                }

                // encerra o evento de agendamento
                evento.estado = true;
                eventRep.Update(evento);
                eventRep.SubmitChanges();

                // duvida: gerar pendencia para o usuario da atividade, informando que ele tem pontos para adquirir?
            }
        }

        private static void GenerateEmployeePendencyFor(Evento evt)
        {
            using (var pendencyRep = DataAccess.Instance.GetPendenciaRepository())
            {
                var pendencia = new Pendencia()
                {
                    usuario = evt.usuario,
                    evento = evt.id,
                    estado = false,
                    Evento = null,
                    Usuario = null
                };

                pendencyRep.Add(pendencia);
                pendencyRep.SubmitChanges();
            }
        }

        // generate pendencies to RH users
        private static void GenerateHrPendencyFor(Evento evt)
        {
            using (var pendencyRep = DataAccess.Instance.GetPendenciaRepository())
            using (var userRep = DataAccess.Instance.GetUsuarioRepository())
            {
                var users = userRep.Find(u => u.perfil == "rh").ToList();
                foreach (var u in users)
                {

                    var pendencia = new Pendencia()
                    {
                        usuario = u.id,
                        evento = evt.id,
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
}
