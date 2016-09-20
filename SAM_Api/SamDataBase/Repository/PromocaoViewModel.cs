using SamDataBase.Model;
using Opus.RepositoryPattern;
using System.Data.Entity;
using System;
using System.Linq;

namespace Opus.DataBaseEnvironment
{
    public class PromocaoRepository : Repository<Promocao>
    {

        public PromocaoRepository(DbContext context) : base(context)
        {
        }

        public DateTime RecuperaDataUltimaPromocao(int id)
        {
            var dataUltimaPromocao = Find(p => p.usuario == id).OrderByDescending(p => p.data).Select(p => p.data).FirstOrDefault();

            return dataUltimaPromocao;
        }

        //Preencher aqui
    }
}