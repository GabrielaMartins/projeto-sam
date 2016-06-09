using SamDataBase.Model;
using Opus.RepositoryPattern;
using System.Data.Entity;

namespace Opus.DataBaseEnvironment
{
	public class ItenRepository : Repository<Iten>
	{
		public ItenRepository(DbContext context) : base (context)
		{
		}
		//Preencher aqui
	}
}