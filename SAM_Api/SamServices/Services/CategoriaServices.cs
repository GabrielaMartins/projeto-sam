using AutoMapper;
using Opus.DataBaseEnvironment;
using SamApiModels.Categoria;
using SamDataBase.Model;
using System.Collections.Generic;
using System.Linq;
using System;

namespace SamServices.Services
{
    public static class CategoriaServices
    {

        public static List<CategoriaViewModel> RecuperaTodas()
        {
            using(var rep = DataAccess.Instance.GetCategoriaRepository())
            {
                var categorias = Mapper.Map<List<Categoria>, List<CategoriaViewModel>>(rep.GetAll().ToList());

                return categorias;
            }
        }

        public static CategoriaViewModel Recupera(int id)
        {
            using (var rep = DataAccess.Instance.GetCategoriaRepository())
            {
                var categoria = rep.Find(c => c.id == id).SingleOrDefault();
                var categoriaViewModel = Mapper.Map<Categoria, CategoriaViewModel>(categoria);
                return categoriaViewModel;
            }
        }
    }
}
