using Opus.DataBaseEnvironment;
using SamApiModels.Evento;
using SamApiModels.Promocao;
using SamDataBase.Model;
using System.Collections.Generic;
using System.Linq;


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

                    // encerra a pendencia associada ao evento do funcionário (informa que nao foi aceito)
                    PendenciaServices.CloseEmployeePendencyFor(evento, usuario.id);
                                      
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

                    // encerra a pendencia associada ao evento do funcionário
                    PendenciaServices.CloseEmployeePendencyFor(evento, usuario.id);

                    // remove as pendencias associadas ao evento do rh
                    PendenciaServices.RemoveHrPendencyFor(evento);

                    return true;
                }
                                
            }
        }
    }
}
