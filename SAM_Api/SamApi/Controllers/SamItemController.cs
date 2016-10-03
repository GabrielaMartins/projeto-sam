using System.Web.Http;
using System.Net.Http;
using System.Net;
using System.Linq;
using SamApi.Attributes.Authorization;
using SamApiModels.Item;
using SamServices.Services;
using Swashbuckle.Swagger.Annotations;
using System.Collections.Generic;
using MessageSystem.Mensagem;

namespace SamApi.Controllers
{
    /// <summary>
    /// Permite efetuar ações sobre os itens do SAM
    /// </summary>
    [RoutePrefix("api/sam/item")]
    public class SamItemController : ApiController
    {
        /// <summary>
        /// Recupera a lista de itens do SAM
        /// </summary>
        [HttpGet]
        [Route("all")]
        [SwaggerResponse(HttpStatusCode.OK, "Caso seja possível obter a lista de itens do SAM", typeof(List<ItemViewModel>))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Caso a requisição não seja autorizada", typeof(DescriptionMessage))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Caso occora um erro não previsto", typeof(DescriptionMessage))]
        [SamResourceAuthorizer(Roles = "rh,funcionario")]
        public HttpResponseMessage Get()
        {

            var itens = ItemServices.RecuperaItens();
            return Request.CreateResponse(HttpStatusCode.OK, itens);

        }

        /// <summary>
        /// Recupera um item específico do SAM
        /// </summary>
        /// <param name="id">Identifica o item do SAM</param>
        [HttpGet]
        [Route("{id}")]
        [SwaggerResponse(HttpStatusCode.OK, "Caso seja possível obter o item do SAM", typeof(ItemViewModel))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Caso a requisição não seja autorizada", typeof(DescriptionMessage))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Caso occora um erro não previsto", typeof(DescriptionMessage))]
        [SamResourceAuthorizer(Roles = "rh,funcionario")]
        public HttpResponseMessage Get(int id)
        {
            var itemViewModel = ItemServices.Recupera(id);
            return Request.CreateResponse(HttpStatusCode.OK, itemViewModel);
        }

        /// <summary>
        /// Cria um novo item no sam
        /// </summary>
        /// <param name="item">Dados do novo item</param>
        /// <returns></returns>
        [HttpPost]
        [Route("save")]
        [SwaggerResponse(HttpStatusCode.OK, "Caso seja possível criar um novo item no SAM", typeof(DescriptionMessage))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Caso a requisição não seja autorizada", typeof(DescriptionMessage))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Caso occora um erro não previsto", typeof(DescriptionMessage))]
        [SamResourceAuthorizer(Roles = "rh")]
        public HttpResponseMessage Post(AddItemViewModel item)
        {
            ItemServices.CriaItem(item);

            return Request.CreateResponse(HttpStatusCode.OK, new DescriptionMessage(HttpStatusCode.OK, "Item Added", "Item Added"));
            
        }

        /// <summary>
        /// Permite alterar os dados de um item do sam
        /// </summary>
        /// <param name="id">Identifica o item do SAM</param>
        /// <param name="item">Contém as informações para a atualização</param>
        [HttpPut]
        [Route("update/{id}")]
        [SwaggerResponse(HttpStatusCode.OK, "Caso seja possível alterar um item do SAM", typeof(DescriptionMessage))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Caso a requisição não seja autorizada", typeof(DescriptionMessage))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Caso occora um erro não previsto", typeof(DescriptionMessage))]
        [SamResourceAuthorizer(Roles = "rh")]
        public HttpResponseMessage Put(int id, UpdateItemViewModel item)
        {

            ItemServices.AtualizaItem(id, item);

            return Request.CreateResponse(HttpStatusCode.OK, new DescriptionMessage(HttpStatusCode.OK, "Item Updated", $"Item #{id} Updated"));
            

        }

        /// <summary>
        /// Permite remover um item do SAM
        /// </summary>
        /// <param name="id">Identifica o item a ser removido</param>
        [HttpDelete]
        [Route("delete/{id}")]
        [SwaggerResponse(HttpStatusCode.OK, "Caso seja possível remover um item do SAM", typeof(DescriptionMessage))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Caso a requisição não seja autorizada", typeof(DescriptionMessage))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Caso occora um erro não previsto", typeof(DescriptionMessage))]
        [SamResourceAuthorizer(Roles = "rh")]
        public HttpResponseMessage Delete(int id)
        {
            ItemServices.DeleteItem(id);
            return Request.CreateResponse(HttpStatusCode.OK, new DescriptionMessage(HttpStatusCode.OK, "Item Deleted", $"Item #{id} Deleted")); 
        }
    }
}
