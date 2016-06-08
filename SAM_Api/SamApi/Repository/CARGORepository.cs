using SamApi.App_Data;
using Opus.RepositoryPattern;
using System.Data.Entity;

namespace Opus.DataBaseEnvironment
{
	public class CARGORepository : Repository<CARGO>
	{
		public CARGORepository(DbContext context) : base (context)
		{
		}
		//Preencher aqui
	}
}