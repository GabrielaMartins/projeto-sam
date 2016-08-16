using AutoMapper;
using SamApi.Helpers;
using SamApiModels;
using SamApiModels.Categoria;
using SamApiModels.Event;
using SamApiModels.Item;
using SamApiModels.User;
using SamDataBase.Model;

namespace SamApi.Mappers
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        protected override void Configure()
        {
            // **************** UsuarioViewModel -> Usuario **************** //
            Mapper.CreateMap<UsuarioViewModel, Usuario>()

            // ignora as propriedades de navegacoes quando vai inserir no banco
            .ForMember(u => u.Cargo, opt => opt.Ignore())
            .ForMember(u => u.Eventos, opt => opt.Ignore())
            .ForMember(u => u.Pendencias, opt => opt.Ignore())
            .ForMember(u => u.Promocoes, opt => opt.Ignore())
            .ForMember(u => u.ResultadoVotacoes, opt => opt.Ignore())

            // mapeia as chaves estrangeiras
            .ForMember(u => u.cargo, opt => opt.MapFrom(src => src.Cargo.id))
            .ForMember(u => u.foto, opt => opt.MapFrom(src => ImageHelper.GetPhysicalPathForImage(src.samaccount)));


            // **************** ItemViewModel -> Item **************** //
            Mapper.CreateMap<ItemViewModel, Item>()

           // ignora as propriedades de navegacoes quando vai inserir no banco
           .ForMember(i => i.Categoria, opt => opt.Ignore())
           .ForMember(i => i.Eventos, opt => opt.Ignore())
           .ForMember(i => i.TaggedItens, opt => opt.Ignore())

           // mapeia as chaves estrangeiras
           .ForMember(i => i.categoria, opt => opt.MapFrom(src => src.Categoria.id));

            // **************** CategoriaViewModel -> Categoria **************** //
            Mapper.CreateMap<CategoriaViewModel, Categoria>()
           .ForMember(c => c.Itens, opt => opt.Ignore());

            // **************** EventoViewModel -> Evento **************** //
            Mapper.CreateMap<EventoViewModel, Evento>()

           // ignora as propriedades de navegacoes quando vai inserir no banco
           .ForMember(e => e.Pendencias, opt => opt.Ignore())
           .ForMember(e => e.ResultadoVotacoes, opt => opt.Ignore())
           .ForMember(e => e.Usuario, opt => opt.Ignore())
           .ForMember(e => e.Item, opt => opt.Ignore())

           // mapeia as chaves estrangeiras
           .ForMember(e => e.usuario, opt => opt.MapFrom(src => src.Usuario.id))
           .ForMember(e => e.item, opt => opt.MapFrom(src => src.Item.id));
        }
    }
}