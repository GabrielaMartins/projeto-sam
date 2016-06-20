using AutoMapper;
using Opus.DataBaseEnvironment;
using SamApiModels;
using SamDataBase.Model;
using System.Collections.Generic;

namespace SamApi.Mappers
{
    public class DomainToViewModelMappingProfile : Profile
    {
        protected override void Configure()
        {

            Mapper.CreateMap<Cargo, CargoViewModel>();

            Mapper.CreateMap<Evento, EventoViewModel>();

            Mapper
                .CreateMap<Usuario, UsuarioViewModel>()
                .ForMember(
                    u => u.Cargo,
                    opt => opt.MapFrom(src => src.Cargo))
                .ForMember(
                    u => u.ProximoCargo,
                    opt => opt.MapFrom(src => DataAccess.Instance.UsuarioRepository().RecuperaProximoCargo(src.samaccount)))
                .ForMember(
                    u => u.redes,
                    opt => opt.MapFrom(src => src.redes.Split(';')));            
                
        }
    }
}