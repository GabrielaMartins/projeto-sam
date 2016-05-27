using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SamApi.Models
{
    public class Login
    {

        public string User { get; set; }

        public string Password { get; set; }

        public Login(string user, string password)
        {
            this.User = user;
            this.Password = password;
        }
    }
}