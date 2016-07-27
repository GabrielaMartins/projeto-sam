using System;
using System.Net;

namespace ExceptionSystem.Models
{
    public class ExpectedException : Exception
    {

        public ErrorMessage Info { get; set; }

        public Exception Reason { get; set; }

        public ExpectedException(int code, string title, Exception reason = null)
        {

            var innerMessage = string.Empty;
            if (reason.InnerException != null)
            {
                innerMessage = reason.InnerException.Message;
            }

            Info = new ErrorMessage(code, title, reason.Message + innerMessage);
            Reason = reason;
        }

        public ExpectedException(HttpStatusCode code, string title, Exception reason)
        {
           
            var innerMessage = string.Empty;
            if (reason.InnerException != null)
            {
                innerMessage = reason.InnerException.Message;
            }

            Info = new ErrorMessage(code, title, reason.Message + innerMessage);
            Reason = reason;
        }

    }
}