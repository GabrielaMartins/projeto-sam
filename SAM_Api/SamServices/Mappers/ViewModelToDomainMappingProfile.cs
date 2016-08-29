using AutoMapper;
using Opus.DataBaseEnvironment;
using SamApiModels.Categoria;
using SamApiModels.Evento;
using SamApiModels.Item;
using SamApiModels.Models.Agendamento;
using SamApiModels.Models.User;
using SamApiModels.User;
using SamApiModels.Votacao;
using SamDataBase.Model;
using System.Linq;

namespace SamServices.Mappers
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        protected override void Configure()
        {

            // **************** AddVotoViewModel -> ResultadoVotacao **************** //
            Mapper.CreateMap<AddVotoViewModel, ResultadoVotacao>()

            // ignora as propriedades de navegacoes quando vai inserir no banco
            .ForMember(e => e.Evento, opt => opt.Ignore())
            .ForMember(e => e.Usuario, opt => opt.Ignore())

            // mapeia campos com nomes diferentes
            .ForMember(e => e.dificuldade, opt => opt.MapFrom(src => src.Dificuldade))
            .ForMember(e => e.modificador, opt => opt.MapFrom(src => src.Modificador))
            .ForMember(e => e.evento, opt => opt.MapFrom(src => src.Evento))
            .ForMember(e => e.usuario, opt => opt.MapFrom(src => DataAccess
                                                                .Instance.GetUsuarioRepository()
                                                                .Find(u => u.samaccount == src.Usuario)
                                                                .Select(x => x.id)
                                                                .SingleOrDefault()
                                                        ));
            

            // **************** AgendamentoViewModel -> Evento **************** //
            Mapper.CreateMap<AgendamentoViewModel, Evento>()

            // ignora as propriedades de navegacoes quando vai inserir no banco
            .ForMember(e => e.Item, opt => opt.Ignore())
            .ForMember(e => e.Pendencias, opt => opt.Ignore())
            .ForMember(e => e.ResultadoVotacoes, opt => opt.Ignore())
            .ForMember(e => e.Usuario, opt => opt.Ignore())

            // mapeia as chaves estrangeiras
            .ForMember(e => e.data, opt => opt.MapFrom(src => src.Data))
            .ForMember(e => e.item, opt => opt.MapFrom(src => src.Item))
            .ForMember(e => e.usuario, opt => opt.MapFrom(src =>
                       DataAccess.Instance.GetUsuarioRepository()
                       .Find(u => u.samaccount == src.funcionario)
                       .Select(u => u.id).SingleOrDefault())
            );

            // **************** AddUsuarioViewModel -> Usuario **************** //
            Mapper.CreateMap<AddUsuarioViewModel, Usuario>()

            // ignora as propriedades de navegacoes quando vai inserir no banco
            .ForMember(u => u.Cargo, opt => opt.Ignore())
            .ForMember(u => u.Eventos, opt => opt.Ignore())
            .ForMember(u => u.Pendencias, opt => opt.Ignore())
            .ForMember(u => u.Promocoes, opt => opt.Ignore())
            .ForMember(u => u.ResultadoVotacoes, opt => opt.Ignore())

            // mapeia as chaves estrangeiras
            .ForMember(u => u.cargo, opt => opt.MapFrom(src => src.cargo));

            // **************** UpdateUsuarioViewModel -> Usuario **************** //
            Mapper.CreateMap<UpdateUsuarioViewModel, Usuario>()

            // ignora as propriedades de navegacoes quando vai inserir no banco
            .ForMember(u => u.Cargo, opt => opt.Ignore())
            .ForMember(u => u.Eventos, opt => opt.Ignore())
            .ForMember(u => u.Pendencias, opt => opt.Ignore())
            .ForMember(u => u.Promocoes, opt => opt.Ignore())
            .ForMember(u => u.ResultadoVotacoes, opt => opt.Ignore())

            // mapeia as chaves estrangeiras
            .ForMember(u => u.cargo, opt => opt.MapFrom(src => src.cargo));

            // **************** UsuarioViewModel -> Usuario **************** //
            Mapper.CreateMap<UsuarioViewModel, Usuario>()

            //.ForMember(u => u.foto, opt => opt.MapFrom(src => ImageHelper.GetPhysicalPathForImage(src.samaccount)))
            
            // ignora as propriedades de navegacoes quando vai inserir no banco
            .ForMember(u => u.Cargo, opt => opt.Ignore())
            .ForMember(u => u.Eventos, opt => opt.Ignore())
            .ForMember(u => u.Pendencias, opt => opt.Ignore())
            .ForMember(u => u.Promocoes, opt => opt.Ignore())
            .ForMember(u => u.ResultadoVotacoes, opt => opt.Ignore())

            // mapeia as chaves estrangeiras
            .ForMember(u => u.cargo, opt => opt.MapFrom(src => src.Cargo.id));


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