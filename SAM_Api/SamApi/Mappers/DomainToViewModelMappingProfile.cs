using AutoMapper;
using SamApiModels;
using SamDataBase.Model;

namespace SamApi.Mappers
{
    public class DomainToViewModelMappingProfile : Profile
    {
        protected override void Configure()
        {
            Mapper
                .CreateMap<Usuario, UsuarioViewModel>()
                .ForMember(
                    u => u.Cargo,
                    opt => opt.MapFrom(src => src.Cargo));

            Mapper.CreateMap<Cargo, CargoViewModel>();
                
        }
    }
}