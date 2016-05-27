using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace SamApi.Models
{
    public class Message
    {

        public int Code { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public Message(HttpStatusCode code, string title, string description)
        {
            Code = (int)code;
            Title = title;
            Description = description;
        }

        public Message(int code, string title, string description)
        {
            Code = code;
            Title = title;
            Description = description;
        }
    }
}