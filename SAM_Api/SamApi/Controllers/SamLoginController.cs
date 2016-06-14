﻿using System;
using System.Web.Http;
using Opus.Helpers.ActiveDirectoryService;
using Opus.Helpers.Security;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using Opus.DataBaseEnvironment;
using System.Linq;
using SamApiModels;
using AutoMapper;
using SamDataBase.Model;

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
            HttpResponseMessage response = null;
            string token = string.Empty;

            // ask Active Directory if the User's credentials is valid
            if (adConsumer.IsValidUser(login.User, login.Password))
            {

                // if yes, get User's information from database
                var userRepository = DataAccess.Instance.UsuarioRepository();
                var usr = userRepository.Find(u => u.samaccount.Equals(login.User)).SingleOrDefault();
                
                // check if our user not exists in our database
                if (usr == null)
                {

                    // return a http error
                    var error1 = new MessageViewModel(HttpStatusCode.NotFound, "User Not Found", "We could not found the user '" + login.User + "' in our database");
                   
                    response = Request.CreateResponse(HttpStatusCode.NotFound, error1);
                    response.Headers.CacheControl = new CacheControlHeaderValue()
                    {
                        MaxAge = TimeSpan.FromMinutes(20)
                    };

                    return response;
                }
                else
                {
                    // Transform our Usuario model to UsuarioViewModel (erro)
                    var usuarioViewModel = Mapper.Map<Usuario, UsuarioViewModel>(usr);

                    // generate token based on User
                    token = JwtManagement.GenerateToken(usuarioViewModel);
                    var result2 = new Dictionary<string, object>() { { "token", token } };
                    response = Request.CreateResponse(HttpStatusCode.OK, result2);
                    response.Headers.CacheControl = new CacheControlHeaderValue()
                    {
                        MaxAge = TimeSpan.FromMinutes(20)
                    };

                    return response;
                }
            }

            // if credential is invalid
            var error2 = new MessageViewModel(HttpStatusCode.Unauthorized, "User Not Authorized", "User login has failed");
            response = Request.CreateResponse(HttpStatusCode.Unauthorized, error2);
            response.Headers.CacheControl = new CacheControlHeaderValue()
            {
                MaxAge = TimeSpan.FromMinutes(20)
            };

            return response;

        }

        // GET api/sam/testGenerate
        [Route("testGenerate")]
        [HttpGet]
        public Dictionary<string, string> TestGenerate()
        {
            // variables to configure our token
            var currentTime = System.DateTime.Now;
            //var expTime = currentTime.AddMinutes(500000L);

            var userInfo = new Dictionary<string, object>()
            {
                {"key", "jesley"},
                {"name", "Jesley Marcelino"},
                {"email", "jesley@opus.software.com.br"}

                // we can put more information here
            };

            var context = new Dictionary<string, object>()
            {
                {"user", userInfo}

                // we can put more information here
            };

            var payload = new Dictionary<string, object>()
            {
                // informa o cliente quem emitiu o token
                { "iss", "http://opus.sam.com" },

                // informa a dasta e hora que o token foi emitido
                { "iat", currentTime},

                // esse é o tempo de vida do token (isso faz dar erro)
                //{ "exp", expTime},

                // assunto do token
                { "sub", "Jesley Marcelino"},

                // contém informações que queremos colocar no token, como usuário por exemplo
                { "context", context }

            };

            var secretKey = "GQDstcKsx0NHjPOuXOYg5MbeJ1XT0uFiwDVvVBrk";
            string token = JWT.JsonWebToken.Encode(payload, secretKey, JWT.JwtHashAlgorithm.HS256);

            return new Dictionary<string, string>()
            {
                { "token", token }
            };

        }

        // GET api/sam/testDecode
        [Route("testDecode")]
        [HttpGet]
        public HttpResponseMessage TestDecode([FromUri] string token)
        {
            HttpResponseMessage response = null;
            IDictionary<string, object> payload = null;
            try
            {
                var secretKey = "GQDstcKsx0NHjPOuXOYg5MbeJ1XT0uFiwDVvVBrk";

                string jsonPayload = JWT.JsonWebToken.Decode(token, secretKey);
                payload = JWT.JsonWebToken.DecodeToObject(token, secretKey) as IDictionary<string, object>;

                response = Request.CreateResponse(HttpStatusCode.OK,payload);
            }
            catch (JWT.SignatureVerificationException)
            {
                var res = new Dictionary<string, object>()
                {
                    {"code", HttpStatusCode.InternalServerError},
                    {"title", "Invalid token!"},
                    {"message", "The token signature has failed"}
                };
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, res);
            }

            return response;
        }
    }
}