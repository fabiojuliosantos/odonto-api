using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Odonto.API.DTOs.Dentistas;
using Odonto.API.Models;
using Odonto.API.Services.Interface;

namespace Odonto.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DentistasController : ControllerBase
    {
        private readonly IDentistaService _service;
        private readonly IMapper _mapper;
        public DentistasController(IDentistaService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Dentista>> BuscarTodosDentistas()
        {
            var dentistas = _service.BuscarTodosDentistas();
            return Ok(dentistas);
        }
        
        [HttpGet("buscar-dentista-id/{id}")]
        public ActionResult<Dentista> BuscarDentistaPorId(int id)
        {
            var dentista = _service.BuscarPorId(id);
            return Ok(dentista);
        }
        
        [HttpPost("cadastrar-dentista")]
        public ActionResult<DentistasCadastroDTO> CadastrarDentista(DentistasCadastroDTO dentistaDto)
        {
            var dentista = _mapper.Map<Dentista>(dentistaDto);
            _service.CadastrarDentista(dentista);
            return Ok(dentista);    
        }

        [HttpPut("atualizar-dentista")]
        public ActionResult<DentistasDTO>AtualizarDentista(DentistasDTO dentistaDto)
        {
            var dentista = _mapper.Map<Dentista>(dentistaDto);
            
            _service.AtualizarDentista(dentista);
            
            return Ok(dentista);
        }
        [HttpDelete("excluir-dentista")]
        public ActionResult<Dentista>ExcluirDentista(Dentista dentista)
        {
            _service.ExcluirDentista(dentista);
            return Ok(dentista);
        }
    }
}
