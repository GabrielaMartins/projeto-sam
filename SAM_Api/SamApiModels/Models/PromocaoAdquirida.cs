using SamApiModels.Cargo;
using SamApiModels.User;
using System;

namespace SamApiModels
{
    public class PromocaoAdquiridaViewModel
    {

        public UsuarioViewModel Usuario { get; set; }

        public CargoViewModel CargoAdquirido { get; set; }

        public CargoViewModel CargoAnterior { get; set; }

        public DateTime Data { get; set; }

        public PromocaoAdquiridaViewModel()
        {

        }

    }
}