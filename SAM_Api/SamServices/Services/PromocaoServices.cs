using MessageSystem.Erro;
using Opus.DataBaseEnvironment;
using SamApiModels.Evento;
using SamApiModels.Promocao;
using SamDataBase.Model;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace SamServices.Services
{
    public static class PromocaoServices
    {

        public static List<ProximaPromocaoViewModel> RecuperaProximasPromocoes()
        {
            var promocoesViewModel = new List<ProximaPromocaoViewModel>();
            var cargos = CargoServices.RecuperaTodos();
            var usuarios = UsuarioServices.RecuperaTodos();

            var db = new SamEntities();
            promocoesViewModel =
            (from c in cargos
             from u in usuarios
             where
             u.Cargo.id != c.id &&
             (c.pontuacao - u.pontos) > 0 &&
             (c.pontuacao - u.pontos) <= (c.pontuacao * 0.2)
             select new ProximaPromocaoViewModel()
             {
                 Usuario = u,
                 PontosFaltantes = (u.ProximoCargo[0].pontuacao - u.pontos)

             })
             .OrderBy(p => p.PontosFaltantes)
             .ToList();

            return promocoesViewModel;
        }

        public static bool AprovaPromocao(EventoPromocaoViewModel promocao)
        {
            using (var repEvento = DataAccess.Instance.GetEventoRepository())
            using (var repUsuario = DataAccess.Instance.GetUsuarioRepository())
            {
                // encontra o evento
                var evento = repEvento.Find(e => e.id == promocao.Evento).SingleOrDefault();
                if(evento.tipo != "promocao")
                {
                    throw new ErroEsperado(HttpStatusCode.BadRequest, "It's not a promotion event", $"The event #{promocao.Evento} could not be voted because it's not a promotion event");
                }

                // encontra o usuário
                var usuario = repUsuario.Find(u => u.samaccount == promocao.Usuario).SingleOrDefault();


                if (!promocao.PodePromover)
                {
                    // encerra o evento de promocao
                    evento.processado = true;

                    // informa o resultado do evento (recusado)
                    evento.estado = false;

                    // remove as pendencias associadas ao evento do rh
                    PendenciaServices.RemoveHrPendencyFor(evento);

                    // encerra a pendencia associada ao funcionário naquele evento (informa que nao foi aceito) e
                    // remove as outras pendencias de promocao desse usuario
                    using (var pendencyRep = DataAccess.Instance.GetPendenciaRepository())
                    {

                        // Encerra a pendencia associada ao evento
                        var pendencia = pendencyRep.Find(p => p.evento == evento.id).SingleOrDefault();
                        pendencia.estado = false;
                        pendencyRep.Update(pendencia);
                        pendencyRep.SubmitChanges();

                        // remove as pendencias de promocao remanescentes atreladas ao funcionário
                        var pendencias = pendencyRep.Find(p => p.evento == evento.id && p.usuario == usuario.id && p.id != pendencia.id).ToList();
                        foreach (var p in pendencias)
                        {
                            pendencyRep.Delete(p);
                            pendencyRep.SubmitChanges();
                        }
                    }
                                      
                    return false;

                }
                else
                {
                   
                    // troca o cargo do usuário
                    usuario.cargo = promocao.Cargo;

                    // atualiza as informações
                    repUsuario.Update(usuario);
                    repUsuario.SubmitChanges();

                    // encerra o evento de promocao
                    evento.processado = true;

                    // informa o resultado do evento (aceito)
                    evento.estado = true;

                    repEvento.Update(evento);
                    repEvento.SubmitChanges();

                    // remove as pendencias associadas ao evento do rh
                    PendenciaServices.RemoveHrPendencyFor(evento);

                    // encerra a pendencia associada ao funcionário naquele evento (informa que nao foi aceito) e
                    // remove as outras pendencias de promocao desse usuario
                    using (var pendencyRep = DataAccess.Instance.GetPendenciaRepository())
                    {

                        // Encerra a pendencia associada ao evento
                        var pendencia = pendencyRep.Find(p => p.evento == evento.id).SingleOrDefault();
                        pendencia.estado = true;
                        pendencyRep.Update(pendencia);
                        pendencyRep.SubmitChanges();

                        // remove as pendencias de promocao remanescentes atreladas ao funcionário
                        var pendencias = pendencyRep.Find(p => p.evento == evento.id && p.usuario == usuario.id && p.id != pendencia.id).ToList();
                        foreach (var p in pendencias)
                        {
                            pendencyRep.Delete(p);
                            pendencyRep.SubmitChanges();
                        }
                    }

                    return true;
                }
                                
            }
        }
    }
}
