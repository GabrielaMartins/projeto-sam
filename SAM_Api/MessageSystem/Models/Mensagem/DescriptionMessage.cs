using System.Collections.Generic;
using System.Net;

namespace MessageSystem.Mensagem
{
    /// <summary>
    /// Representa o formato de mensagens da API
    /// </summary>
    public class DescriptionMessage
    {
        /// <summary>
        /// Código da mensagem
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Tpitulo da mensagem
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Detalhes da mensagem
        /// </summary>
        public List<string> Details { get; set; }


        /// <summary>
        /// Construtor do objeto
        /// </summary>
        /// <param name="code">Código da mensagem</param>
        /// <param name="title">Título da mensagem</param>
        /// <param name="detail">Detalhe da mensagem</param>
        public DescriptionMessage(HttpStatusCode code, string title, string detail = "")
        {
            Code = (int)code;
            Title = title;
            if(detail == "")
            {
                Details = new List<string>();
            }

            Details = new List<string>(){detail};
        }

        /// <summary>
        /// Construtor do objeto
        /// </summary>
        /// <param name="code">Código da mensagem</param>
        /// <param name="title">Título da mensagem</param>
        /// <param name="detail">Detalhe da mensagem</param>
        public DescriptionMessage(int code, string title, string detail = "")
        {
            Code = code;
            Title = title;
            if (detail == "")
            {
                Details = new List<string>();
            }

            Details = new List<string>() { detail };
        }

        /// <summary>
        /// Construtor do objeto
        /// </summary>
        /// <param name="code">Código da mensagem</param>
        /// <param name="title">Título da mensagem</param>
        /// <param name="details">Detalhes da mensagem</param>
        public DescriptionMessage(HttpStatusCode code, string title, List<string> details)
        {
            Code = (int)code;
            Title = title;
            Details = details;
        }

        /// <summary>
        /// Construtor do objeto
        /// </summary>
        /// <param name="code">Código da mensagem</param>
        /// <param name="title">Título da mensagem</param>
        /// <param name="details">Detalhes da mensagem</param>
        public DescriptionMessage(int code, string title, List<string> details)
        {
            Code = code;
            Title = title;
            Details = details;
        }
    }
}