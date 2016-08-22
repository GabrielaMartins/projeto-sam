using System.Web.Http;
using Opus.Helpers.ActiveDirectoryService;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using Opus.DataBaseEnvironment;
using System.Linq;
using AutoMapper;
using SamDataBase.Model;
using Opus.Helpers;
using System.Configuration;
using DefaultException.Models;
using SamApiModels.User;
using SamApiModels.Login;
using Swashbuckle.Swagger.Annotations;

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
        /// Representa as credencias do usuário no AD da Opus
        /// </param>
        [SwaggerResponse(HttpStatusCode.OK, "Caso seja possível gerar o token de acesso do SAM", typeof(SamToken))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Caso o usuário não seja autorizada pelo AD da OPUS", typeof(DescriptionMessage))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Caso occora um erro não previsto", typeof(DescriptionMessage))]
        [Route("login")]
        [HttpPost]
        public HttpResponseMessage Login(LoginViewModel login)
        {

            ActiveDirectoryHelper adConsumer = new ActiveDirectoryHelper(ConfigurationManager.AppSettings["OpusADServer"]);
            string token = string.Empty;

            // ask Active Directory if the User's credentials is valid
            #if !DEBUG
                if (!adConsumer.IsValidUser(login.User, login.Password))
                {
                    // if credential is invalid, return an error
                    throw new ExpectedException(HttpStatusCode.Unauthorized, "Unauthenticated", "The server could not authenticated the user");
                }
            #endif
            using (var userRep = DataAccess.Instance.GetUsuarioRepository()) {

                // if yes, get User's information from database
                var query = userRep.Find(u => u.samaccount.Equals(login.User));
                var usr = query.SingleOrDefault();

                // check if our user not exists in our database
                if (usr == null)
                {

                    // return a http error
                    throw new ExpectedException(HttpStatusCode.NotFound, "User Not Found", $"We could not found the user '{login.User}' in our database");
                   
                }

                // Transform our Usuario model to UsuarioViewModel
                var usuarioViewModel = Mapper.Map<Usuario, UsuarioViewModel>(usr);

                // generate token based on User
                token = JwtHelper.GenerateToken(usuarioViewModel);
                var tokenResult = new SamToken(){Token = token};

                // returns our token
                return Request.CreateResponse(HttpStatusCode.OK, tokenResult);
            }
        }
    }
}