using SamDataBase.Model;
using Opus.RepositoryPattern;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using Opus.DataBaseEnvironment;

namespace Opus.DataBaseEnvironment
{
	public class ResultadoVotacoesRepository : Repository<ResultadoVotacao>
	{
		public ResultadoVotacoesRepository(DbContext context) : base (context)
		{
		}

		//Preencher aqui
        public List<ResultadoVotacao> RecuperaResultados(int? qtd = null)
        {
            var db = DbContext as SamEntities;
            if (qtd.HasValue)
            {
                return db.ResultadoVotacaos.OrderBy(v => v.Evento.data).Take(qtd.Value).ToList();
            }
            else
            {
                return db.ResultadoVotacaos.OrderBy(v => v.Evento.data).ToList();
            }
        }
	}
}