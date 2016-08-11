using System.Web.Http;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using Opus.DataBaseEnvironment;
using AutoMapper;
using SamDataBase.Model;
using SamApi.Helpers;
using SamApi.Attributes;
using SamApiModels.User;
using DefaultException.Models;
using System.IO;
using System.Configuration;
using System.Web.Http.Description;
using System.Web;
using Swashbuckle.Swagger.Annotations;

namespace SamApi.Controllers
{
    ///<Summary>
    /// Permite efetuar ações sobre usuários do SAM
    ///</Summary>
    [RoutePrefix("api/sam/user")]
    public class SamUserController : ApiController
    {

        /// <summary>
        /// Retorna a lista de usuários do SAM
        /// </summary>
        [SwaggerResponse(HttpStatusCode.OK, "Caso seja possível obter a lista de usuários do SAM", typeof(List<UsuarioViewModel>))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Caso a requisição não seja autorizada", typeof(DescriptionMessage))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Caso occora um erro não previsto", typeof(DescriptionMessage))]
        [HttpGet]
        [Route("all")]
        [SamResourceAuthorizer(Roles = "rh")]
        public HttpResponseMessage Get()
        {
            using (var userRep = DataAccess.Instance.GetUsuarioRepository())
            {
                var users = userRep.GetAll().ToList();

                var usersViewModel = Mapper.Map<List<Usuario>, List<UsuarioViewModel>>(users);

                return Request.CreateResponse(HttpStatusCode.OK, usersViewModel);
            }

        }

        /// <summary>
        /// Retorna um usuário específico do SAM
        /// </summary>
        /// <param name="samaccount">Identifica o usuário procurado.</param>
        [SwaggerResponse(HttpStatusCode.OK, "Caso o usuário do SAM seja encontrado", typeof(UsuarioViewModel))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Caso o usuário requerido não seja encontrado na base de dados do SAM", typeof(DescriptionMessage))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Caso a requisição não seja autorizada", typeof(DescriptionMessage))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Caso occora um erro não previsto", typeof(DescriptionMessage))]
        [HttpGet]
        [Route("{samaccount}")]
        [SamResourceAuthorizer(AuthorizationType = SamResourceAuthorizer.AuthType.TokenEquality)]
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

        /// <summary>
        /// Cria um novo usuário na base de dados do SAM.
        /// </summary>
        /// <param name="user">Usuário a ser inserido.</param>
      
        [SwaggerResponse(HttpStatusCode.OK, "Caso o usuário seja inserido com sucesso na base de dados do SAM", typeof(DescriptionMessage))]
        [SwaggerResponse(HttpStatusCode.Forbidden, "Caso o usuário a ser inserido ja exista na base de dados do SAM", typeof(DescriptionMessage))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Caso a requisição não seja autorizada", typeof(DescriptionMessage))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Caso occora um erro não previsto", typeof(DescriptionMessage))]
        [HttpPost]
        [Route("save")]
        [ResponseType(typeof(DescriptionMessage))]
        [SamResourceAuthorizer(Roles = "rh")]
        public HttpResponseMessage Post([FromBody]AddUsuarioViewModel user)
        {

            using (var userRep = DataAccess.Instance.GetUsuarioRepository())
            {

                var userFound = userRep.Find(u => u.samaccount == user.samaccount).SingleOrDefault() == null;
                if (userFound)
                {
                    throw new ExpectedException(HttpStatusCode.Forbidden, "Duplicated User", $"user '{user.samaccount}' already exists");
                }

                // save image to disk (we need do it before all other task)
                var logicPath = ConfigurationManager.AppSettings["LogicImagePath"];
                var physicalPath = HttpContext.Current.Server.MapPath(logicPath);

                ImageHelper.SaveAsImage(user.foto, user.samaccount, physicalPath);

                user.foto = $"{logicPath}{Path.PathSeparator}{user.samaccount}";

                // map new values to our reference
                var newUser = Mapper.Map<AddUsuarioViewModel, Usuario>(user);

                // add to entity context
                userRep.Add(newUser);

                // commit changes
                userRep.SubmitChanges();

                return Request.CreateResponse(HttpStatusCode.Created, new DescriptionMessage(HttpStatusCode.OK, "User Added", "User Added"));

            }
        }

        /// <summary>
        /// Atualiza as informações de um usuário na base de dados do SAM.
        /// </summary>
        /// <param name="id">Identifica o usuário a ser alterado.</param>
        [SwaggerResponse(HttpStatusCode.OK, "Caso o usuário seja alterado com sucesso na base de dados do SAM", typeof(DescriptionMessage))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Caso o usuário não seja encontrado na base de dados do SAM", typeof(DescriptionMessage))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Caso a requisição não seja autorizada", typeof(DescriptionMessage))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Caso occora um erro não previsto", typeof(DescriptionMessage))]
        [HttpPut]
        [Route("update/{id}")]
        [ResponseType(typeof(DescriptionMessage))]
        [SamResourceAuthorizer(AuthorizationType = SamResourceAuthorizer.AuthType.TokenEquality)]
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
                var logicPath = ConfigurationManager.AppSettings["LogicImagePath"];
                var physicalPath = HttpContext.Current.Server.MapPath(logicPath);

                ImageHelper.SaveAsImage(user.foto, user.samaccount, physicalPath);

                return Request.CreateResponse(HttpStatusCode.OK, new DescriptionMessage(HttpStatusCode.OK, "User Updated", "User updated"));
                
            }

        }

        /// <summary>
        /// Remove as informações de um usuário na base de dados do SAM.
        /// </summary>
        /// <param name="id">Identifica o usuário a ser removido.</param>
        [SwaggerResponse(HttpStatusCode.OK, "Caso o usuário seja removido com sucesso na base de dados do SAM", typeof(DescriptionMessage))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Caso o usuário não seja encontrado na base de dados do SAM", typeof(DescriptionMessage))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Caso a requisição não seja autorizada", typeof(DescriptionMessage))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Caso occora um erro não previsto", typeof(DescriptionMessage))]
        [HttpDelete]
        [Route("delete/{id}")]
        [ResponseType(typeof(DescriptionMessage))]
        [SamResourceAuthorizer(AuthorizationType = SamResourceAuthorizer.AuthType.TokenEquality)]
        public HttpResponseMessage Delete(int id)
        {
            using (var userRep = DataAccess.Instance.GetUsuarioRepository())
            {
                userRep.Delete(id);
                return Request.CreateResponse(HttpStatusCode.OK, new DescriptionMessage(HttpStatusCode.OK, "User Deleted", "User deleted"));
            }
        }
    }
}
