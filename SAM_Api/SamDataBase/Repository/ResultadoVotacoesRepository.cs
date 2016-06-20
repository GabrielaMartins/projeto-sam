using SamDataBase.Model;
using Opus.RepositoryPattern;
using System.Data.Entity;
using Opus.DataBaseEnvironment;

namespace Opus.DataBaseEnvironment
{
	public class ResultadoVotacoesRepository : Repository<ResultadoVotacoesRepository>
	{
		public ResultadoVotacoesRepository(DbContext context) : base (context)
		{
		}
		//Preencher aqui
	}
}