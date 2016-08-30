using System.ComponentModel.DataAnnotations;

namespace SamApiModels.Login
{
    /// <summary>
    /// Representa as credenciais de um usuário
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// Representa o usuario na rede da OPUS
        /// </summary>
        [Required]
        public string User { get; set; }

        /// <summary>
        /// Representa a senha do usuário na rede da OPUS
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// Contrutor do objeto
        /// </summary>
        public LoginViewModel()
        {

        }

        /// <summary>
        /// Construtor do objeto
        /// </summary>
        /// <param name="user">Representa o usuário da rede da OPUS</param>
        /// <param name="password">Representa a senha do usuário na rede da Opus</param>
        public LoginViewModel(string user, string password)
        {
            User = user;
            Password = password;
        }
    }
}