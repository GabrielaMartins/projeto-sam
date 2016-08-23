using AutoMapper;
using Opus.DataBaseEnvironment;
using SamApiModels.Cargo;
using SamDataBase.Model;
using System.Collections.Generic;
using System;
using System.Linq;

namespace SamServices.Services
{
    public static class CargoServices
    {

        public static List<CargoViewModel> RecuperaProximoCargo(int cargo)
        {
            using (var cargoRep = DataAccess.Instance.GetCargoRepository())
            {
                var cargos = cargoRep.RecuperaProximoCargo(cargo);
                var cargosViewModel = Mapper.Map<List<Cargo>, List<CargoViewModel>>(cargos);
                return cargosViewModel;
            }
        }

        public static List<CargoViewModel> RecuperaTodos()
        {
            using (var rep = DataAccess.Instance.GetCargoRepository())
            {
                var cargos = Mapper.Map<List<Cargo>, List<CargoViewModel>>(rep.GetAll().ToList());
                return cargos;
            }
       
        }
    }
}
