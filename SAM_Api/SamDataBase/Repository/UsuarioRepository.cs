using SamDataBase.Model;
using Opus.RepositoryPattern;
using System.Data.Entity;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Opus.DataBaseEnvironment
{
	public class UsuarioRepository : Repository<Usuario>
	{
        SamEntities db;
		public UsuarioRepository(DbContext context) : base (context)
		{
            db = new SamEntities();
        }
        
        public List<dynamic> proximasPromocoes()
        {
            var dados = (from usuarios in db.Usuarios
                        from cargos in db.Cargos
                        where usuarios.cargo == cargos.CargoAnterior.id && (cargos.pontuacao - usuarios.pontos > 0 && cargos.pontuacao - usuarios.pontos < 100) && usuarios.cargo != null
                        select new {usuarios.id, usuarios.nome, imagem = usuarios.foto, cargoAtual = usuarios.Cargo.nome, proximoCargo = cargos.nome, pontosFaltantes = cargos.pontuacao - usuarios.pontos }).ToList<dynamic>();

            return dados;
        }

    }
}