using System;

namespace SamHelpers
{

    public class ActiveDirectoryUser
    {

        public string Key { get; private set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime CreationDate { get; set; }
       
        public ActiveDirectoryUser(string key, string name, string email, DateTime creationDate)
        {
            Key = key;
            Name = name;
            Email = email;
            CreationDate = creationDate;
        }

    }
}