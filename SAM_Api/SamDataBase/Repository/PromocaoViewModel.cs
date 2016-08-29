using SamDataBase.Model;
using Opus.RepositoryPattern;
using System.Data.Entity;

namespace Opus.DataBaseEnvironment
{
    public class PromocaoRepository : Repository<Promocao>
    {

        public PromocaoRepository(DbContext context) : base(context)
        {
        }

        //Preencher aqui
    }
}