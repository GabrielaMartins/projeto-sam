using AutoMapper;
using DefaultException.Models;
using Opus.DataBaseEnvironment;
using SamApi.Attributes.Authorization;
using SamApiModels.Models.Agendamento;
using SamDataBase.Model;
using Swashbuckle.Swagger.Annotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SamApi.Controllers
{
    [RoutePrefix("api/sam/scheduling")]
    public class SamAgendamentoController : ApiController
    {

        /// <summary>
        /// Cria o agendamento de um evento no SAM
        /// </summary>
        [SwaggerResponse(HttpStatusCode.OK, "Caso seja possível agendar o evento do SAM", typeof(DescriptionMessage))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Caso a requisição não seja autorizada", typeof(DescriptionMessage))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Caso occora um erro não previsto", typeof(DescriptionMessage))]
        [SamResourceAuthorizer(Roles = "funcionario")]
        [HttpPost]
        [Route("create")]
        public HttpResponseMessage Create(AgendamentoViewModel agendamento)
        {
            using (var rep = DataAccess.Instance.GetEventoRepository())
            {
                var evento = Mapper.Map<AgendamentoViewModel, Evento>(agendamento);
                evento.tipo = "agendamento";

                rep.Add(evento);
                rep.SubmitChanges();

                // create a pendency to new event
                GeneratePendencyFor(evento);

                return Request.CreateResponse(HttpStatusCode.Created, new DescriptionMessage(HttpStatusCode.Created, "Scheduling done", ""));
            }
        }

        private void GeneratePendencyFor(Evento evt)
        {
            using (var pendencyRep = DataAccess.Instance.GetPendenciaRepository())
            using (var userRep = DataAccess.Instance.GetUsuarioRepository())
            {
                var users = userRep.Find(u => u.perfil == "RH").ToList();
                foreach(var u in users)
                {

                    var pendencia = new Pendencia()
                    {
                        usuario = u.id,
                        evento = evt.id,
                        Evento = evt,
                        Usuario = evt.Usuario,
                        estado = false
                    };

                    pendencyRep.Add(pendencia);
                    pendencyRep.SubmitChanges();
                }
                
            }
        }
    }
}
