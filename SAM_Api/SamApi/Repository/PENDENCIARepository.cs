using SamApi.App_Data;
using Opus.RepositoryPattern;
using System.Data.Entity;

namespace Opus.DataBaseEnvironment
{
	public class PENDENCIARepository : Repository<PENDENCIA>
	{
		public PENDENCIARepository(DbContext context) : base (context)
		{
		}
		//Preencher aqui
	}
}