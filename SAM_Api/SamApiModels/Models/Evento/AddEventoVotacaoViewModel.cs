﻿using SamModelValidationRules.Attributes.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamApiModels.Models.Evento
{
    /// <summary>
    /// Representa a inserção de um novo evento de votação
    /// </summary>
    public class AddEventoVotacaoViewModel
    {
        /// <summary>
        /// Representa o item da votação
        /// </summary>
        [Required]
        [ValidKey(ValidKeyAttribute.Entities.Item)]
        public int Item { get; set; }

        /// <summary>
        /// Representa o usuário do evento sendo votado
        /// </summary>
        [Required]
        [ValidKey(ValidKeyAttribute.Entities.Usuario)]
        public string Usuario { get; set; }

        /// <summary>
        /// Representa a data que ocorrerá o evento
        /// </summary>
        [Required]
        [ValidDate]
        public string Data { get; set; }

        /// <summary>
        /// Representa o tipo do evento
        /// </summary>
        [ReadOnly(true)]
        [DefaultValue("votacao")]
        public string tipo { get; private set; }

        /// <summary>
        /// Representa o estado do evento
        /// </summary>
        [ReadOnly(true)]
        [DefaultValue(false)]
        public bool estado { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public AddEventoVotacaoViewModel()
        {
            tipo = "votacao";
        }
    }
}