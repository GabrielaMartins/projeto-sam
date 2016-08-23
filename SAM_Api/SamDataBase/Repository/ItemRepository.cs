using SamDataBase.Model;
using Opus.RepositoryPattern;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Opus.DataBaseEnvironment
{
	public class ItemRepository : Repository<Item>
	{
		public ItemRepository(DbContext context) : base (context)
		{
		}

        //Preencher aqui
        public List<Usuario> RecuperaUsuariosQueFizeram(int item)
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

            return usuarios;

        }

    }
}