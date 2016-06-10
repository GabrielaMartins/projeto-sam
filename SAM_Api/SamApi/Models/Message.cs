using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace SamApi.Helpers
{
    public class Message
    {

        public int Code { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public static readonly Message TokenMissing = new Message(HttpStatusCode.BadRequest, "Invalid header", "The server cannot find a key named 'token' in http header");

        public static readonly Message InvalidToken = new Message(HttpStatusCode.BadRequest, "Invalid token", "The server cannot validate the token");

        public static readonly Message Unauthorized = new Message(HttpStatusCode.Unauthorized, "Unauthorized", "You have no permission");

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