using SamApi.App_Data;
using Opus.RepositoryPattern;
using System.Data.Entity;

namespace Opus.DataBaseEnvironment
{
	public class CATEGORIARepository : Repository<CATEGORIA>
	{
		public CATEGORIARepository(DbContext context) : base (context)
		{
		}
		//Preencher aqui
	}
}