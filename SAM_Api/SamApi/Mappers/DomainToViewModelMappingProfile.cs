﻿using AutoMapper;
using Opus.DataBaseEnvironment;
using SamApi.Helpers;
using SamApiModels;
using SamDataBase.Model;
using System.Collections.Generic;
using System.Linq;

namespace SamApi.Mappers
{
    public class DomainToViewModelMappingProfile : Profile
    {
        protected override void Configure()
        {

            Mapper.CreateMap<ResultadoVotacao, ResultadoVotacaoViewModel>()
            .ForMember(p => p.Evento, opt => opt.MapFrom(src => src.Evento))
            .ForMember(p => p.Usuario, opt => opt.MapFrom(src => src.Usuario));

            Mapper.CreateMap<Pendencia, PendenciaViewModel>()
            .ForMember(p => p.Evento, opt => opt.MapFrom(src => src.Evento))
            .ForMember(p => p.Usuario, opt => opt.MapFrom(src => src.Usuario));

            Mapper.CreateMap<Cargo, CargoViewModel>();

            Mapper.CreateMap<Evento, EventoViewModel>()
                .ForMember(
                    e => e.Item,
                    opt => opt.MapFrom(src => src.Item))
                .ForMember(
                    e => e.Usuario,
                    opt => opt.MapFrom(src => src.Usuario));

            Mapper.CreateMap<Categoria, CategoriaViewModel>();

            Mapper.CreateMap<Promocao, ProximaPromocaoViewModel>()
            .ForMember(
                u => u.Usuario,
                opt => opt.MapFrom(src => src.Usuario));

            Mapper.CreateMap<Item, ItemViewModel>()
                .ForMember(
                i => i.Categoria,
                opt => opt.MapFrom(src => src.Categoria));

            Mapper.CreateMap<Usuario, UsuarioViewModel>()
            .ForMember(
                u => u.Cargo,
                opt => opt.MapFrom(src => src.Cargo))
            .ForMember(
                u => u.foto,
                opt => opt.MapFrom(src => ImageHelper.GetLogicPathForImage(src.samaccount)))
            .ForMember(
                u => u.ProximoCargo,
                opt => opt.MapFrom(src => DataAccess.Instance.GetCargoRepository().RecuperaProximoCargo(src.cargo))
            );

            // Nao funciona
            //Mapper.CreateMap<List<Usuario>, List<UsuarioViewModel>>();

            //Mapper.CreateMap<List<Evento>, List<EventoViewModel>>();

            //Mapper.CreateMap<List<Cargo>, List<CargoViewModel>>();

            //Mapper.CreateMap<List<Pendencia>, List<PendenciaViewModel>>();


        }
    }
}