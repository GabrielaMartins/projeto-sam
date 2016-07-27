using ExceptionSystem.Models;
using System.Data.Entity.Validation;
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
            
            if (context.Exception is ExpectedException)
            {
                var ex = context.Exception as ExpectedException;

                dynamic content = new {Info = ex.Info};
                if (ex.Reason != null)
                    content.Exception = ex.Reason;

                context.Response = new HttpResponseMessage((HttpStatusCode)ex.Info.Code)
                {
                    Content = new ObjectContent(content.GetType(), content, new JsonMediaTypeFormatter()),
                    ReasonPhrase = ex.Info.Title
                };

            }
            else if (context.Exception is DbEntityValidationException)
            {
                var ex = context.Exception as DbEntityValidationException;
                var content = new
                {
                    Info = new ErrorMessage(HttpStatusCode.BadRequest, "Properties required", ex.Message),
                    Exception = ex
                };

                context.Response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new ObjectContent(content.GetType(), content, new JsonMediaTypeFormatter()),
                    ReasonPhrase = "Property Validator Failed"
                };
                
            }
            else
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new ObjectContent(context.Exception.GetType(), context.Exception, new JsonMediaTypeFormatter()),
                    ReasonPhrase = context.Exception.Message,
                };

               
            }
        }
    }
}