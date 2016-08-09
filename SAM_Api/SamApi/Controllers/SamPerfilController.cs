using System.Net.Http;
using System.Web.Http;
using System.Net;
using Opus.DataBaseEnvironment;
using System.Linq;
using AutoMapper;
using SamDataBase.Model;
using Opus.Helpers;
using System.Collections.Generic;
using SamApiModels.User;
using SamApiModels.Perfil;
using SamApi.Attributes;

namespace SamApi.Controllers
{

    [RoutePrefix("api/sam/perfil")]
    public class SamPerfilController : ApiController
    {

        [HttpGet]
        [Route("{samaccount}")]
        [SamAuthorize(Roles="rh,funcionario", AuthorizationType = SamAuthorize.AuthType.TokenEquality)]
        public HttpResponseMessage Get(string samaccount)
        {

            using (var userRep = DataAccess.Instance.GetUsuarioRepository())
            {
                var usuario = userRep.Find(u => u.samaccount == samaccount).SingleOrDefault();

                // dados para o perfil
                var usuarioViewModel = Mapper.Map<Usuario, UsuarioViewModel>(usuario);
                var eventosViewModel = userRep.RecuperaEventos(usuario);
                var promocoesAdquiridas = userRep.RecuperaPromocoesAdquiridas(usuario);

                var perfilViewModel = new PerfilViewModel()
                {
                    Usuario = usuarioViewModel,
                    Atividades = eventosViewModel.ToList(),
                    PromocoesAdquiridas = promocoesAdquiridas
                };

                return Request.CreateResponse(HttpStatusCode.OK, perfilViewModel);
            }

        }

        private void CreateChart()
        {

        }
    }
}
