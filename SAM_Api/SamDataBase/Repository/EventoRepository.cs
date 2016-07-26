using SamDataBase.Model;
using Opus.RepositoryPattern;
using System.Data.Entity;
using SamApiModels;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using System;

namespace Opus.DataBaseEnvironment
{
	public class EventoRepository : Repository<Evento>
	{      
        public EventoRepository(DbContext context) : base (context)
		{


		}

        //Preencher aqui
        public List<EventoViewModel> RecuperaEventos(int? quantidade = null)
        {
            if (quantidade.HasValue)
            {
                var eventos = GetAll().OrderBy(e => e.data).Take(quantidade.Value).ToList();
                var eventosViewModel = Mapper.Map<List<Evento>, List<EventoViewModel>>(eventos);
                return eventosViewModel;
            }
            else
            {
                var eventos = GetAll().ToList();
                var eventosViewModel = Mapper.Map<List<Evento>, List<EventoViewModel>>(eventos);

                return eventosViewModel;
            }

        }


        public List<EventoViewModel> RecuperaCertificacoesMaisProcuradas()
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

        public List<UltimoEventoViewModel> UltimosEventos()
        {

            using (var eventosRepository = DataAccess.Instance.GetEventoRepository())
            {

                // TODO: refatorar isso depois, tentar inserir como um metodo do repositorio de eventos
                var ultimosEventos = eventosRepository.GetAll()
                    .OrderByDescending(x => x.data)
                    .ThenBy(x => x.Item.nome)
                    .Take(10).AsEnumerable()
                    .Select(x =>
                        new UltimoEventoViewModel
                        {
                            Evento = Mapper.Map<Evento, EventoViewModel>(x),
                            UsuariosQueFizeram = DataAccess.Instance.GetItemRepository().RecuperaUsuariosQueFizeram(x.item.Value)
                        }).ToList();

                return ultimosEventos;
            }
        }

    }
}