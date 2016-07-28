using System;
using System.Web.Http;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using Opus.DataBaseEnvironment;
using SamApiModels;
using AutoMapper;
using SamDataBase.Model;
using Opus.Helpers;
using SamApi.Helpers;
using System.Data.Entity.Validation;
using ExceptionSystem.Models;

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
        public HttpResponseMessage GetBySamaccount(string samaccount)
        {

            // TODO: verificações com o token
            using (var userRep = DataAccess.Instance.GetUsuarioRepository())
            {

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
        [HttpPost]
        public HttpResponseMessage Post([FromBody]UsuarioViewModel user)
        {

            var token = HeaderHelper.ExtractHeaderValue(Request, "token");
            var decodedToken = JwtHelper.DecodeToken(token.SingleOrDefault());
            var context = decodedToken["context"] as Dictionary<string, object>;
            var userInfo = context["user"] as Dictionary<string, object>;
            var samaccount = userInfo["samaccount"] as string;

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
        [Route("update/{id}")]
        [HttpPut]
        public HttpResponseMessage Put(int id, [FromBody]UsuarioViewModel user)
        {


            var token = HeaderHelper.ExtractHeaderValue(Request, "token").SingleOrDefault();
            var decodedToken = JwtHelper.DecodeToken(token);
            var context = decodedToken["context"] as Dictionary<string, object>;
            var userInfo = context["user"] as Dictionary<string, object>;
            var samaccount = userInfo["samaccount"] as string;
            var perfil = context["perfil"] as string;

            using (var userRep = DataAccess.Instance.GetUsuarioRepository())
            {

                // is the user using the service at this moment
                var userMakingAction = userRep.Find(u => u.samaccount == samaccount).SingleOrDefault();

                // it will be updated with values provided by the parameter
                var userToBeUpdated = userRep.Find(u => u.id == id).SingleOrDefault();
                if (userToBeUpdated == null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new MessageViewModel(HttpStatusCode.NotFound, "User Not Found", "The server could not find the user with id = '" + id + "'"));
                }

                // only RH can update different staffs, else just can update himself
                if (perfil == "RH" || userMakingAction.id == userToBeUpdated.id)
                {

                    // map values from 'user' to 'userToBeUpdated'
                    var updatedUser = Mapper.Map(user, userToBeUpdated);

                    // update: flush changes to proxy
                    userRep.Update(updatedUser);

                    try
                    {
                        // commit changes to database
                        userRep.SubmitChanges();
                    }
                    catch (DbEntityValidationException ex)
                    {
                        // if error, we delete the image
                        ImageHelper.DeleteImage(user.samaccount);

                        throw new ExpectedException(HttpStatusCode.BadRequest, "Properties required", ex);
                    }

                    // save to disk
                    ImageHelper.saveAsImage(user.foto, samaccount);

                    return Request.CreateResponse(HttpStatusCode.OK, new MessageViewModel(HttpStatusCode.OK, "User Updated", "User updated"));

                }
                else
                {
                    // return an error
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, new MessageViewModel(HttpStatusCode.Unauthorized, "Unauthorized", "You can't update other users"));
                }
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
