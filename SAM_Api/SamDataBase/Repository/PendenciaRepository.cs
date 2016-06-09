using SamDataBase.Model;
using Opus.RepositoryPattern;
using System.Data.Entity;

namespace Opus.DataBaseEnvironment
{
	public class PendenciaRepository : Repository<Pendencia>
	{
		public PendenciaRepository(DbContext context) : base (context)
		{
		}
		//Preencher aqui
	}
}