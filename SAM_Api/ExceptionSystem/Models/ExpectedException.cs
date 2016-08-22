using System;
using System.Collections.Generic;
using System.Net;

namespace DefaultException.Models
{
    public class ExpectedException : Exception
    {

        private int Code { get; set; }

        public ExpectedException(HttpStatusCode code, string title, string reason = "") : base(title, new Exception(reason))
        {
            Code = (int)code;
        }

        public ExpectedException(int code, string title, string reason = "") : base(title, new Exception(reason))
        {
            Code = code;
        }

        public ExpectedException(int code, Exception reason) : base(reason.Message, reason.InnerException)
        {
            Code = code;
        }

        public ExpectedException(HttpStatusCode code, Exception reason) : base(reason.Message, reason.InnerException)
        {

            Code = (int)code;
        }

        public ExpectedException(HttpStatusCode code, string title, Exception reason) : base(title, reason)
        {
            Code = (int)code;
        }

        public ExpectedException(int code, string title, Exception reason) : base(title, reason)
        {
            Code = code;
        }

        public DescriptionMessage GetAsPrettyMessage()
        {
            var innerMessages = new List<string>();
            var innerException = InnerException;
            while (innerException != null)
            {
                var msg = innerException.Message;
                if (msg != string.Empty)
                {
                    innerMessages.Add(innerException.Message);
                }
                innerException = innerException.InnerException;
            }

            return new DescriptionMessage(Code, Message, innerMessages);
        }


    }
}