using System.Net.Http;
using System.Web.Http;
using System.Net;
using Opus.DataBaseEnvironment;
using SamApiModels;
using System.Linq;
using AutoMapper;
using SamDataBase.Model;
using System;
using Opus.Helpers;
using System.Collections.Generic;

namespace SamApi.Controllers
{

    [RoutePrefix("api/sam/perfil")]
    public class SamPerfilController : ApiController
    {

        [Route("")]
        [HttpGet]
        public HttpResponseMessage Get()
        {

            var token = HeaderHandler.ExtractHeaderValue(Request, "token");
            if (token == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, MessageViewModel.TokenMissing);
            }

            var decodedToken = JwtManagement.DecodeToken(token.SingleOrDefault());
            var context = decodedToken["context"] as Dictionary<string, object>;
            var userInfo = context["user"] as Dictionary<string, object>;
            var samaccount = userInfo["samaccount"] as string;

            var usuario = DataAccess.Instance.UsuarioRepository().Find(u => u.samaccount == samaccount).SingleOrDefault();

            // dados para o perfil
            var usuarioViewModel = Mapper.Map<Usuario, UsuarioViewModel>(usuario);
            var eventosViewModel = DataAccess.Instance.UsuarioRepository().RecuperaEventos(usuario);
            var promocoesAdquiridas = DataAccess.Instance.UsuarioRepository().RecuperaPromocoesAdquiridas(usuario);

            var perfilViewModel = new PerfilViewModel()
            {
                Usuario = usuarioViewModel,
                Atividades = eventosViewModel.ToList(),
                PromocoesAdquiridas = promocoesAdquiridas
            };

            return Request.CreateResponse(HttpStatusCode.OK, perfilViewModel);

        }

    }
}
