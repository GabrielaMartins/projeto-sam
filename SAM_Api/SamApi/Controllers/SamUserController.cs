using System.Web.Http;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using SamApi.Attributes.Authorization;
using SamApiModels.User;
using Swashbuckle.Swagger.Annotations;
using SamApiModels.Models.User;
using SamServices.Services;
using SamModelValidationRules.Attributes.Validation;
using MessageSystem.Mensagem;

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
        [SamResourceAuthorizer(Roles = "rh")]
        [HttpGet]
        [Route("all")]
        public HttpResponseMessage Get()
        {

            var users = UsuarioServices.RecuperaTodos();
            return Request.CreateResponse(HttpStatusCode.OK, users);
        }

        /// <summary>
        /// Retorna um usuário específico do SAM
        /// </summary>
        /// <param name="samaccount">Identifica o usuário procurado.</param>
        [SwaggerResponse(HttpStatusCode.OK, "Caso o usuário do SAM seja encontrado", typeof(UsuarioViewModel))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Caso o usuário requerido não seja encontrado na base de dados do SAM", typeof(DescriptionMessage))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Caso a requisição não seja autorizada", typeof(DescriptionMessage))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Caso occora um erro não previsto", typeof(DescriptionMessage))]
        [SamResourceAuthorizer(AuthorizationType = SamResourceAuthorizer.AuthType.TokenEquality)]
        [HttpGet]
        [Route("{samaccount}")]
        public HttpResponseMessage GetBySamaccount(string samaccount)
        {
            var usuario = UsuarioServices.Recupera(samaccount);
            return Request.CreateResponse(HttpStatusCode.OK, usuario);
        }

        /// <summary>
        /// Atribui pontos ao usuário baseado em um certo evento do SAM
        /// </summary>
        /// <param name="atribuicao">Representa os dados da atribuição de pontos</param>
        [SwaggerResponse(HttpStatusCode.OK, "Caso seja possível atribuir os pontos ao usuário do SAM", typeof(DescriptionMessage))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Caso o usuário requerido não seja encontrado na base de dados do SAM", typeof(DescriptionMessage))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Caso a requisição não seja autorizada", typeof(DescriptionMessage))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Caso occora um erro não previsto", typeof(DescriptionMessage))]
        [SamResourceAuthorizer(Roles = "rh")]
        [HttpPost]
        [Route("atribuicao")]
        public HttpResponseMessage AtribuiPontos(AtribuicaoPontosUsuarioViewModel atribuicao)
        {
            UsuarioServices.AtribuiPontos(atribuicao);
            return Request.CreateResponse(HttpStatusCode.OK, new DescriptionMessage(HttpStatusCode.OK, "points granted", $"You granted points to user '{atribuicao.Usuario}' for the event #{atribuicao.Evento}"));
        }

        /// <summary>
        /// Cria um novo usuário na base de dados do SAM.
        /// </summary>
        /// <param name="user">Usuário a ser inserido.</param>

        [SwaggerResponse(HttpStatusCode.OK, "Caso o usuário seja inserido com sucesso na base de dados do SAM", typeof(DescriptionMessage))]
        [SwaggerResponse(HttpStatusCode.Forbidden, "Caso o usuário a ser inserido ja exista na base de dados do SAM", typeof(DescriptionMessage))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Caso a requisição não seja autorizada", typeof(DescriptionMessage))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Caso occora um erro não previsto", typeof(DescriptionMessage))]
        [SamResourceAuthorizer(Roles = "rh")]
        [HttpPost]
        [Route("save")]
        public HttpResponseMessage Post([FromBody]AddUsuarioViewModel user)
        {
            UsuarioServices.CriaUsuario(user);
            return Request.CreateResponse(HttpStatusCode.Created, new DescriptionMessage(HttpStatusCode.OK, "User Added", "User Added"));
        }

        /// <summary>
        /// Atualiza as informações de um usuário na base de dados do SAM.
        /// </summary>
        /// <param name="samaccount">Identifica o usuário a ser alterado.</param>
        /// <param name="user">Dados para ser atualizados serem alterados.</param>
        [SwaggerResponse(HttpStatusCode.OK, "Caso o usuário seja alterado com sucesso na base de dados do SAM", typeof(DescriptionMessage))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Caso o usuário não seja encontrado na base de dados do SAM", typeof(DescriptionMessage))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Caso a requisição não seja autorizada", typeof(DescriptionMessage))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Caso occora um erro não previsto", typeof(DescriptionMessage))]
        [SamResourceAuthorizer(AuthorizationType = SamResourceAuthorizer.AuthType.TokenEquality)]
        [HttpPut]
        [Route("update/{samaccount}")]
        public HttpResponseMessage Put([ValidKey(ValidKeyAttribute.Entities.Usuario)]string samaccount, UpdateUsuarioViewModel user)
        {
            UsuarioServices.AtualizaUsuario(samaccount, user);
            return Request.CreateResponse(HttpStatusCode.OK, new DescriptionMessage(HttpStatusCode.OK, "User Updated", "User updated"));
        }

        /// <summary>
        /// Remove as informações de um usuário na base de dados do SAM.
        /// </summary>
        /// <param name="samaccount">Identifica o usuário a ser removido.</param>
        [SwaggerResponse(HttpStatusCode.OK, "Caso o usuário seja removido com sucesso na base de dados do SAM", typeof(DescriptionMessage))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Caso o usuário não seja encontrado na base de dados do SAM", typeof(DescriptionMessage))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Caso a requisição não seja autorizada", typeof(DescriptionMessage))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Caso occora um erro não previsto", typeof(DescriptionMessage))]
        [SamResourceAuthorizer(Roles = "rh")]
        [HttpDelete]
        [Route("delete/{samaccount}")]
        public HttpResponseMessage Delete(string samaccount)
        {
            UsuarioServices.DeletaUsuario(samaccount);
            return Request.CreateResponse(HttpStatusCode.OK, new DescriptionMessage(HttpStatusCode.OK, "User Deleted", "User deleted"));
        }
    }
}
