using SamDataBase.Model;
using Opus.RepositoryPattern;
using System.Data.Entity;
using SamApiModels;
using System.Linq;
using AutoMapper;
using System.Collections.Generic;

namespace Opus.DataBaseEnvironment
{
	public class CargoRepository : Repository<Cargo>
	{
		public CargoRepository(DbContext context) : base (context)
		{
		}

        //Preencher aqui
        public List<CargoViewModel> RecuperaProximoCargo(int? cargo)
        {
            var db = (SamEntities)DbContext;
            var query = (from c in db.Cargos
                         where c.anterior == cargo.Value
                         select c);

            var cargos = query.ToList();
            var cargosViewModel = Mapper.Map<List<Cargo>, List<CargoViewModel>>(cargos);
            
            return cargosViewModel;
        }
    }
}