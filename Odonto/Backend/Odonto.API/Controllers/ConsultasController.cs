using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Odonto.API.DTOs.Consultas;
using Odonto.API.Models;
using Odonto.API.Services.Interface;

namespace Odonto.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultasController : ControllerBase
    {
        private readonly IConsultaService _service;
        private readonly IMapper _mapper;

        public ConsultasController(IConsultaService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        [HttpGet]
        public ActionResult<ConsultasDTO> BuscarTodasConsultas()
        {
            var consultas = _service.BuscarTodasConsultas();
            
            var consultasDto = _mapper.Map<IEnumerable<ConsultasDTO>>(consultas);
            
            return Ok(consultasDto);
        }
        [HttpGet("buscar-consulta-id/{id}")]
        public ActionResult<ConsultasDTO> BuscarConsultaPorId(int id)
        {
            var consulta = _service.BuscarConsultaPorId(id);

            var consultaDto = _mapper.Map<ConsultasDTO>(consulta);

            return Ok(consultaDto);
        }

        [HttpPost("cadastrar-consulta")]
        public ActionResult<ConsultasCadastroDTO> CadastrarConsulta(ConsultasCadastroDTO consultaDto)
        {
            var consulta = _mapper.Map<Consulta>(consultaDto);
                        
            _service.CadastrarConsulta(consulta);
            
            return Ok(consulta);
        }

        [HttpPut("atualizar-consulta")]
        public ActionResult<ConsultasDTO> AtualizarConsulta(ConsultasDTO consultaDto)
        {
            var consulta = _mapper.Map<Consulta>(consultaDto);

            _service.AtualizarConsulta(consulta);
            
            return Ok(consulta);
        }

        [HttpDelete("excluir-consulta")]
        public ActionResult<ConsultasDTO> ExcluirConsulta(ConsultasDTO consultaDto)
        {
            var consulta = _mapper.Map<Consulta>(consultaDto);

            _service.ExcluirConsulta(consulta);
            
            return Ok(consulta);
        }
    }
}
