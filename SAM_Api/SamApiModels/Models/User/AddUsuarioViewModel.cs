﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SamApiModels.User
{
    /// <summary>
    /// Representa um novo usuário
    /// </summary>
    public class AddUsuarioViewModel
    {
        /// <summary>
        /// Segue o formato UrlEncoded64
        /// </summary>
        [Required]
        public string foto { get; set; }

        /// <summary>
        /// Objeto do tipo Date que representa data de início desse usuário no SAM
        /// </summary>
        [Required]
        [DataType("System.DateTime")]
        public DateTime dataInicio { get; set; }

        /// <summary>
        /// Diz se o usuário está ou não ativo no sistema do SAM
        /// </summary>
        [Required]
        public bool ativo { get; set; }

        /// <summary>
        /// Identifica o cargo atual do usuário
        /// </summary>
        [Required]
        public int cargo { get; set; }

        /// <summary>
        /// Identifica o usuário no SAM, o samaccout é um dado que vem do ActiveDirectory da Opus
        /// </summary>
        [Required]
        public string samaccount { get; set; }

        /// <summary>
        /// Nome completo do usuário
        /// </summary>
        [Required]
        public string nome { get; set; }

        /// <summary>
        /// Indica o perfil do usuário, impactanto nas regras de acesso. Aceita os valores (Funcionario, RH)
        /// </summary>
        [Required]
        public string perfil { get; set; }

        /// <summary>
        /// Descrição do usuário como hobs por exemplo
        /// </summary>
        public string descricao { get; set; }

        /// <summary>
        /// Vetor de tamanho no máximo 3, contendo as redes sociais:(github; facebook; linkedin;)
        /// </summary>
        public string[] redes { get; set; }

        /// <summary>
        /// Quantidade de pontos que o usuário obtém
        /// </summary>
        public int pontos { get; set; }

        public AddUsuarioViewModel()
        {
            redes = new string[3];
        }
    }
}
