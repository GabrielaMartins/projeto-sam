using System;
using System.Web.Http;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using Opus.DataBaseEnvironment;
using AutoMapper;
using SamDataBase.Model;
using SamApi.Helpers;
using SamApi.Attributes;
using SamApiModels.User;
using SamApiModels.Message;
using DefaultException.Models;

namespace SamApi.Controllers
{
    [RoutePrefix("api/sam/user")]
    public class SamUserController : ApiController
    {
        // GET: api/sam/user/all
        [Route("all")]
        [HttpGet]
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
        [HttpGet]
        [SamAuthorize(Roles="rh")]
        public HttpResponseMessage GetBySamaccount(string samaccount)
        {

            using (var userRep = DataAccess.Instance.GetUsuarioRepository())
            {

                var user = userRep.Find(u => u.samaccount.Equals(samaccount)).SingleOrDefault();


                if (user == null)
                {
                    throw new ExpectedException(HttpStatusCode.NotFound, "User not found", "We can't find this user");
                }

                // Transform our Usuario model to UsuarioViewModel
                var usuarioViewModel = Mapper.Map<Usuario, UsuarioViewModel>(user);

                return Request.CreateResponse(HttpStatusCode.OK, usuarioViewModel);
            }

        }


        // POST: api/sam/user/save
        [Route("save")]
        [HttpPost]
        [SamAuthorize(Roles = "rh")]
        public HttpResponseMessage Post([FromBody]UsuarioViewModel user)
        {

            using (var userRep = DataAccess.Instance.GetUsuarioRepository())
            {

                // save image to disk (we need do it before all other task)
                ImageHelper.saveAsImage(user.foto, user.samaccount);

                // map new values to our reference
                var newUser = Mapper.Map<UsuarioViewModel, Usuario>(user);

                // add to entity context
                userRep.Add(newUser);

                // commit changes
                userRep.SubmitChanges();

                return Request.CreateResponse(HttpStatusCode.OK, new MessageViewModel(HttpStatusCode.OK, "User Added", "User Added"));

            }
        }

        // PUT: api/sam/user/update/{id}
        [HttpPut]
        [Route("update/{id}")]
        [SamAuthorize(Roles = "rh,funcionario", AuthorizationType = SamAuthorize.AuthType.UpdateUser)]
        public HttpResponseMessage Put(int id, [FromBody]UsuarioViewModel user)
        {
            
            using (var userRep = DataAccess.Instance.GetUsuarioRepository())
            {

                // it will be updated with values provided by the parameter
                var userToBeUpdated = userRep.Find(u => u.id == id).SingleOrDefault();
                if (userToBeUpdated == null)
                {
                    throw new ExpectedException(HttpStatusCode.NotFound, "User Not Found", "The server could not find the user with id = '" + id + "'");
                }

                // map values from 'user' to 'userToBeUpdated'
                var updatedUser = Mapper.Map(user, userToBeUpdated);

                // update: flush changes to proxy
                userRep.Update(updatedUser);

                // commit changes to database
                userRep.SubmitChanges();

                // save image to disk
                ImageHelper.saveAsImage(user.foto, userToBeUpdated.samaccount);

                return Request.CreateResponse(HttpStatusCode.OK, new MessageViewModel(HttpStatusCode.OK, "User Updated", "User updated"));
                
            }

        }

        // DELETE: api/sam/user/delete/{id}
        [Route("delete/{id}")]
        [SamAuthorize(Roles = "rh")]
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            using (var userRep = DataAccess.Instance.GetUsuarioRepository())
            {
                userRep.Delete(id);
                return Request.CreateResponse(HttpStatusCode.OK, new MessageViewModel(HttpStatusCode.OK, "User Deleted", "User deleted"));
            }
        }
    }
}
