﻿using AutoMapper;
using Opus.DataBaseEnvironment;
using SamApiModels.Evento;
using SamApiModels.Promocao;
using SamApiModels.User;
using SamApiModels.Votacao;
using SamDataBase.Model;
using System.Collections.Generic;
using System;
using System.Linq;
using DefaultException.Models;
using System.Net;
using System.Configuration;
using System.Web;
using SamServices.Helpers;
using SamApiModels.Models.User;

namespace SamServices.Services
{
    public static class UserServices
    {

        public static List<EventoViewModel> RecuperaEventos(UsuarioViewModel usuario, int? quantidade = null, string tipo = null)
        {
            using (var userRep = DataAccess.Instance.GetUsuarioRepository())
            using (var itemRep = DataAccess.Instance.GetItemRepository())
            {
                var eventos = userRep.RecuperaEventos(usuario.id, quantidade, tipo);
                var eventoViewModels = Mapper.Map<List<Evento>, List<EventoViewModel>>(eventos);
                var r = new List<EventoViewModel>();
                foreach (var evento in eventoViewModels)
                {
                    var usuariosViewModel = Mapper.Map<List<Usuario>, List<UsuarioViewModel>>(itemRep.RecuperaUsuariosQueFizeram(evento.Item.id));
                    evento.Item.Usuarios = usuariosViewModel;
                    r.Add(evento);
                }

                return r;
            }
        }

        public static UsuarioViewModel Recupera(string samaccount)
        {
            using (var rep = DataAccess.Instance.GetUsuarioRepository())
            {
                var usuario = Mapper.Map<Usuario, UsuarioViewModel>(rep.Find(u => u.samaccount == samaccount).SingleOrDefault());

                return usuario;
            }
        }

        public static List<UsuarioViewModel> RecuperaTodos()
        {
            using (var userRep = DataAccess.Instance.GetUsuarioRepository())
            {
        
                var users = Mapper.Map<List<Usuario>, List<UsuarioViewModel>>(userRep.GetAll().ToList());

                return users;
            }
        }

        public static List<PromocaoAdquiridaViewModel> RecuperaPromocoesAdquiridas(UsuarioViewModel usuario)
        {
            using(var userRep = DataAccess.Instance.GetUsuarioRepository())
            {
                var promocoesAdquiridas = userRep.RecuperaPromocoesAdquiridas(usuario.id);

                var promocoesAdquiridasViewModel = Mapper.Map<List<Promocao>, List<PromocaoAdquiridaViewModel>>(promocoesAdquiridas);

                return promocoesAdquiridasViewModel;
            }
        }

        public static List<PendenciaUsuarioViewModel> RecuperaPendencias(UsuarioViewModel usuario)
        {
            using(var userRep = DataAccess.Instance.GetUsuarioRepository())
            {
                var pendencias = userRep.RecuperaPendencias(usuario.id);

                var pendeciasViewModel = Mapper.Map<List<Pendencia>, List<PendenciaUsuarioViewModel>>(pendencias);

                return pendeciasViewModel;
            }
        }

        public static List<VotoViewModel> RecuperaVotos(UsuarioViewModel usuario, int? quantity = null)
        {
            using(var userRep = DataAccess.Instance.GetUsuarioRepository())
            {
                var votacoes = userRep.RecuperaVotos(usuario.id, quantity);

                var votacoesViewModel = Mapper.Map<List<ResultadoVotacao>, List<VotoViewModel>>(votacoes);

                return votacoesViewModel;
            }
        }
        
        // TODO: Preciso implementar
        public static List<ProximaPromocaoViewModel> RecuperaProximasPromocoes(Usuario usuario)
        {

            return null;
        }

        public static void CriaUsuario(AddUsuarioViewModel user)
        {
            using (var userRep = DataAccess.Instance.GetUsuarioRepository())
            {

                var userFound = userRep.Find(u => u.samaccount == user.samaccount).SingleOrDefault() != null;
                if (userFound)
                {
                    throw new ExpectedException(HttpStatusCode.Forbidden, "Duplicated User", $"user '{user.samaccount}' already exists");
                }

                // save image to disk (we need do it before all other task)
                var logicPath = ConfigurationManager.AppSettings["LogicImagePath"];
                var physicalPath = HttpContext.Current.Server.MapPath(logicPath);

                ImageHelper.SaveAsImage(user.foto, user.samaccount, physicalPath);

                user.foto = ImageHelper.GetLogicPathForImage(user.samaccount);

                // map new values to our reference
                var newUser = Mapper.Map<AddUsuarioViewModel, Usuario>(user);

                // add to entity context
                userRep.Add(newUser);

                // commit changes
                userRep.SubmitChanges();
            }
        }

        public static void AtualizaUsuario(string samaccount, UpdateUsuarioViewModel user)
        {
            using (var userRep = DataAccess.Instance.GetUsuarioRepository())
            {

                // it will be updated with values provided by the parameter
                var userToBeUpdated = userRep.Find(u => u.samaccount == samaccount).SingleOrDefault();
                if (userToBeUpdated == null)
                {
                    throw new ExpectedException(HttpStatusCode.NotFound, "User Not Found", $"The server could not find the user '{samaccount}'");
                }

                if (user.foto == "")
                {
                    user.foto = userToBeUpdated.foto;
                }
                else
                {
                    // save image to disk
                    var logicPath = ConfigurationManager.AppSettings["LogicImagePath"];
                    var physicalPath = HttpContext.Current.Server.MapPath(logicPath);

                    ImageHelper.SaveAsImage(user.foto, userToBeUpdated.samaccount, physicalPath);

                    // update image path
                    user.foto = ImageHelper.GetLogicPathForImage(userToBeUpdated.samaccount);
                }

                // map values from 'user' to 'userToBeUpdated'
                var updatedUser = Mapper.Map(user, userToBeUpdated);

                // update: flush changes to proxy
                userRep.Update(updatedUser);

                // commit changes to database
                userRep.SubmitChanges();
            }
        }

        public static void DeletaUsuario(string samaccount)
        {
            using (var userRep = DataAccess.Instance.GetUsuarioRepository())
            {
                var usr = userRep.Find(u => u.samaccount == samaccount).SingleOrDefault();
                userRep.Delete(usr.id);
                userRep.SubmitChanges();
            }
        }
    }
}