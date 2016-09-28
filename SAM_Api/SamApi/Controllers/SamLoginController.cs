﻿using System.Web.Http;
using System.Net.Http;
using System.Net;
using System.Configuration;
using SamApiModels.Login;
using Swashbuckle.Swagger.Annotations;
using SamHelpers;
using Opus.DataBaseEnvironment;
using System.Linq;
using MessageSystem.Mensagem;
using MessageSystem.Erro;

namespace SamApiService.Controllers
{
    /// <summary>
    /// Oferece funcionalidades para controlar o login ao SAM
    /// </summary>
    [RoutePrefix("api/sam")]
    public class SamLoginController : ApiController
    {
        /// <summary>
        /// Gera o token de acesso para um usuário autenticado
        /// </summary>
        /// <param name="login">
        /// Representa as credenciais do usuário no AD da Opus
        /// </param>
        [SwaggerResponse(HttpStatusCode.OK, "Caso seja possível gerar o token de acesso do SAM", typeof(SamToken))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Caso o usuário não seja autorizada pelo AD da OPUS", typeof(DescriptionMessage))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Caso occora um erro não previsto", typeof(DescriptionMessage))]
        [Route("login")]
        [HttpPost]
        public HttpResponseMessage Login(LoginViewModel login)
        {

            string token = string.Empty;
            
            #if !DEBUG
                ActiveDirectoryHelper adConsumer = new ActiveDirectoryHelper(ConfigurationManager.AppSettings["OpusADServer"]);

                // ask Active Directory if the User's credentials is valid
            
                if (!adConsumer.IsValidUser(login.User, login.Password))
                {
                    // if credential is invalid, return an error
                    throw new ErroEsperado(HttpStatusCode.Unauthorized, "Unauthenticated", "The server could not authenticated the user");
                }
            #endif

            // if yes, get User's information from database
            using (var rep = DataAccess.Instance.GetUsuarioRepository())
            {
                var usuario = rep.Find(u => u.samaccount == login.User).SingleOrDefault();

                // check if our user not exists in our database
                if (usuario == null)
                {

                    // return a http error
                    throw new ErroEsperado(HttpStatusCode.NotFound, "User Not Found", $"We could not found the user '{login.User}' in our database");

                }

                // generate token based on User
                token = JwtHelper.GenerateToken(usuario);
                var tokenResult = new SamToken() { Token = token };

                // returns our token
                return Request.CreateResponse(HttpStatusCode.OK, tokenResult);
            }

        }
    }
}