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
                evento.tipo = "agendamento";

                rep.Add(evento);
                rep.SubmitChanges();

                // create a pendency to rh based on this event
                GenerateHrPendencyFor(evento);
            }
        }

        public static void AprovaAgendamento(int id)
        {
            using (var eventRep = DataAccess.Instance.GetEventoRepository())
            using (var pendencyRep = DataAccess.Instance.GetPendenciaRepository())
            {

                // recupera o evento
                var evento = eventRep.Find(e => e.id == id).SingleOrDefault();

                // cria um novo evento
                var novoEvento = new Evento()
                {
                    estado = false,
                    data = evento.data,
                    item = evento.item,
                    usuario = evento.usuario,
                    tipo = "atividade"
                };
               
                eventRep.Add(novoEvento);
                eventRep.SubmitChanges();

                // gera pendencia para o RH aprovar (atribuir pontos para o novo evento)
                GenerateHrPendencyFor(novoEvento);

                // deleta a pendencia associada ao evento de agendamento
                var pendency = pendencyRep.Find(p => p.evento == id).SingleOrDefault();
                pendencyRep.Delete(pendency.id);
                pendencyRep.SubmitChanges();

                // gera pendencia (notificação) para o funcionario que solicitou o agendamento
                GenerateStaffPendencyFor(evento, evento.Usuario);
            }
        }

        private static void GenerateStaffPendencyFor(Evento evt, Usuario user)
        {
            using (var pendencyRep = DataAccess.Instance.GetPendenciaRepository())
            {
                var pendencia = new Pendencia()
                {
                    usuario = user.id,
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
