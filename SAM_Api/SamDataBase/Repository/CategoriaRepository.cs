using SamDataBase.Model;
using Opus.RepositoryPattern;
using System.Data.Entity;

namespace Opus.DataBaseEnvironment
{
	public class CategoriaRepository : Repository<Categoria>
	{
		public CategoriaRepository(DbContext context) : base (context)
		{
		}
		//Preencher aqui
	}
}