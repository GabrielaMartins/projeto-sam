using MessageSystem.Mensagem;
using System;
using System.Collections.Generic;
using System.Net;

namespace MessageSystem.Erro
{
    /// <summary>
    /// Representa os erros suportados pela API
    /// </summary>
    public class ErroEsperado : Exception
    {

        private int Code { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code">Código do erro</param>
        /// <param name="title">Título para o erro</param>
        /// <param name="reason">Detalhe do erro</param>
        public ErroEsperado(HttpStatusCode code, string title, string reason = "") : base(title, new Exception(reason))
        {
            Code = (int)code;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code">Código do erro</param>
        /// <param name="title">Título para o erro</param>
        /// <param name="reason">Detalhe do erro</param>
        public ErroEsperado(int code, string title, string reason = "") : base(title, new Exception(reason))
        {
            Code = code;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code">Código do erro</param>
        /// <param name="reason">Detalhe do erro</param>
        public ErroEsperado(int code, Exception reason) : base(reason.Message, reason.InnerException)
        {
            Code = code;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code">Código do erro</param>
        /// <param name="reason">Detalhe do erro</param>
        public ErroEsperado(HttpStatusCode code, Exception reason) : base(reason.Message, reason.InnerException)
        {

            Code = (int)code;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code">Código do erro</param>
        /// <param name="title">Titulo do erro</param>
        /// <param name="reason">Detalhe do erro</param>
        public ErroEsperado(HttpStatusCode code, string title, Exception reason) : base(title, reason)
        {
            Code = (int)code;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code">Código do erro</param>
        /// <param name="title">Titulo do erro</param>
        /// <param name="reason">Detalhe do erro</param>
        public ErroEsperado(int code, string title, Exception reason) : base(title, reason)
        {
            Code = code;
        }

        /// <summary>
        /// Retorna o erro formatado como uma mensagem
        /// </summary>
        /// <returns></returns>
        public DescriptionMessage GetAsPrettyMessage()
        {
            var innerMessages = new List<string>();
            var innerException = InnerException;
            while (innerException != null)
            {
                var msg = innerException.Message;
                if (msg != string.Empty)
                {
                    innerMessages.Add(innerException.Message);
                }
                innerException = innerException.InnerException;
            }

            return new DescriptionMessage(Code, Message, innerMessages);
        }


    }
}