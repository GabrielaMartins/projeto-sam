using SamApi.Helpers;
using System.Net.Http;
using System.Web.Http;
using Opus.DataBaseEnvironment;
using System.Linq;
using AutoMapper;
using SamApiModels;
using System;
using System.Collections.Generic;
using System.Net;
using SamDataBase.Model;

namespace SamApi.Controllers
{

    [RoutePrefix("api/sam/perfil")]
    public class PerfilController : ApiController
    {

        [Route("{samaccount}")]
        [HttpGet]
        public HttpResponseMessage Get(string samaccount)
        {
            CommonOperations commonOperations = new CommonOperations(Request);
            HttpResponseMessage response = null;

            // this line check and prepare some variables for us
            commonOperations.Check();

            // if we have response, so it's an error
            if (commonOperations.ResponseError != null)
                return commonOperations.ResponseError;

            var token = commonOperations.DecodedToken;

            // fazer verificações com o token

            var user = DataAccess.Instance.UsuarioRepository().Find(u => u.samaccount.Equals(samaccount)).SingleOrDefault();
            if(user != null)
            {
                var usuario = Mapper.Map<Usuario, UsuarioViewModel>(user);
                //var eventos = user.Eventos.ToList(); TODO: Arrumar

                // teste
                var evento = new Evento()
                {
                    Pendencias = null,
                    ResultadoVotacoes = null,
                    Usuario = null,
                    Item = null,
                    usuario = null,
                    item = null,
                    tipo = "promocao",
                    data = DateTime.Now,
                    estado = true,
                    id = 1
                };

                List<Evento> eventos = new List<Evento>();
                eventos.Add(evento);
                var perfilViewModel = new PerfilViewModel() {Usuario = usuario, Eventos = eventos };
                response = Request.CreateResponse(HttpStatusCode.OK, perfilViewModel);
                return response;
            }
                
            return response;
        }
    }
}
