using SamApi.App_Data;
using Opus.RepositoryPattern;
using System.Data.Entity;

namespace Opus.DataBaseEnvironment
{
	public class EVENTORepository : Repository<EVENTO>
	{
		public EVENTORepository(DbContext context) : base (context)
		{
		}
		//Preencher aqui
	}
}