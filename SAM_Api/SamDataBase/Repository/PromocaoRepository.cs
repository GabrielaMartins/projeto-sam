using SamDataBase.Model;
using Opus.RepositoryPattern;
using System.Data.Entity;
using System;
using System.Linq;

namespace Opus.DataBaseEnvironment
{
    public class PromocaoRepository : Repository<Promocao> {

        public PromocaoRepository(DbContext context) : base (context)
        {
        }

        public DateTime RecuperaDataUltimaPromocao(int usuario)
        {
            using (var rep = DataAccess.Instance.GetPromocaoRepository())
            {
                var dataPromocao = rep.Find(p => p.usuario == usuario).OrderByDescending(p => p.data).Select(p => p.data).FirstOrDefault();

                return dataPromocao;
            }
        }

        //Preencher aqui
    }
}
