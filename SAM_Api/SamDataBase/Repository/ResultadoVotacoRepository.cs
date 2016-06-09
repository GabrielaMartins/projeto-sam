using SamDataBase.Model;
using Opus.RepositoryPattern;
using System.Data.Entity;

namespace Opus.DataBaseEnvironment
{
	public class ResultadoVotacoRepository : Repository<ResultadoVotaco>
	{
		public ResultadoVotacoRepository(DbContext context) : base (context)
		{
		}
		//Preencher aqui
	}
}