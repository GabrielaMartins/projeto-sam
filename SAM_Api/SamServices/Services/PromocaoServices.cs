using SamApiModels.Promocao;
using SamDataBase.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamServices.Services
{
    public static class PromocaoServices
    {

        public static List<ProximaPromocaoViewModel> RecuperaProximasPromocoes()
        {
            var promocoesViewModel = new List<ProximaPromocaoViewModel>();
            var cargos = CargoServices.RecuperaTodos();
            var usuarios = UsuarioServices.RecuperaTodos();

            var db = new SamEntities();
            promocoesViewModel =
            (from c in cargos
             from u in usuarios
             where
             u.Cargo.id != c.id &&
             (c.pontuacao - u.pontos) >= 0 &&
             (c.pontuacao - u.pontos) <= (c.pontuacao * 0.2)
             select new ProximaPromocaoViewModel()
             {
                 Usuario = u,
                 PontosFaltantes = (u.ProximoCargo.ElementAt(0).pontuacao - u.pontos)

             })
             .OrderBy(p => p.PontosFaltantes)
             .ToList();

            return promocoesViewModel;
        }
    }
}
