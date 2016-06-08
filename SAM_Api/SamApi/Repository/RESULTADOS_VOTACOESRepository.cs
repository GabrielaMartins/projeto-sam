using SamApi.App_Data;
using Opus.RepositoryPattern;
using System.Data.Entity;

namespace Opus.DataBaseEnvironment
{
	public class RESULTADOS_VOTACOESRepository : Repository<RESULTADOS_VOTACOES>
	{
		public RESULTADOS_VOTACOESRepository(DbContext context) : base (context)
		{
		}
		//Preencher aqui
	}
}