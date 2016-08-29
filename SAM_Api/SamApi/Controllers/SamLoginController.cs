using System.Web.Http;
using Opus.Helpers.ActiveDirectoryService;
using System.Net.Http;
using System.Net;
using System.Linq;
using Opus.Helpers;
using System.Configuration;
using DefaultException.Models;
using SamApiModels.User;
using SamApiModels.Login;
using Swashbuckle.Swagger.Annotations;
using SamServices.Services;

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

            // if yes, get User's information from database
            var usuario = UserServices.Recupera(login.User);
               
            // check if our user not exists in our database
            if (usuario == null)
            {
          
                // return a http error
                throw new ExpectedException(HttpStatusCode.NotFound, "User Not Found", $"We could not found the user '{login.User}' in our database");
                   
            }

                
            // generate token based on User
            token = JwtHelper.GenerateToken(usuario);
            var tokenResult = new SamToken(){Token = token};

            // returns our token
            return Request.CreateResponse(HttpStatusCode.OK, tokenResult);
            
        }
    }
}