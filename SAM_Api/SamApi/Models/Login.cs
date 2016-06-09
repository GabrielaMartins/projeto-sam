namespace SamApi.Models
{
    public class Login
    {

        public string User { get; set; }

        public string Password { get; set; }

        public Login()
        {

        }

        public Login(string user, string password)
        {
            User = user;
            Password = password;
        }
    }
}