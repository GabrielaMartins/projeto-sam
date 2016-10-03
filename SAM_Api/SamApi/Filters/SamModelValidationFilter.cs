using MessageSystem.Mensagem;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace SamApi.Filters
{
    /// <summary>
    /// É executado quando a validação de um modelo falha
    /// </summary>
    public class SamModelValidationFilter : ActionFilterAttribute
    {
        /// <summary>
        /// É executado quando a validação de um modelo falha, retornando 400(Bad Request) e uma mensagem de erro
        /// </summary>
        /// <param name="actionContext">
        /// Representa o contexto em que a ação ocorreu
        /// </param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
            {

                var errorList = actionContext.ModelState.Values.SelectMany(m => m.Errors)
                                 .Select(e => e.ErrorMessage).Where(e => e != "")
                                 .ToList();

                var error = new DescriptionMessage(HttpStatusCode.BadRequest, "Invalid value" , errorList);
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new ObjectContent(error.GetType(), error, new JsonMediaTypeFormatter()),
                    ReasonPhrase = "Invalid Value Supplied"
                };
            }
        }
    }
}