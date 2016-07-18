using System;
using System.Web.Http;
using Opus.Helpers.ActiveDirectoryService;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using Opus.DataBaseEnvironment;
using System.Linq;
using SamApiModels;
using AutoMapper;
using SamDataBase.Model;
using Opus.Helpers;

namespace SamApiService.Controllers
{

    [RoutePrefix("api/sam")]
    public class SamLoginController : ApiController
    {

        // GET api/sam/login
        [Route("login")]
        [HttpPost]
        public HttpResponseMessage Login(LoginViewModel login)
        {

            ActiveDirectoryConsumer adConsumer = new ActiveDirectoryConsumer("opus.local");
            string token = string.Empty;

            // ask Active Directory if the User's credentials is valid
            if (!adConsumer.IsValidUser(login.User, login.Password))
            {
                // if credential is invalid, return an error
                return Request.CreateResponse(HttpStatusCode.Unauthorized, MessageViewModel.Unauthenticated);
            }


            // if yes, get User's information from database
            var usr = DataAccess.Instance.UsuarioRepository().Find(u => u.samaccount.Equals(login.User)).SingleOrDefault();

            // check if our user not exists in our database
            if (usr == null)
            {

                // return a http error
                var error = new MessageViewModel(HttpStatusCode.NotFound, "User Not Found", "We could not found the user '" + login.User + "' in our database");
                return Request.CreateResponse(HttpStatusCode.NotFound, error);
            }

            // Transform our Usuario model to UsuarioViewModel
            var usuarioViewModel = Mapper.Map<Usuario, UsuarioViewModel>(usr);

            // generate token based on User
            token = JwtManagement.GenerateToken(usuarioViewModel);
            var tokenResult = new Dictionary<string, object>() { { "token", token } };

            // returns our token
            return Request.CreateResponse(HttpStatusCode.OK, tokenResult);

        }

        // GET api/sam/testGenerateForUser/{samaccount}
        [Route("testGenerateForUser/{samaccount}")]
        [HttpGet]
        public HttpResponseMessage TestGenerate(string samaccount)
        {

            // if yes, get User's information from database
            var usr = DataAccess.Instance.UsuarioRepository().Find(u => u.samaccount.Equals(samaccount)).SingleOrDefault();

            // check if our user not exists in our database
            if (usr == null)
            {

                // return a http error
                var error = new MessageViewModel(HttpStatusCode.NotFound, "User Not Found", "We could not found the user '" + samaccount + "' in our database");
                return Request.CreateResponse(HttpStatusCode.NotFound, error);
            }

            // Transform our Usuario model to UsuarioViewModel
            var usuarioViewModel = Mapper.Map<Usuario, UsuarioViewModel>(usr);

            // generate token based on User
            var token = JwtManagement.GenerateToken(usuarioViewModel);
            var tokenResult = new Dictionary<string, object>() { { "token", token } };

            // returns our token
            return Request.CreateResponse(HttpStatusCode.OK, tokenResult);
        }

        // GET api/sam/testDecode
        [Route("testDecode")]
        [HttpGet]
        public HttpResponseMessage TestDecode()
        {
            var token = HeaderHandler.ExtractHeaderValue(Request, "token");
            if (token == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, MessageViewModel.TokenMissing);
            }

            IDictionary<string, object> payload = null;
            try
            {
                var secretKey = "GQDstcKsx0NHjPOuXOYg5MbeJ1XT0uFiwDVvVBrk";
                string valor = token.SingleOrDefault();
                string jsonPayload = JWT.JsonWebToken.Decode(valor, secretKey);
                payload = JWT.JsonWebToken.DecodeToObject(valor, secretKey) as IDictionary<string, object>;

                return Request.CreateResponse(HttpStatusCode.OK, payload);
            }
            catch (JWT.SignatureVerificationException)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, MessageViewModel.InvalidToken);
            }

        }
    }
}