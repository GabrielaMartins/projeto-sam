using SamDataBase.Model;
using Opus.RepositoryPattern;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using SamApiModels;
using AutoMapper;
using System;
using SamApiModels.User;
using SamApiModels.Item;
using SamApiModels.Categoria;

namespace Opus.DataBaseEnvironment
{
	public class ItemRepository : Repository<Item>
	{
		public ItemRepository(DbContext context) : base (context)
		{
		}

        //Preencher aqui
        public List<UsuarioViewModel> RecuperaUsuariosQueFizeram(int item)
        {
            var repositorioEvento = DataAccess.Instance.GetEventoRepository();

            var db = (SamEntities)DbContext;
            var query = (from e in db.Eventos
                     from u in db.Usuarios
                     where
                     e.item == item &&
                     e.usuario == u.id
                     select u);

            var usuarios = query.Distinct().ToList();
            return Mapper.Map<List<Usuario>, List<UsuarioViewModel>>(usuarios);

        }

        public List<ItemViewModel> RecuperaItensESeusUsuarios()
        {
            var r = new List<ItemViewModel>();
            var itensViewModel = Mapper.Map<List<Item>,List<ItemViewModel>>(GetAll().ToList());
            foreach(var item in itensViewModel)
            {
                item.Usuarios = RecuperaUsuariosQueFizeram(item.id);
                r.Add(item);
            }

            return r;
        }
    }
}