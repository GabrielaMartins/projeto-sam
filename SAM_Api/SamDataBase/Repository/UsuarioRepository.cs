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
        public List<EventoViewModel> RecuperaEventos(Usuario usuario, int? quantidade = null, string tipo = null)
        {

            if (quantidade.HasValue)
            {
                if (tipo != null)
                {
                    var eventos = DataAccess.Instance.EventoRepository().Find(e => e.usuario == usuario.id && e.tipo == tipo).Take(quantidade.Value).ToList();
                    var eventoViewModels = Mapper.Map<List<Evento>, List<EventoViewModel>>(eventos);
                    return eventoViewModels;
                }
                else
                {
                    var eventos = DataAccess.Instance.EventoRepository().Find(e => e.usuario == usuario.id).Take(quantidade.Value).ToList();
                    var eventoViewModels = Mapper.Map<List<Evento>, List<EventoViewModel>>(eventos);
                    return eventoViewModels;
                }
            }
            else
            {
                if (tipo != null)
                {
                    var eventos = DataAccess.Instance.EventoRepository().Find(e => e.usuario == usuario.id && e.tipo == tipo).ToList();
                    var eventoViewModels = Mapper.Map<List<Evento>, List<EventoViewModel>>(eventos);
                    return eventoViewModels;
                }
                else
                {
                    var eventos = DataAccess.Instance.EventoRepository().Find(e => e.usuario == usuario.id).ToList();
                    var eventoViewModels = Mapper.Map<List<Evento>, List<EventoViewModel>>(eventos);
                    return eventoViewModels;
                }
            }

        }

        public List<PromocaoViewModel> RecuperaProximasPromocoes(Usuario usuario)
        {
            var promocoesViewModel = new List<PromocaoViewModel>();
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
                 usuario = u,
                 cargo = c,
                 PontosFaltantes = c.pontuacao - u.pontos
             }).AsEnumerable()
            .Select(x => new PromocaoViewModel()
            {
                Usuario = Mapper.Map<Usuario, UsuarioViewModel>(x.usuario),
                PontosFaltantes = x.PontosFaltantes
            }
            ).ToList();

            return promocoesViewModel;
        }

        public List<PromocaoAdquiridaViewModel> RecuperaPromocoes(Usuario usuario)
        {
            var db = DbContext as SamEntities;
            var q = (from p in db.Promocoes
                     where p.usuario == usuario.id
                     select new
                     {
                         Usuario = p.Usuario,
                         CargoAnterior = p.CargoAnterior,
                         Cargo = p.Cargo,
                         Data = p.data
                     }).AsEnumerable()
                     .Select(x => new PromocaoAdquiridaViewModel()
                     {
                         Cargo = Mapper.Map<Cargo, CargoViewModel>(x.Cargo),
                         CargoAnterior = Mapper.Map<Cargo, CargoViewModel>(x.CargoAnterior),
                         Usuario = Mapper.Map<Usuario, UsuarioViewModel>(x.Usuario),
                         Data = x.Data
                     });

            var promocoesRealizadas = q.ToList();

            return promocoesRealizadas;
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
    }
}