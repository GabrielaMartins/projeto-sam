using System.Web.Http;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using System;
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
            
            // erase here
            var response = Request.CreateResponse(HttpStatusCode.OK, new MessageViewModel(HttpStatusCode.ServiceUnavailable, "Not Implemented", "under construction"));
            response.Headers.CacheControl = new CacheControlHeaderValue()
            {
                MaxAge = TimeSpan.FromMinutes(20)
            };

            return response;
        }


        // GET: api/sam/user/{samaccount}
        [Route("{samaccount}")]
        public HttpResponseMessage GetBySamaccount(string samaccount)
        {

            // TODO: verificações com o token
            using (var userRep = DataAccess.Instance.GetUsuarioRepository()) {

                var user = userRep.Find(u => u.samaccount.Equals(samaccount)).SingleOrDefault();


                if (user == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "User not found", "We can't find this user");
                }

                // Transform our Usuario model to UsuarioViewModel (Da erro)
                var usuarioViewModel = Mapper.Map<Usuario, UsuarioViewModel>(user);

                return Request.CreateResponse(HttpStatusCode.OK, usuarioViewModel);
            }

        }


        // POST: api/sam/user/save
        [Route("save")]
        public HttpResponseMessage Post([FromBody]UsuarioViewModel user)
        {

            //CommonOperations commonOperations = new CommonOperations(Request);
            //HttpResponseMessage response = null;

            //// this line check and prepare some variables for us
            //commonOperations.Check();

            //// if we have response, so it's an error
            //if (commonOperations.ResponseError != null)
            //    return commonOperations.ResponseError;

            //var token = commonOperations.DecodedToken;

            // erase here
            var response = Request.CreateResponse(HttpStatusCode.OK, new MessageViewModel(HttpStatusCode.ServiceUnavailable, "Not Implemented", "under construction"));
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
            using (var userRep = DataAccess.Instance.GetUsuarioRepository())
            {
                // erase here
                var response = Request.CreateResponse(HttpStatusCode.OK, new MessageViewModel(HttpStatusCode.ServiceUnavailable, "Not Implemented", "under construction"));
                response.Headers.CacheControl = new CacheControlHeaderValue()
                {
                    MaxAge = TimeSpan.FromMinutes(20)
                };

                return response;
            }
        }

        // DELETE: api/sam/user/delete/{id}
        [Route("delete/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            using (var userRep = DataAccess.Instance.GetUsuarioRepository())
            {
                // erase here
                var response = Request.CreateResponse(HttpStatusCode.OK, new MessageViewModel(HttpStatusCode.ServiceUnavailable, "Not Implemented", "under construction"));
                response.Headers.CacheControl = new CacheControlHeaderValue()
                {
                    MaxAge = TimeSpan.FromMinutes(20)
                };

                return response;
            }
        }
    }
}
