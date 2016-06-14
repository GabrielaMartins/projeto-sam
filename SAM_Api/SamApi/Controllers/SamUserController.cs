using System.Web.Http;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using System;
using SamApi.Helpers;
using Opus.DataBaseEnvironment;
using SamApiModels;
using AutoMapper;
using SamDataBase.Model;

namespace SamApi.Controllers
{
    [RoutePrefix("api/sam/user")]
    public class SamUserController : ApiController
    {
        // GET: api/sam/user/all
        [Route("all")]
        public HttpResponseMessage Get()
        {
            CommonOperations commonOperations = new CommonOperations(Request);
            HttpResponseMessage response = null;

            // this line check and prepare some variables for us
            commonOperations.Check();

            // if we have response, so it's an error
            if (commonOperations.ResponseError != null)
                return commonOperations.ResponseError;

            var token = commonOperations.DecodedToken;

            // fill here

            // ********* //

            // erase here
            response = Request.CreateResponse(HttpStatusCode.OK, new MessageViewModel(HttpStatusCode.ServiceUnavailable, "Not Implemented", "under construction"));
            response.Headers.CacheControl = new CacheControlHeaderValue()
            {
                MaxAge = TimeSpan.FromMinutes(20)
            };

            return response;
        }

        // GET: api/sam/user/{id:int}
        [Route("{id:int}")]
        public HttpResponseMessage GetById(int id)
        {

            CommonOperations commonOperations = new CommonOperations(Request);
            HttpResponseMessage response = null;

            // this line check and prepare some variables for us
            commonOperations.Check();

            // if we have response, so it's an error
            if (commonOperations.ResponseError != null)
                return commonOperations.ResponseError;

            var token = commonOperations.DecodedToken;

            // erase here
            response = Request.CreateResponse(HttpStatusCode.OK, new MessageViewModel(HttpStatusCode.ServiceUnavailable, "Not Implemented", "under construction"));
            response.Headers.CacheControl = new CacheControlHeaderValue()
            {
                MaxAge = TimeSpan.FromMinutes(20)
            };

            return response;
        }

        // GET: api/sam/user/{samaccount}
        [Route("{samaccount}")]
        [System.Web.Http.Cors.EnableCors("*", "*", "*")]
        public HttpResponseMessage GetBySamaccount(string samaccount)
        {

            CommonOperations commonOperations = new CommonOperations(Request);
            HttpResponseMessage response = null;

            // this line check and prepare some variables for us
            commonOperations.Check();

            // if we have response, so it's an error
            if (commonOperations.ResponseError != null)
                return commonOperations.ResponseError;

            var token = commonOperations.DecodedToken;


            // check if token allow us to get user
            var ctx = token["context"] as Dictionary<string, object>;
            var userInfo = ctx["user"] as Dictionary<string, object>;
            var userSamaccount = userInfo["samaccount"];
            var userPerfil = ctx["perfil"];

            // se o perfil for Funcionário
            if (userPerfil.Equals("Funcionário"))
            {
                // só permite buscar a si próprio
                if (userSamaccount.Equals(samaccount))
                {
                    var userRepository = DataAccess.Instance.UsuarioRepository();
                    var user = userRepository.Find(u => u.samaccount.Equals(samaccount)).SingleOrDefault();


                    if (user == null)
                    {

                    }

                    // Transform our Usuario model to UsuarioViewModel (Da erro)
                    var usuarioViewModel = Mapper.Map<Usuario, UsuarioViewModel>(user);

                    response = Request.CreateResponse(HttpStatusCode.OK, usuarioViewModel); 
                    return response;
                }

            }

            return response;
        }

        // POST: api/sam/user/save
        [Route("save")]
        public HttpResponseMessage Post([FromBody]UsuarioViewModel user)
        {

            CommonOperations commonOperations = new CommonOperations(Request);
            HttpResponseMessage response = null;

            // this line check and prepare some variables for us
            commonOperations.Check();

            // if we have response, so it's an error
            if (commonOperations.ResponseError != null)
                return commonOperations.ResponseError;

            var token = commonOperations.DecodedToken;

            // erase here
            response = Request.CreateResponse(HttpStatusCode.OK, new MessageViewModel(HttpStatusCode.ServiceUnavailable, "Not Implemented", "under construction"));
            response.Headers.CacheControl = new CacheControlHeaderValue()
            {
                MaxAge = TimeSpan.FromMinutes(20)
            };

            return response;
        }

        // PUT: api/sam/user/update/{id}
        [Route("update/{id}")]
        public HttpResponseMessage Put(int id, [FromBody]UsuarioViewModel user)
        {

            CommonOperations commonOperations = new CommonOperations(Request);
            HttpResponseMessage response = null;

            // this line check and prepare some variables for us
            commonOperations.Check();

            // if we have response, so it's an error
            if (commonOperations.ResponseError != null)
                return commonOperations.ResponseError;

            var token = commonOperations.DecodedToken;

            // erase here
            response = Request.CreateResponse(HttpStatusCode.OK, new MessageViewModel(HttpStatusCode.ServiceUnavailable, "Not Implemented", "under construction"));
            response.Headers.CacheControl = new CacheControlHeaderValue()
            {
                MaxAge = TimeSpan.FromMinutes(20)
            };

            return response;

        }

        // DELETE: api/sam/user/delete/{id}
        [Route("delete/{id}")]
        public HttpResponseMessage Delete(int id)
        {

            CommonOperations commonOperations = new CommonOperations(Request);
            HttpResponseMessage response = null;

            // this line check and prepare some variables for us
            commonOperations.Check();

            // if we have response, so it's an error
            if (commonOperations.ResponseError != null)
                return commonOperations.ResponseError;

            var token = commonOperations.DecodedToken;

            // erase here
            response = Request.CreateResponse(HttpStatusCode.OK, new MessageViewModel(HttpStatusCode.ServiceUnavailable, "Not Implemented", "under construction"));
            response.Headers.CacheControl = new CacheControlHeaderValue()
            {
                MaxAge = TimeSpan.FromMinutes(20)
            };

            return response;
        }
    }
}
