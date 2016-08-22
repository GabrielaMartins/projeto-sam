namespace SamApiModels.Login
{
    public class LoginViewModel
    {

        public string User { get; set; }

        public string Password { get; set; }

        public LoginViewModel()
        {

        }

        public LoginViewModel(string user, string password)
        {
            User = user;
            Password = password;
        }
    }
}