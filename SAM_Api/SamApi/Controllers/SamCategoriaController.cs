using System.Web.Http;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using System;
using SamApi.Helpers;
using SamApiModels;

namespace SamApi.Controllers
{
    [RoutePrefix("api/sam/categoria")]
    public class SamCategoriaController : ApiController
    {
        // GET: api/sam/categoria/all
        [Route("all")]
        public HttpResponseMessage Get()
        {
            var categorias = new List<dynamic>()
            {
                new {nome = "CategoriaViewModel 1"},
                new {nome = "CategoriaViewModel 2"}
            };

            var response = Request.CreateResponse(HttpStatusCode.OK, categorias);
            response.Headers.CacheControl = new CacheControlHeaderValue()
            {
                MaxAge = TimeSpan.FromMinutes(20)
            };

            return response;

            //CommonOperations commonOperations = new CommonOperations(Request);
            //HttpResponseMessage response = null;

            //// this line check and prepare some variables for us
            //commonOperations.Check();

            //// if we have response, so it's an error
            //if (commonOperations.ResponseError != null)
            //    return commonOperations.ResponseError;

            //var token = commonOperations.DecodedToken;

            //// erase here
            //response = Request.CreateResponse(HttpStatusCode.OK, new Message(HttpStatusCode.ServiceUnavailable, "Not Implemented", "under construction"));
            //response.Headers.CacheControl = new CacheControlHeaderValue()
            //{
            //    MaxAge = TimeSpan.FromMinutes(20)
            //};

            //return response;
        }

        // GET: api/sam/categoria/{id}
        [Route("{id}")]
        public HttpResponseMessage Get(int id)
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
