using SamApi.App_Data;
using Opus.RepositoryPattern;
using System.Data.Entity;

namespace Opus.DataBaseEnvironment
{
	public class TAGRepository : Repository<TAG>
	{
		public TAGRepository(DbContext context) : base (context)
		{
		}
		//Preencher aqui
	}
}