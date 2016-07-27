using System;
using System.Net;

namespace ExceptionSystem.Models
{
    public class ErrorMessage
    {

        public int Code { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

       
        public ErrorMessage(HttpStatusCode code, string title, string description)
        {
            Code = (int)code;
            Title = title;
            Description = description;
        }

        public ErrorMessage(int code, string title, string description)
        {
            Code = code;
            Title = title;
            Description = description;
        }
    }
}