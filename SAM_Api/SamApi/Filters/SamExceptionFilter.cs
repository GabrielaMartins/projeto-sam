using ExceptionSystem.Models;
using System.Data.Entity.Validation;
using System.Dynamic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http.Filters;

namespace SamApi.Filters
{
    public class SamExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            // Exception known to us
            if (context.Exception is ExpectedException)
            {
                var ex = context.Exception as ExpectedException;
                var errorMessage = ex.GetAsPrettyMessage();
                context.Response = new HttpResponseMessage((HttpStatusCode)errorMessage.Code)
                {
                    Content = new ObjectContent(errorMessage.GetType(), errorMessage, new JsonMediaTypeFormatter()),
                    ReasonPhrase = errorMessage.Title
                };

            }
            else if (context.Exception is DbEntityValidationException)
            {
               
                var ex = context.Exception as DbEntityValidationException;
                var e = new ExpectedException(HttpStatusCode.BadRequest, ex);
                var errorMessage = e.GetAsPrettyMessage();
                context.Response = new HttpResponseMessage((HttpStatusCode)errorMessage.Code)
                {
                    Content = new ObjectContent(errorMessage.GetType(), errorMessage, new JsonMediaTypeFormatter()),
                    ReasonPhrase = errorMessage.Title
                };

            }
            else
            {
                // TODO: logar as exceções não esperadas

                // retornar internal server error
                context.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new ObjectContent(context.Exception.GetType(), context.Exception, new JsonMediaTypeFormatter()),
                    ReasonPhrase = "Unexpected Error",
                };

               
            }
        }
    }
}