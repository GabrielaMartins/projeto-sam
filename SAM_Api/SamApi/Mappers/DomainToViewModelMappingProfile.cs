using AutoMapper;
using Opus.DataBaseEnvironment;
using SamApi.Helpers;
using SamApiModels;
using SamApiModels.Event;
using SamApiModels.Item;
using SamApiModels.User;
using SamDataBase.Model;
using System.Collections.Generic;
using System.Linq;

namespace SamApi.Mappers
{
    public class DomainToViewModelMappingProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Cargo, CargoViewModel>();

            Mapper.CreateMap<Categoria, CategoriaViewModel>();

            Mapper.CreateMap<Item, ItemViewModel>()
            .ForMember(i => i.Categoria, opt => opt.MapFrom(src => src.Categoria));
            
            Mapper.CreateMap<Item, ItemEventoViewModel>()
            .ForMember(i => i.Categoria, opt => opt.MapFrom(src => src.Categoria));
           
            Mapper.CreateMap<ResultadoVotacao, ResultadoVotacaoViewModel>()
            .ForMember(v => v.Evento, opt => opt.MapFrom(src => src.Evento))
            .ForMember(v => v.Usuario, opt => opt.MapFrom(src => src.Usuario));

            Mapper.CreateMap<Pendencia, EventoPendenciaViewModel>()
            .ForMember(p => p.Usuario, opt => opt.MapFrom(src => src.Usuario));
            
            Mapper.CreateMap<Evento, EventoViewModel>()
            .ForMember(e => e.Pendencias, opt => opt.MapFrom(src => src.Pendencias))
            .ForMember(e => e.Item, opt => opt.MapFrom(src => src.Item))
            .ForMember(e => e.Usuario, opt => opt.MapFrom(src => src.Usuario));

            Mapper.CreateMap<Promocao, ProximaPromocaoViewModel>()
            .ForMember( u => u.Usuario, opt => opt.MapFrom(src => src.Usuario));
            
            Mapper.CreateMap<Usuario, UsuarioViewModel>()
            .ForMember( u => u.Cargo, opt => opt.MapFrom(src => src.Cargo))
            .ForMember(u => u.foto, opt => opt.MapFrom(src => ImageHelper.GetLogicPathForImage(src.samaccount)))
            .ForMember(u => u.ProximoCargo, opt => opt.MapFrom(src => DataAccess.Instance.GetCargoRepository().RecuperaProximoCargo(src.cargo)));

           
        }
    }
}