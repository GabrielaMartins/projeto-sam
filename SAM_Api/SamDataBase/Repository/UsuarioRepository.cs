using SamDataBase.Model;
using Opus.RepositoryPattern;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Opus.DataBaseEnvironment
{
    public class UsuarioRepository : Repository<Usuario>
    {

        public UsuarioRepository(DbContext context) : base(context)
        {

        }

        //Preencher aqui
        public List<Evento> RecuperaEventos(int usuario, int? quantidade = null, string tipo = null)
        {

            if (quantidade.HasValue)
            {
                if (tipo != null)
                {
                    var eventos = DataAccess.Instance.GetEventoRepository().Find(e => e.usuario == usuario && e.tipo == tipo).Take(quantidade.Value).ToList();
                    return eventos;
                }
                else
                {
                    var eventos = DataAccess.Instance.GetEventoRepository().Find(e => e.usuario == usuario).Take(quantidade.Value).ToList();
                    return eventos;
                }
            }
            else
            {
                if (tipo != null)
                {
                    var eventos = DataAccess.Instance.GetEventoRepository().Find(e => e.usuario == usuario && e.tipo == tipo).ToList();
                    return eventos;
                }
                else
                {
                    var eventos = DataAccess.Instance.GetEventoRepository().Find(e => e.usuario == usuario).ToList();
                    return eventos;
                }
            }

        }

        public List<Promocao> RecuperaPromocoesAdquiridas(int usuario)
        {
            var db = DbContext as SamEntities;
            var promocoesRealizadas =
            (from p in db.Promocoes
             where p.usuario == usuario
             select p).ToList();

            return promocoesRealizadas;
        }

        public List<Pendencia> RecuperaPendencias(int usuario)
        {

            var pendencias = DataAccess.Instance
                .GetPendenciaRepository()
                .Find(p => p.usuario == usuario && p.estado == true)
                .ToList();

            return pendencias;


        }

        public List<ResultadoVotacao> RecuperaVotos(int usuario, int? quantity = null)
        {
            List<ResultadoVotacao> votos = null;
            if (quantity.HasValue)
            {
                votos = DataAccess.Instance
                   .GetResultadoVotacoRepository()
                   .Find(r => r.usuario == usuario || r.Evento.usuario == usuario)
                   .Take(quantity.Value)
                   .ToList();
            }
            else
            {
                votos = DataAccess.Instance
                    .GetResultadoVotacoRepository()
                    .Find(r => r.usuario == usuario || r.Evento.usuario == usuario)
                    .ToList();
            }

            return votos;
        }
    }
}