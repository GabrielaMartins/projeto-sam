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
	public class ItemRepository : Repository<Item>
	{
		public ItemRepository(DbContext context) : base (context)
		{
		}

        //Preencher aqui
        public List<UsuarioViewModel> RecuperaUsuariosQueFizeram(int item)
        {
            var repositorioEvento = DataAccess.Instance.GetEventoRepository();

            var usuarios = repositorioEvento.GetAll().Where(x => x.item == item).Where(y => y.usuario == y.Usuario.id).Select(u => u.Usuario).ToList();
            var usuariosViewModel = new List<UsuarioViewModel>();
            foreach(var usuario in usuarios)
            {
                var usuarioViewModel = Mapper.Map<Usuario, UsuarioViewModel>(usuario);
                usuariosViewModel.Add(usuarioViewModel);
            }

            return usuariosViewModel;
        }
    }
}