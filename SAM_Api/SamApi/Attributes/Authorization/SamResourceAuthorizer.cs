using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Controllers;
using DefaultException.Models;

namespace SamApi.Attributes.Authorization
{
    public class SamResourceAuthorizer : AuthorizeAttribute
    {

        public enum AuthType
        {
            Basic,
            TokenEquality
        }

        public AuthType AuthorizationType { get; set; }

        private string ErrorMessage;

        public SamResourceAuthorizer(AuthType authorizationType = AuthType.Basic)
        {
            AuthorizationType = authorizationType;
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {

            // check basic authorization
            if (!BasicAuthorization(actionContext))
            {
                return false;
            }

            bool isAuthorized = true;
            switch (AuthorizationType)
            {
                case AuthType.TokenEquality:
                    isAuthorized = AuthorizeTokenEquality(actionContext);
                    break;
            }

            return isAuthorized;

        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            throw new ExpectedException(HttpStatusCode.Unauthorized, "Unauthorized", ErrorMessage);

        }

        private bool AuthorizeTokenEquality(HttpActionContext context)
        {
            try
            {
                var r = false;
                var userId = 0;
                var id = Convert.ToInt32(context.Request.Headers.GetValues("id").SingleOrDefault());
                var perfil = context.Request.Headers.GetValues("perfil").SingleOrDefault();
                var samaccount = context.Request.Headers.GetValues("samaccount").SingleOrDefault();
                var size = context.Request.RequestUri.Segments.Length;
                var param = context.Request.RequestUri.Segments[size - 1];

                if (int.TryParse(param, out userId))
                {
                    r = (userId == id || perfil.ToLower() == "rh");
                }
                else
                {
                    r = (param == samaccount || perfil.ToLower() == "rh");
                }

                if (r == false)
                {
                    ErrorMessage = "You can't view, delete or update informations about other users";
                }

                return r;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not find some keys in request header. Make sure you decoded token in every request", ex);
            }
        }

        private bool BasicAuthorization(HttpActionContext actionContext)
        {

            if (string.IsNullOrEmpty(Roles) && string.IsNullOrEmpty(Users))
            {
                return true;
            }

            try
            {
                var perfil = actionContext.Request.Headers.GetValues("perfil").SingleOrDefault();
                var samaccount = actionContext.Request.Headers.GetValues("samaccount").SingleOrDefault();

                var isAuthorized = Roles.ToLowerInvariant().Contains(perfil.ToLowerInvariant()) ||
                                   Users.ToLowerInvariant().Contains(samaccount.ToLowerInvariant());

                if (!isAuthorized)
                {
                    ErrorMessage = $"User '{samaccount}' is not authorized to perform this action";
                }

                return isAuthorized;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not find some keys in request header. Make sure you decoded token in every request", ex);
            }

        }

    }
}