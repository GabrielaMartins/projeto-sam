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
        public List<Evento> RecuperaEventos(int? quantidade = null)
        {
            if (quantidade.HasValue)
            {
                var eventos = GetAll().OrderBy(e => e.data).Take(quantidade.Value).ToList();
                return eventos;
            }
            else
            {
               var eventos = GetAll().ToList();
                return eventos;
            }

        }
        
        public List<Evento> RecuperaCertificacoesMaisProcuradas()
        {

            //select ev.item, count(ev.item) as qtd from Eventos ev group by ev.item order by qtd desc

            //var eventos = DataAccess.Instance.EventoRepository().GetAll();
            //var query = from evento in eventos
            //            group evento
            //            by evento.item into t
            //            select t;


            //var certificacoesViewModel = new List<EventoViewModel>();
            //var certificacoes = query.ToList();
            //foreach(var certificacao in certificacoes)
            //{
            //    var certificacaoViewModel = Mapper.Map<Evento, EventoViewModel>(certificacao);
            //    certificacoesViewModel.Add(certificacaoViewModel);
            //}

            //return certificacoesViewModel;

            throw new NotImplementedException();
        }

    }
}