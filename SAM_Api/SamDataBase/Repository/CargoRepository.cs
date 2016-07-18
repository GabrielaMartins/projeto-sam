using SamDataBase.Model;
using Opus.RepositoryPattern;
using System.Data.Entity;

namespace Opus.DataBaseEnvironment
{
	public class CargoRepository : Repository<Cargo>
	{
		public CargoRepository(DbContext context) : base (context)
		{
		}
		//Preencher aqui
	}
}