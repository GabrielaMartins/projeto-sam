using System.Net.Http;
using System.Web.Http;
using System.Net;
using Opus.DataBaseEnvironment;
using SamApiModels;
using System.Linq;
using AutoMapper;
using SamDataBase.Model;
using System;

namespace SamApi.Controllers
{

    [RoutePrefix("api/sam/perfil")]
    public class SamPerfilController : ApiController
    {

        [Route("{samaccount}")]
        [HttpGet]
        public HttpResponseMessage Get(string samaccount)
        {

            // fazer verificações com o token
            //CommonOperations commonOperations = new CommonOperations(Request);
            //HttpResponseMessage response = null;

            //// this line check and prepare some variables for us
            //commonOperations.Check();

            //// if we have response, so it's an error
            //if (commonOperations.ResponseError != null)
            //    return commonOperations.ResponseError;

            //var token = commonOperations.DecodedToken;

            try
            {
                var usuario = DataAccess.Instance.UsuarioRepository().Find(u => u.samaccount == samaccount).SingleOrDefault();
                var usuarioViewModel = Mapper.Map<Usuario, UsuarioViewModel>(usuario);

                var eventosViewModel = DataAccess.Instance.UsuarioRepository().RecuperaEventos(usuario).Take(10);
                var perfilViewModel = new PerfilViewModel() { Usuario = usuarioViewModel, Eventos = eventosViewModel.ToList() };
                return Request.CreateResponse(HttpStatusCode.OK, perfilViewModel);
            }
            catch (NullReferenceException)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new MessageViewModel(HttpStatusCode.NotFound, "Perfil not found", "we can't find perfil for this user"));
            }

        }

    }
}
