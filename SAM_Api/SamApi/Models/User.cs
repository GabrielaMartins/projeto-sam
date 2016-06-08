using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SamApi.Models
{
    public class User
    {

        public string Key { get; private set; }
        public string Name { get; set; }
        public string Email { get; set; }
      
        public User(string key, string name, string email)
        {
            Key = key;
            Name = name;
            Email = email;
        }
    }
}