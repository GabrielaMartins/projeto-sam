using SamDataBase.Model;
using Opus.RepositoryPattern;
using System.Data.Entity;
using SamApiModels;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace Opus.DataBaseEnvironment
{
    public class PromocaoRepository : Repository<Promocao> {

        public PromocaoRepository(DbContext context) : base (context)
        {
        }

        //Preencher aqui
        public List<PromocaoViewModel> RecuperaProximasPromocoes()
        {
            var promocoesViewModel = new List<PromocaoViewModel>();

            var db = DbContext as SamEntities;
            promocoesViewModel = 
            (from c in db.Cargos
            from u in db.Usuarios
            where 
            u.cargo != c.id &&
            (c.pontuacao - u.pontos) >= 0 &&
            (c.pontuacao - u.pontos) <= (c.pontuacao * 0.2)
            select new
            {
                usuario = u,
                cargo = c
            }).AsEnumerable()
            .Select( x => {
                var usuarioViewModel = Mapper.Map<Usuario, UsuarioViewModel>(x.usuario);
                return new PromocaoViewModel()
                {
                    Usuario = usuarioViewModel,
                    PontosFaltantes = (usuarioViewModel.ProximoCargo[0].pontuacao - x.usuario.pontos)
                };
            }).ToList();

            return promocoesViewModel;
        }
    }
}
