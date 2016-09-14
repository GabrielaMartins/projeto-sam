using SamDataBase.Model;
using Opus.RepositoryPattern;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Opus.DataBaseEnvironment
{
	public class EventoRepository : Repository<Evento>
	{      
        public EventoRepository(DbContext context) : base (context)
		{


		}

        //Preencher aqui
        public List<Evento> RecuperaEventos(string tipo = null, int ? quantidade = null)
        {
            if (quantidade.HasValue)
            {
                if(tipo != null)
                    return Find(e => e.tipo == tipo).OrderByDescending(e => e.data).Take(quantidade.Value).ToList();

                return GetAll().OrderByDescending(e => e.data).Take(quantidade.Value).ToList();
            }
            else
            {
                if(tipo != null)
                    return Find(e => e.tipo == tipo).OrderByDescending(e => e.data).ToList();

                return GetAll().OrderByDescending(e => e.data).ToList();
            }

        }
    }
}