using SamDataBase.Model;
using Opus.RepositoryPattern;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using SamApiModels;
using AutoMapper;
using SamApiModels.Evento;
using SamApiModels.User;
using SamApiModels.Pendencia;
using SamApiModels.Cargo;
using SamApiModels.Promocao;

namespace Opus.DataBaseEnvironment
{
    public class UsuarioRepository : Repository<Usuario>
    {

        public UsuarioRepository(DbContext context) : base(context)
        {

        }

        //Preencher aqui
        public List<EventoViewModel> RecuperaEventos(Usuario usuario, int? quantidade = null, string tipo = null)
        {

            if (quantidade.HasValue)
            {
                if (tipo != null)
                {
                    var eventos = DataAccess.Instance.GetEventoRepository().Find(e => e.usuario == usuario.id && e.tipo == tipo).Take(quantidade.Value).ToList();
                    var eventoViewModels = Mapper.Map<List<Evento>, List<EventoViewModel>>(eventos);
                    var r = new List<EventoViewModel>();
                    foreach (var evento in eventoViewModels)
                    {
                        using (var itemRep = DataAccess.Instance.GetItemRepository())
                        {
                            evento.Item.Usuarios = itemRep.RecuperaUsuariosQueFizeram(evento.Item.id);
                        }

                        r.Add(evento);  
                    }
                    return r;
                }
                else
                {
                    var eventos = DataAccess.Instance.GetEventoRepository().Find(e => e.usuario == usuario.id).Take(quantidade.Value).ToList();
                    var eventoViewModels = Mapper.Map<List<Evento>, List<EventoViewModel>>(eventos);
                    var r = new List<EventoViewModel>();
                    foreach (var evento in eventoViewModels)
                    {
                        using (var itemRep = DataAccess.Instance.GetItemRepository())
                        {
                            evento.Item.Usuarios = itemRep.RecuperaUsuariosQueFizeram(evento.Item.id);
                        }

                        r.Add(evento);
                    }
                    return r;
                }
            }
            else
            {
                if (tipo != null)
                {
                    var eventos = DataAccess.Instance.GetEventoRepository().Find(e => e.usuario == usuario.id && e.tipo == tipo).ToList();
                    var eventoViewModels = Mapper.Map<List<Evento>, List<EventoViewModel>>(eventos);
                    var r = new List<EventoViewModel>();
                    foreach (var evento in eventoViewModels)
                    {
                        using (var itemRep = DataAccess.Instance.GetItemRepository())
                        {
                            evento.Item.Usuarios = itemRep.RecuperaUsuariosQueFizeram(evento.Item.id);
                        }

                        r.Add(evento);
                    }
                    return r;
                }
                else
                {
                    var eventos = DataAccess.Instance.GetEventoRepository().Find(e => e.usuario == usuario.id).ToList();
                    var eventoViewModels = Mapper.Map<List<Evento>, List<EventoViewModel>>(eventos);
                    var r = new List<EventoViewModel>();
                    foreach (var evento in eventoViewModels)
                    {
                        using (var itemRep = DataAccess.Instance.GetItemRepository())
                        {
                            evento.Item.Usuarios = itemRep.RecuperaUsuariosQueFizeram(evento.Item.id);
                        }

                        r.Add(evento);
                    }
                    return r;
                }
            }

        }

        public List<ProximaPromocaoViewModel> RecuperaProximasPromocoes(Usuario usuario)
        {
            var promocoesViewModel = new List<ProximaPromocaoViewModel>();
            var db = (SamEntities)DbContext;

            if (usuario == null)
                return null;

            promocoesViewModel =
            (from c in db.Cargos
             from u in db.Usuarios
             where
             u.id == usuario.id &&
             u.cargo != c.id &&
             (c.pontuacao - u.pontos) >= 0 &&
             (c.pontuacao - u.pontos) <= (c.pontuacao * 0.2)
             select new
             {
                 Usuario = u,
                 PontosFaltantes = c.pontuacao - u.pontos
            }).AsEnumerable()
            .Select(x => new ProximaPromocaoViewModel()
            {
                Usuario = Mapper.Map<Usuario, UsuarioViewModel>(x.Usuario),
                PontosFaltantes = x.PontosFaltantes
            }
            ).ToList();

            return promocoesViewModel;
        }

        public List<PromocaoAdquiridaViewModel> RecuperaPromocoesAdquiridas(Usuario usuario)
        {
            var db = DbContext as SamEntities;
            var promocoesRealizadas =
            (from p in db.Promocoes
             where p.usuario == usuario.id
             select new
             {
                 Usuario = p.Usuario,
                 CargoAnterior = p.CargoAnterior,
                 CargoAdquirido = p.Cargo,
                 Data = p.data
             }).AsEnumerable()
            .Select(x => new PromocaoAdquiridaViewModel()
            {
                CargoAdquirido = Mapper.Map<Cargo, CargoViewModel>(x.CargoAdquirido),
                CargoAnterior = Mapper.Map<Cargo, CargoViewModel>(x.CargoAnterior),
                Usuario = Mapper.Map<Usuario, UsuarioViewModel>(x.Usuario),
                Data = x.Data

            }).ToList();

            return promocoesRealizadas;
        }

        public List<PendenciaUsuarioViewModel> RecuperaPendencias(Usuario usuario)
        {

            var pendencias = DataAccess.Instance
                .GetPendenciaRepository()
                .Find(p => p.usuario == usuario.id && p.estado == true)
                .AsEnumerable()
                .Select(x => new PendenciaUsuarioViewModel()
                {
                    Usuario = Mapper.Map<Usuario, UsuarioViewModel>(x.Usuario),
                    Evento = Mapper.Map<Evento, PendenciaEventoViewModel>(x.Evento)
                }).ToList();

            return pendencias;


        }

        public List<ResultadoVotacaoViewModel> RecuperaVotacoes(Usuario usuario)
        {
            var votacoes = DataAccess.Instance
                .GetResultadoVotacoRepository()
                .Find(r => r.usuario == usuario.id || r.Evento.usuario == usuario.id)
                .ToList();


            return Mapper.Map<List<ResultadoVotacao>, List<ResultadoVotacaoViewModel>>(votacoes);
        }
    }
}