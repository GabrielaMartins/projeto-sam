using AutoMapper;
using SamApiModels;
using SamDataBase.Model;

namespace SamApi.Mappers
{
    public class DomainToViewModelMappingProfile : Profile
    {
        protected override void Configure()
        {

            Mapper.CreateMap<Cargo, CargoViewModel>();

            Mapper
                .CreateMap<Usuario, UsuarioViewModel>()
                .ForMember(
                    u => u.Cargo,
                    opt => opt.MapFrom(src => src.Cargo))
                .ForMember(
                    u => u.redes,
                    opt => opt.MapFrom(src => src.redes.Split(';')));            
                
        }
    }
}