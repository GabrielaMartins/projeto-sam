using AutoMapper;
using Opus.DataBaseEnvironment;
using SamApiModels.Pendencia;
using SamDataBase.Model;
using System.Collections.Generic;
using System.Linq;
using System;

namespace SamServices.Services
{
    public static class PendencyServices
    {
        public static PendenciaEventoViewModel Recupera(int id)
        {
            using (var rep = DataAccess.Instance.GetPendenciaRepository())
            {
                var pendencia = rep.Find(p => p.id == id).SingleOrDefault();

                var pendenciaViewModel = Mapper.Map<Pendencia, PendenciaEventoViewModel>(pendencia);

                return pendenciaViewModel;
            }
        }

        public static List<PendenciaEventoViewModel> RecuperaTodas()
        {
            using (var rep = DataAccess.Instance.GetPendenciaRepository())
            {
                var pendencias = rep.GetAll().ToList();

                var pendenciasViewModel = Mapper.Map<List<Pendencia>, List<PendenciaEventoViewModel>>(pendencias);

                return pendenciasViewModel;
            }
        }

        public static void Delete(int id)
        {
            using (var rep = DataAccess.Instance.GetPendenciaRepository())
            {

                rep.Delete(x => x.id == id);
                rep.SubmitChanges();
            }
        }
    }
}
