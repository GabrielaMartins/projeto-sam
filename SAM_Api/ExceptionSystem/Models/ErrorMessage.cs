using System;
using System.Collections.Generic;
using System.Net;

namespace ExceptionSystem.Models
{
    public class ErrorMessage
    {

        public int Code { get; set; }

        public string Title { get; set; }

        public List<string> Details { get; set; }

       
        public ErrorMessage(HttpStatusCode code, string title, List<string> details)
        {
            Code = (int)code;
            Title = title;
            Details = details;
        }

        public ErrorMessage(int code, string title, List<string> description)
        {
            Code = code;
            Title = title;
            Details = description;
        }
    }
}