using SamApi.App_Data;
using Opus.RepositoryPattern;
using System.Data.Entity;

namespace Opus.DataBaseEnvironment
{
	public class ITENRepository : Repository<ITEN>
	{
		public ITENRepository(DbContext context) : base (context)
		{
		}
		//Preencher aqui
	}
}