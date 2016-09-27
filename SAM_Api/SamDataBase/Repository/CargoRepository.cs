using SamDataBase.Model;
using Opus.RepositoryPattern;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using System;

namespace Opus.DataBaseEnvironment
{
	public class CargoRepository : Repository<Cargo>
	{
		public CargoRepository(DbContext context) : base (context)
		{
		}

        //Preencher aqui
        public List<Cargo> RecuperaProximoCargo(int? cargo)
        {
            var db = (SamEntities)DbContext;
            var query = (from c in db.Cargos
                         where c.anterior == cargo.Value
                         select c);

            var cargos = query.ToList();
                        
            return cargos;
        }

        public List<Cargo> GetAll(string samaccount)
        {
            var db = (SamEntities)DbContext;
            var q = (from c in db.Cargos
                     from u in db.Usuarios
                     where
                     u.samaccount == samaccount &&
                     c.anterior >= u.cargo
                     select c);

            return q.ToList();
        }
    }
}