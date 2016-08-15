using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamApiModels.Models.Agendamento
{
    public class AgendamentoViewModel
    {
        /// <summary>
        /// Identifica o item associado ao agendamento
        /// </summary>
        public int Item;

        /// <summary>
        /// Identifica a categoria associada ao item do agendamento
        /// </summary>
        public int Categoria;

        /// <summary>
        /// Identifica o funcionário requerendo o agendamento
        /// </summary>
        public string Funcionario;

        /// <summary>
        /// Data na qual irá ocorrer o evento
        /// </summary>
        public DateTime Data;

        public AgendamentoViewModel()
        {

        }

    }
}
