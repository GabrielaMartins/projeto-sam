using AutoMapper;
using SamApi.Helpers;
using SamApiModels;
using SamDataBase.Model;

namespace SamApi.Mappers
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        protected override void Configure()
        {

            Mapper.CreateMap<UsuarioViewModel, Usuario>()
            .ForMember(u => u.Cargo, opt => opt.Ignore())
            .ForMember(u => u.Eventos, opt => opt.Ignore())
            .ForMember(u => u.Pendencias, opt => opt.Ignore())
            .ForMember(u => u.Promocoes, opt => opt.Ignore())
            .ForMember(u => u.ResultadoVotacoes, opt => opt.Ignore())
            .ForMember(u => u.foto, opt => opt.MapFrom(src => ImageHelper.GetPhysicalPathForImage(src.samaccount)))
            .ForMember(u => u.cargo, opt => opt.MapFrom(src => src.Cargo.id));
        }
    }
}