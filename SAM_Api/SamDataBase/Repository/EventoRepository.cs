using SamDataBase.Model;
using Opus.RepositoryPattern;
using System.Data.Entity;

namespace Opus.DataBaseEnvironment
{
	public class EventoRepository : Repository<Evento>
	{      
        public EventoRepository(DbContext context) : base (context)
		{


		}
	}
}