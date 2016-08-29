using DefaultException.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace SamApi.Filters
{
    public class SamModelValidationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.ModelState.IsValid == false)
            {

                var errorList = actionContext.ModelState.Values.SelectMany(m => m.Errors)
                                 .Select(e => e.ErrorMessage)
                                 .ToList();

                var details = new List<string>();
                var title = string.Empty;
                foreach (var err in errorList)
                {
                    var values = err.Split('|');
                    title = values[0];
                    for(int i = 1; i < values.Length; i++)
                    {
                        var r = values[i];
                        details.Add(values[i]);
                    }
                }

                var error = new DescriptionMessage(HttpStatusCode.BadRequest, title, details);
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new ObjectContent(error.GetType(), error, new JsonMediaTypeFormatter()),
                    ReasonPhrase = title
                };
            }
        }
    }
}