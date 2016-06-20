using SamDataBase.Model;
using Opus.RepositoryPattern;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using SamApiModels;
<<<<<<< HEAD
=======
using AutoMapper;
using System;
>>>>>>> 22d34add76fe9a0ac00af9ecbd1aaf58daa53df0

namespace Opus.DataBaseEnvironment
{
	public class UsuarioRepository : Repository<Usuario>
	{
        private SamEntities db;
		public UsuarioRepository(DbContext context) : base (context)
		{
            db = new SamEntities();
        }
        
        public List<PromocoesViewModel> proximasPromocoes()
        {
            
            var dados = (from usuarios in db.Usuarios
                        from cargos in db.Cargos
                        where usuarios.cargo == cargos.CargoAnterior.id && (cargos.pontuacao - usuarios.pontos > 0 && cargos.pontuacao - usuarios.pontos < 100) && usuarios.cargo != null
                        select new PromocoesViewModel {
                            id = usuarios.id,
                            nome = usuarios.nome,
                            imagem = usuarios.foto,
                            cargoAtual = usuarios.Cargo.nome,
                            proximoCargo = cargos.nome,
                            pontosFaltantes = cargos.pontuacao - usuarios.pontos
                        }).ToList<PromocoesViewModel>();

            return dados;
        }

        public List<CargoViewModel> RecuperaProximoCargo(string samaccount)
        {

            var query = (from cargo in db.Cargos
                         from usuario in db.Usuarios
                         where cargo.anterior == usuario.cargo && usuario.samaccount == samaccount
                         select cargo);

            var cargos = query.ToList();
            var cargosViewModel = new List<CargoViewModel>();
            foreach(var cargo in cargos)
            {
                var cargoViewModel = Mapper.Map<Cargo, CargoViewModel>(cargo);
                cargosViewModel.Add(cargoViewModel);
            }

            return cargosViewModel;
        }

        public PerfilViewModel RecuperaPerfil(string samaccount)
        {

            try
            {
                var usuario = DataAccess.Instance.UsuarioRepository().Find(u => u.samaccount == samaccount).SingleOrDefault();
                var eventos = DataAccess.Instance.EventoRepository().Find(e => e.usuario == usuario.id).ToList();

                var usuarioViewModel = Mapper.Map<Usuario, UsuarioViewModel>(usuario);
                var eventoViewModels = new List<EventoViewModel>();
                foreach (var evento in eventos)
                {
                    var eventoViewModel = Mapper.Map<Evento, EventoViewModel>(evento);
                    eventoViewModels.Add(eventoViewModel);
                }

                return new PerfilViewModel() { Usuario = usuarioViewModel, Eventos = eventoViewModels };

            }catch(NullReferenceException)
            {
                return null;
            }
        }

    }
}