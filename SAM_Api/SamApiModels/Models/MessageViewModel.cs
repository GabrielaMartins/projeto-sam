using System.Net;

namespace SamApiModels
{
    public class MessageViewModel
    {

        public int Code { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public static readonly MessageViewModel TokenMissing = new MessageViewModel(HttpStatusCode.BadRequest, "Invalid header", "The server cannot find a key named 'token' in http header");

        public static readonly MessageViewModel InvalidToken = new MessageViewModel(HttpStatusCode.Unauthorized, "Invalid token", "The server cannot validate the token");

        public static readonly MessageViewModel Unauthorized = new MessageViewModel(HttpStatusCode.Unauthorized, "Unauthorized", "The user has no permission");

        public static readonly MessageViewModel Unauthenticated = new MessageViewModel(HttpStatusCode.Forbidden, "Unauthenticated", "The server could not authenticated the user");

        public MessageViewModel(HttpStatusCode code, string title, string description)
        {
            Code = (int)code;
            Title = title;
            Description = description;
        }

        public MessageViewModel(int code, string title, string description)
        {
            Code = code;
            Title = title;
            Description = description;
        }
    }
}