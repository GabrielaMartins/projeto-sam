using AutoMapper;
using SamApiModels;
using SamApiModels.Cargo;
using SamApiModels.Categoria;
using SamApiModels.Evento;
using SamApiModels.Item;
using SamApiModels.Pendencia;

namespace SamServices.Mappers
{
    public class ViewModelToViewModelMappingProfile : Profile
    {
        protected override void Configure()
        {

            Mapper.CreateMap<EventoViewModel, PendenciaEventoViewModel>();

            Mapper.CreateMap<EventoItemViewModel, ItemViewModel>();

        }
    }
}