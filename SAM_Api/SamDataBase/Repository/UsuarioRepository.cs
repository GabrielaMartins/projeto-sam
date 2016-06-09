using SamDataBase.Model;
using Opus.RepositoryPattern;
using System.Data.Entity;

namespace Opus.DataBaseEnvironment
{
	public class UsuarioRepository : Repository<Usuario>
	{
		public UsuarioRepository(DbContext context) : base (context)
		{
		}
		//Preencher aqui
	}
}