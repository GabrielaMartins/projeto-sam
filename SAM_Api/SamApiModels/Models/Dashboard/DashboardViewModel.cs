﻿using SamApiModels.Evento;
using SamApiModels.Promocao;
using SamApiModels.User;
using System.Collections.Generic;

namespace SamApiModels.Dashboard
{
    public class DashboardViewModel
    {
        public UsuarioViewModel Usuario { get; set; }

        public List<UltimoEventoViewModel> UltimosEventos { get; set; }

        public List<ProximaPromocaoViewModel> ProximasPromocoes { get; set; }

        public List<UsuarioViewModel> Ranking { get; set; }

        // TODO: REFATORAR (Lista de eventos)
        public List<dynamic> CertificacoesMaisProcuradas { get; set; }

        public DashboardViewModel()
        {
            
        }
        
    }
}