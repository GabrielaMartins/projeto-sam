using SamDataBase.Model;
using Opus.RepositoryPattern;
using System.Data.Entity;

namespace Opus.DataBaseEnvironment
{
	public class ResultadoVotacoesRepository : Repository<ResultadoVotacoes>
	{
		public ResultadoVotacoesRepository(DbContext context) : base (context)
		{
		}
		//Preencher aqui
	}
}