using AutoMapper;
using SamApiModels;
using SamApiModels.Cargo;
using SamApiModels.Categoria;
using SamApiModels.Evento;
using SamApiModels.Item;
using SamApiModels.Pendencia;
using SamApiModels.Promocao;
using SamApiModels.User;
using SamApiModels.Votacao;
using SamDataBase.Model;
using Opus.DataBaseEnvironment;
using System.Collections.Generic;

namespace SamServices.Mappers
{
    public class ViewModelToViewModelMappingProfile : Profile
    {
        protected override void Configure()
        {

            Mapper.CreateMap<EventoViewModel, PendenciaEventoViewModel>()
           .ForMember(e => e.id, opt => opt.MapFrom(src => src.id))
           .ForMember(e => e.data, opt => opt.MapFrom(src => src.data))
           .ForMember(e => e.estado, opt => opt.MapFrom(src => src.estado))
           .ForMember(e => e.tipo, opt => opt.MapFrom(src => src.tipo))
           .ForMember(e => e.Item, opt => opt.MapFrom(src => src.Item))
           .ForMember(e => e.Usuario, opt => opt.MapFrom(src => src.Usuario));

            Mapper.CreateMap<EventoItemViewModel, ItemViewModel>();

        }
    }
}