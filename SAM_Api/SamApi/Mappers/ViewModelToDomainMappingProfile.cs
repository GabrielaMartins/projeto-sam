using AutoMapper;
using SamApiModels;
using SamDataBase.Model;

namespace SamApi.Mappers
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        protected override void Configure()
        {

            Mapper.CreateMap<UsuarioViewModel, Usuario>();
            Mapper.CreateMap<CargoViewModel, Cargo>();
        }
    }
}