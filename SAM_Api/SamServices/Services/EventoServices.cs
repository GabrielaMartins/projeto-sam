﻿using AutoMapper;
using Opus.DataBaseEnvironment;
using SamApiModels.Evento;
using SamApiModels.User;
using SamDataBase.Model;
using System.Collections.Generic;
using SamApiModels.Models.Agendamento;
using System;
using System.Linq;

namespace SamServices.Services
{
    public static class EventoServices
    {
        public static List<EventoViewModel> RecuperaEventos(int? quantidade = null)
        {
            using (var eventRep = DataAccess.Instance.GetEventoRepository())
            {
                var events = eventRep.RecuperaEventos(quantidade);

                var eventsViewModel = Mapper.Map<List<Evento>, List<EventoViewModel>>(events);

                return eventsViewModel;
            }
        }

        public static List<UltimoEventoViewModel> RecuperaUltimosEventos(int? quantidade = null)
        {
            using (var itemRep = DataAccess.Instance.GetItemRepository())
            {
                var lastEventsViewModel = new List<UltimoEventoViewModel>();
                var events = RecuperaEventos(quantidade);
                foreach(var evt in events)
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

                // create a pendency to new event
                GeneratePendencyFor(evento);
            }
        }

        private static void GeneratePendencyFor(Evento evt)
        {
            using (var pendencyRep = DataAccess.Instance.GetPendenciaRepository())
            using (var userRep = DataAccess.Instance.GetUsuarioRepository())
            {
                var users = userRep.Find(u => u.perfil == "RH").ToList();
                foreach (var u in users)
                {

                    var pendencia = new Pendencia()
                    {
                        usuario = u.id,
                        evento = evt.id,
                        Evento = evt,
                        Usuario = evt.Usuario,
                        estado = false
                    };

                    pendencyRep.Add(pendencia);
                    pendencyRep.SubmitChanges();
                }

            }
        }
    }
}