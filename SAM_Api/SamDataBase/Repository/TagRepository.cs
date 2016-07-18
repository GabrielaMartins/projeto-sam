using SamDataBase.Model;
using Opus.RepositoryPattern;
using System.Data.Entity;

namespace Opus.DataBaseEnvironment
{
	public class TagRepository : Repository<Tag>
	{
		public TagRepository(DbContext context) : base (context)
		{
		}
		//Preencher aqui
	}
}