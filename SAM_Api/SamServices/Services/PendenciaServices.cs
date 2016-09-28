using AutoMapper;
using Opus.DataBaseEnvironment;
using SamApiModels.Pendencia;
using SamDataBase.Model;
using System.Collections.Generic;
using System.Linq;
using System;

namespace SamServices.Services
{
    public static class PendenciaServices
    {
        public static PendenciaEventoViewModel Recupera(int id)
        {
            using (var rep = DataAccess.Instance.GetPendenciaRepository())
            {
                var pendencia = rep.Find(p => p.id == id).SingleOrDefault();

                var pendenciaViewModel = Mapper.Map<Pendencia, PendenciaEventoViewModel>(pendencia);

                return pendenciaViewModel;
            }
        }

        public static List<PendenciaEventoViewModel> RecuperaTodas()
        {
            using (var rep = DataAccess.Instance.GetPendenciaRepository())
            {
                var pendencias = rep.GetAll().ToList();

                var pendenciasViewModel = Mapper.Map<List<Pendencia>, List<PendenciaEventoViewModel>>(pendencias);

                return pendenciasViewModel;
            }
        }

        public static void Delete(int id)
        {
            using (var rep = DataAccess.Instance.GetPendenciaRepository())
            {

                rep.Delete(x => x.id == id);
                rep.SubmitChanges();
            }
        }

        public static void GenerateEmployeesPendencyFor(Evento evt)
        {
            using (var userRep = DataAccess.Instance.GetUsuarioRepository())
            using (var pendencyRep = DataAccess.Instance.GetPendenciaRepository())
            {
                var usuarios = userRep.Find(u => u.perfil != "rh").ToList();
                foreach (var u in usuarios)
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

        public static void GenerateEmployeePendencyFor(Evento evt)
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

        public static void GenerateHrPendencyFor(Evento evt)
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

        public static void RemoveHrPendencyFor(Evento evento)
        {
            using (var pendencyRep = DataAccess.Instance.GetPendenciaRepository())
            {
                // remove a(s) pendencia(s) associada(s) a esse evento vinculadas aos RH
                var pendencies = pendencyRep.Find(p => p.evento == evento.id && p.Usuario.perfil == "rh").ToList();
                foreach (var p in pendencies)
                {
                    pendencyRep.Delete(p.id);
                    pendencyRep.SubmitChanges();
                }
            }
        }

        public static void RemoveEmployeesPendencyFor(Evento evento)
        {
            using (var pendencyRep = DataAccess.Instance.GetPendenciaRepository())
            {

                // remove a(s) pendencia(s) associada(s) ao evento vinculadas aos funcionários
                var pendencies = pendencyRep.Find(p => p.evento == evento.id && p.Usuario.perfil != "rh").ToList();
                foreach (var p in pendencies)
                {
                    pendencyRep.Delete(p.id);
                    pendencyRep.SubmitChanges();
                }
            }
        }

        public static void CloseEmployeesPendencyFor(Evento evento)
        {
            using (var pendencyRep = DataAccess.Instance.GetPendenciaRepository())
            {

                // remove a(s) pendencia(s) associada(s) ao evento vinculadas aos funcionários
                var pendencies = pendencyRep.Find(p => p.evento == evento.id && p.Usuario.perfil != "rh").ToList();
                foreach (var p in pendencies)
                {
                    p.estado = true;
                    pendencyRep.Update(p);
                    pendencyRep.SubmitChanges();
                }
            }
        }

        public static void RemoveEmployeePendencyFor(Evento evento, int usuario)
        {
            using (var pendencyRep = DataAccess.Instance.GetPendenciaRepository())
            {

                // Remove a(s) pendencia(s) associada(s) ao evento vinculadas aos funcionários
                var pendencies = pendencyRep.Find(p => p.evento == evento.id && p.usuario == usuario).ToList();
                foreach (var p in pendencies)
                {
                    pendencyRep.Delete(p);
                    pendencyRep.SubmitChanges();
                }
            }
        }

        public static void UpdateEmployeePendencyFor(Evento evento, int usuario, bool value = true )
        {
            using (var pendencyRep = DataAccess.Instance.GetPendenciaRepository())
            {

                // Encerra a(s) pendencia(s) associada(s) ao evento vinculadas aos funcionários
                var pendencies = pendencyRep.Find(p => p.evento == evento.id && p.usuario == usuario).ToList();
                foreach (var p in pendencies)
                {
                    p.estado = value;
                    pendencyRep.Update(p);
                    pendencyRep.SubmitChanges();
                }
            }
        }
    }
}
