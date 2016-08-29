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
    public class DomainToViewModelMappingProfile : Profile
    {
        protected override void Configure()
        {

            Mapper.CreateMap<Evento, EventoViewModel>()
           .ForMember(e => e.Item, opt => opt.MapFrom(src => src.Item))
           .ForMember(e => e.Usuario, opt => opt.MapFrom(src => src.Usuario));

            Mapper.CreateMap<Pendencia, PendenciaUsuarioViewModel>()
            .ForMember(x => x.Usuario, opt => opt.MapFrom(src => Mapper.Map<Usuario, UsuarioViewModel>(src.Usuario)))
            .ForMember(x => x.Evento, opt => opt.MapFrom(src => Mapper.Map<Evento, PendenciaEventoViewModel>(src.Evento)));

            Mapper.CreateMap<ResultadoVotacao, VotoViewModel>()
            .ForMember(x => x.Dificuldade, opt => opt.MapFrom(src => src.dificuldade))
            .ForMember(x => x.Modificador, opt => opt.MapFrom(src => src.modificador))
            .ForMember(x => x.Usuario, opt => opt.MapFrom(src => src.Usuario))
            .ForMember(x => x.Evento, opt => opt.MapFrom(src => src.Evento));

            Mapper.CreateMap<Cargo, CargoViewModel>();

            Mapper.CreateMap<Categoria, CategoriaViewModel>();

            Mapper.CreateMap<Item, ItemViewModel>()
            .ForMember(i => i.Categoria, opt => opt.MapFrom(src => Mapper.Map<Categoria, CategoriaViewModel>(src.Categoria)))
            .ForMember(i => i.Usuarios, opt => opt.MapFrom(
                src => Mapper.Map<List<Usuario>, List<UsuarioViewModel>>
                (DataAccess.Instance.GetItemRepository().RecuperaUsuariosQueFizeram(src.id))
             ));
            
            Mapper.CreateMap<Item, EventoItemViewModel>()
            .ForMember(i => i.Categoria, opt => opt.MapFrom(src => src.Categoria));
            
            Mapper.CreateMap<Evento, PendenciaEventoViewModel>()
            .ForMember(e => e.Item, opt => opt.MapFrom(src => src.Item));

            Mapper.CreateMap<Promocao, ProximaPromocaoViewModel>()
            .ForMember(p => p.Usuario, opt => opt.MapFrom(src => src.Usuario));

            Mapper.CreateMap<Promocao, PromocaoAdquiridaViewModel>()
            .ForMember(p => p.CargoAdquirido, opt => opt.MapFrom(src => src.Cargo))
            .ForMember(p => p.CargoAnterior, opt => opt.MapFrom(src => src.CargoAnterior))
            .ForMember(p => p.Usuario, opt => opt.MapFrom(src => src.Usuario));

            Mapper.CreateMap<Usuario, UsuarioViewModel>()
            .ForMember( u => u.Cargo, opt => opt.MapFrom(src => src.Cargo))
            .ForMember(u => u.ProximoCargo, opt => opt.MapFrom(src => DataAccess.Instance.GetCargoRepository().RecuperaProximoCargo(src.cargo)));

            Mapper.CreateMap<Usuario, AddUsuarioViewModel>();


        }
    }
}