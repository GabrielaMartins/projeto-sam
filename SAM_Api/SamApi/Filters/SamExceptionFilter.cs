using DefaultException.Models;
using System;
using System.Configuration;
using System.Data.Entity.Validation;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http.Filters;
using System.Linq;
using System.Collections.Generic;

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
                var e = new ExpectedException(HttpStatusCode.BadRequest, "Entity Validation Error", ex);
                var errorMessage = e.GetAsPrettyMessage();
                context.Response = new HttpResponseMessage((HttpStatusCode)errorMessage.Code)
                {
                    Content = new ObjectContent(errorMessage.GetType(), errorMessage, new JsonMediaTypeFormatter()),
                    ReasonPhrase = errorMessage.Title
                };

            }
            else
            {
                // if we are in debug mode, we just return the exception
                #if DEBUG
                    context.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                    {
                        Content = new ObjectContent(context.Exception.GetType(), context.Exception, new JsonMediaTypeFormatter()),
                        ReasonPhrase = "Internal Server Error.",
                    };

                // else we log the exception to github
                #else

                    var wasCreated = LogException(context.Exception);

                    var hashCode = context.Exception.StackTrace.GetHashCode();

                    if (wasCreated)
                    {
                        var content = "A new unexpected exception has occurred and logged to github. " +
                                      $"See git issue 'Unexpected Exception #{hashCode}' to more detail.";

                        // retornar internal server error
                        context.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                        {
                            Content = new StringContent(content),
                            ReasonPhrase = "Internal Server Error",
                        };
                    }
                    else
                    {
                        var content = "A recidivist exception has occurred. " +
                                     $"See git issue 'Unexpected Exception #{hashCode}' to more detail.";

                        // retornar internal server error
                        context.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                        {
                            Content = new StringContent(content),
                            ReasonPhrase = "Internal Server Error.",
                        };
                    }
                #endif
            }
        }

        private bool LogException(Exception ex)
        {
            var hashCode = Convert.ToString(ex.StackTrace.GetHashCode());

            var user = ConfigurationManager.AppSettings["Git:User"];
            var password = ConfigurationManager.AppSettings["Git:Password"];
            var repOwner = ConfigurationManager.AppSettings["Git:RepOwner"];
            var repName = ConfigurationManager.AppSettings["Git:RepName"];

            var gitIssuer = new GitIssuer.GitIssuer(user, password, repName, repOwner);
            var criterias = new Dictionary<string, string>()
            {
                { "state", "open" }
            };

            var issues = gitIssuer.GetIssues(criterias);
            var issue = issues.Where(i => i.title.Contains(hashCode)).SingleOrDefault();
            if (issue == null)
            {
                // create an issue
                return gitIssuer.CreateIssue($"Unexpected Exception #{hashCode}", ex.StackTrace, new string[] {user}, new string[] { "bug" });

            }

            return false;
        }
    }
}