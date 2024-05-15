using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Odonto.API.Models;
using Odonto.API.Services.Interface;

namespace Odonto.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultasController : ControllerBase
    {
        IConsultaService _service;

        public ConsultasController(IConsultaService service)
        {
            _service = service;
        }
        [HttpGet]
        public ActionResult<Consulta> BuscarTodasConsultas()
        {
            var consultas = _service.BuscarTodasConsultas();
            return Ok(consultas);
        }
        [HttpGet("buscar-consulta-id/{id}")]
        public ActionResult<Consulta> BuscarConsultaPorId(int id) 
        {
            var consulta = _service.BuscarConsultaPorId(id);
            return Ok(consulta);
        }
        
        [HttpPost("cadastrar-consulta")]
        public ActionResult<Consulta> CadastrarConsulta(Consulta consulta) 
        {
            _service.CadastrarConsulta(consulta);
            return Ok(consulta);
        }

        [HttpPut("atualizar-consulta")]
        public ActionResult<Consulta> AtualizarConsulta(Consulta consulta)
        {
            _service.AtualizarConsulta(consulta);
            return Ok(consulta);
        }
        
        [HttpDelete("excluir-consulta")]
        public ActionResult<Consulta> ExcluirConsulta(Consulta consulta)
        {
            _service.ExcluirConsulta(consulta);
            return Ok(consulta);
        }
    }
}
