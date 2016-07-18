using SamDataBase.Model;
using Opus.RepositoryPattern;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using SamApiModels;
using AutoMapper;
using System;

namespace Opus.DataBaseEnvironment
{
    public class UsuarioRepository : Repository<Usuario>
    {

        public UsuarioRepository(DbContext context) : base(context)
        {

        }

        //Preencher aqui
        public List<EventoViewModel> RecuperaAtividades(Usuario usuario, int? quantidade = null)
        {

            if (quantidade.HasValue)
            {
                var eventos = DataAccess.Instance.EventoRepository().Find(e => e.usuario == usuario.id && e.tipo != "promocao").Take(quantidade.Value).ToList();
                var eventosViewModel = Mapper.Map<List<Evento>, List<EventoViewModel>>(eventos);
                return eventosViewModel;
            }
            else
            {
                var eventos = DataAccess.Instance.EventoRepository().Find(e => e.usuario == usuario.id && e.tipo != "promocao").ToList();
                var eventosViewModel = Mapper.Map<List<Evento>, List<EventoViewModel>>(eventos);

                return eventosViewModel;
            }

        }

        public List<PromocaoViewModel> RecuperaProximasPromocoes(Usuario usuario)
        {
            var promocoesViewModel = new List<PromocaoViewModel>();

            var db = DbContext as SamEntities;
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
                 usuario = u,
                 cargo = c
             }).AsEnumerable()
            .Select(x => {
                var usuarioViewModel = Mapper.Map<Usuario, UsuarioViewModel>(x.usuario);
                return new PromocaoViewModel()
                {
                    Usuario = usuarioViewModel,
                    PontosFaltantes = (usuarioViewModel.ProximoCargo[0].pontuacao - x.usuario.pontos)
                };
            }).ToList();

            return promocoesViewModel;

        }

        public List<PromocaoRealizadaViewModel> RecuperaPromocoes(Usuario usuario)
        {

            var promocoesRealizadas = DataAccess.Instance.EventoRepository()
                .Find(e => e.usuario == usuario.id && e.tipo == "promocao")
                .OrderByDescending(e => e.data)
                .AsEnumerable()
                .Select(x => new PromocaoRealizadaViewModel()
                {
                    Evento = Mapper.Map<Evento, EventoViewModel>(x),
                    CargoAdquirido = null
                }).ToArray();

            // calcula os cargos
            var cargo = usuario.Cargo.CargoAnterior;
            for (var i = 0; i < promocoesRealizadas.Count(); i++)
            {
                promocoesRealizadas[i].CargoAdquirido = Mapper.Map<Cargo, CargoViewModel>(cargo);
                cargo = cargo.CargoAnterior;
            }

            return promocoesRealizadas.ToList();
        }

        public UsuarioViewModel RecuperaUsuario(string samaccount)
        {
            var usuario = Find(u => u.samaccount == samaccount).SingleOrDefault();
            if (usuario == null)
                return null;

            var usuarioViewModel = Mapper.Map<Usuario, UsuarioViewModel>(usuario);

            return usuarioViewModel;
        }

        public List<CargoViewModel> RecuperaProximoCargo(Usuario usuario)
        {
            var db = (SamEntities)DbContext;
            var query = (from cargo in db.Cargos
                         from usr in db.Usuarios
                         where cargo.anterior == usr.cargo && usr.samaccount == usuario.samaccount
                         select cargo);

            var cargos = query.ToList();
            var cargosViewModel = new List<CargoViewModel>();
            foreach (var cargo in cargos)
            {
                var cargoViewModel = Mapper.Map<Cargo, CargoViewModel>(cargo);
                cargosViewModel.Add(cargoViewModel);
            }

            return cargosViewModel;
        }

        public List<PendenciaViewModel> RecuperaPendencias(Usuario usuario)
        {
            // recupera toda pendencia onde o usuario é RH
            if (usuario.perfil == "RH")
            {
                var pendenciasRH = DataAccess.Instance.PendenciaRepository().Find(p => p.Usuario.perfil == usuario.perfil).ToList();
                var pendenciaViewModel = Mapper.Map<List<Pendencia>, List<PendenciaViewModel>>(pendenciasRH);

                return pendenciaViewModel;
            }

            // recupera as pendencias do usuario especifico
            else
            {
                var pendencias = DataAccess.Instance.PendenciaRepository().Find(p => p.usuario == usuario.id).ToList();
                var pendenciaViewModel = Mapper.Map<List<Pendencia>, List<PendenciaViewModel>>(pendencias);

                return pendenciaViewModel;
            }

        }

        public List<PontosNoAnoViewModel> PontosAdquiridosPorAno(Usuario usuario)
        {
            var db = (SamEntities)DbContext;
            var query = from u in db.Usuarios
                        from e in db.Eventos
                        from i in db.Items
                        from c in db.Categorias
                        where u.id == usuario.id &&
                              e.data.Year <= DateTime.Now.Year &&
                              e.tipo == "atribuicao" &&
                              e.estado == true &&
                              e.usuario == u.id &&
                              e.item == i.id &&
                              i.categoria == c.id
                        select new PontosNoAnoViewModel()
                        {
                            Ano = e.data.Year,
                            Pontos = (i.dificuldade * i.modificador * c.peso)
                        }; ;

            var pontosNoAnoViewModel = query.ToList();
            return pontosNoAnoViewModel;

        }
    }
}